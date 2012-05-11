using System;
using System.Text;
using System.IO;
using System.Data.SQLite;
using System.Collections.Generic;
using FM;

namespace BandwidthAnalyzer
{
    public class BandwidthAnalyzer : FirewallModule
    {

        private const string MODULE_NAME = "Bandwidth Analyzer Module";
        private const int BUFFER_MAX_SIZE = 5000;
        private string dbPath = null;

        // flag for other classes to set for clearing the log on next write
        public static bool DO_CLEAR_LOG = false;

        public static string DB_NAME = "connection_db.db";

        public const string DIRECTION_INBOUND = "0";
        public const string DIRECTION_OUTBOUND = "1";

        // string types
        public const string TCP = "TCP";
        public const string UDP = "UDP";
        public const string SNMP = "SNMP";
        public const string DHCP = "DHCP";
        public const string DNS = "DNS";
        public const string ARP = "ARP";
        public const string IP = "IP";
        public const string ICMP = "ICMP";
        public const string ICMPV6 = "ICMPv6";
        public const string EETH = "EEth";
        public const string ETHERNET = "Ethernet";
        public const string NO_IP = "N/A";

        private List<String> buffer = null;
        private int bufferSize = 0;

        private SQLiteConnection db = null;

        public BandwidthAnalyzer()
            : base()
        {
            init();
        }

        public BandwidthAnalyzer(INetworkAdapter adapter)
            : base(adapter)
        {
            init();
        }

        private void init()
        {
            Help();
            initBuffer();
            DO_CLEAR_LOG = false;
        }

        public static string GetDBPath()
        {
            string folder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            folder = folder + Path.DirectorySeparatorChar + "firebwall";
            if (!Directory.Exists(folder))
                Directory.CreateDirectory(folder);
            if (!Directory.Exists(folder))
                Directory.CreateDirectory(folder);
            return folder;
        }

        // create the log buffer
        private void initBuffer()
        {
            dbPath = GetDBPath();
            buffer = new List<String>();
            bufferSize = 0;
        }

        // initialize the database connection
        private void initDB()
        {
            try
            {
                string dbFile = GetDBPath() + Path.DirectorySeparatorChar + DB_NAME;
                bool exists = File.Exists(dbFile);

                db = new SQLiteConnection();
                db.ConnectionString = new SQLiteConnectionStringBuilder()
                {
                    {"Data Source", dbFile},
                    {"Version", "3"},
                    {"FailIfMissing", "False"}
                }.ConnectionString;
                db.Open();

                if (!exists)
                {
                    // create the table

                    string createString = "drop table if exists connection_log; create table connection_log(outbound bool, timestamp int, address varchar(39), size int, protocol varchar(10))";

                    SQLiteCommand command = new SQLiteCommand(createString, db);
                    command.ExecuteNonQuery();

                }
            }
            catch (Exception e)
            {
                PassThru.LogCenter.WriteErrorLog(e);
                db = null;
            }
        }

        // add a statement to the buffer, writing the buffer if necessary
        private void bufferStatement(string s)
        {
            lock (this) {
                if (bufferSize + 1 > BUFFER_MAX_SIZE)
                {
                    writeBuffer();
                    bufferSize = 0;
                }
                buffer.Add(s);
                ++bufferSize;
            }
        }

        // write the buffer to the database and then clear it
        private void writeBuffer()
        {
            lock (this)
            {
                try
                {

                    if (DO_CLEAR_LOG)
                    {
                        if (File.Exists(dbPath))
                            File.Delete(dbPath);
                        DO_CLEAR_LOG = false;
                    }

                    SQLiteTransaction transaction = db.BeginTransaction();
                    SQLiteCommand command = null;
                    foreach (String s in buffer)
                    {
                        command = new SQLiteCommand(s, db);
                        command.ExecuteNonQuery();
                    }
                    transaction.Commit();
                    buffer.Clear();
                    bufferSize = 0;
                }
                catch (Exception e)
                {
                    PassThru.LogCenter.Instance.Push(MODULE_NAME, e.Message);
                }
            }
        }

        void Help()
        {
            MetaData.Name = "Bandwidth Analyzer";
            MetaData.Version = "1.1.0.0";
            MetaData.HelpString = "This module logs the source/destination of incoming and outgoing packets and allows for statistical and visual analysis.";
            MetaData.Description = "Performs analysis and generates reports on bandwidth usage";
            MetaData.Contact = "fourthidmension.stephen@gmail.com";
            MetaData.Author = "Stephen C. (fourthdimension)";
        }

        public override ModuleError ModuleStart()
        {
            initDB();
            ModuleError me = new ModuleError();
            me.errorType = ModuleErrorType.Success;
            return me;
        }

        public override ModuleError ModuleStop()
        {
            lock (this) {
                // write any entries that may still be in the buffer to the log file
                writeBuffer();

                if (db == null)
                {
                    try
                    {
                        db.Close();
                    }
                    catch (Exception e)
                    {
                        PassThru.LogCenter.WriteErrorLog(e);
                    }
                }

                ModuleError me = new ModuleError();
                me.errorType = ModuleErrorType.Success;
                return me;
            }
        }


        public override System.Windows.Forms.UserControl GetControl()
        {
            return new ConnectionGraphControl(this) { Dock = System.Windows.Forms.DockStyle.Fill };
        }



        public override PacketMainReturn interiorMain(ref Packet in_packet)
        {
            // let the packet through
            PacketMainReturn pmr = new PacketMainReturn(MODULE_NAME);
            pmr.returnType = PacketMainReturnType.Allow;
            try
            {
                // get a usable form of the packet
                Protocol highestLayer = in_packet.GetHighestLayer();

                string type = "";
                bool outbound = false;
                uint length = 0;
                string ip = "";

                switch (highestLayer)
                {
                    case Protocol.TCP:
                        TCPPacket tcpPacket = (TCPPacket)in_packet;
                        type = TCP;
                        outbound = tcpPacket.Outbound;
                        length = tcpPacket.Length();
                        ip = outbound ? tcpPacket.DestIP.ToString() : tcpPacket.SourceIP.ToString();
                        break;
                    case Protocol.UDP:
                        UDPPacket udpPacket = (UDPPacket)in_packet;
                        type = UDP;
                        length = udpPacket.Length();
                        ip = outbound ? udpPacket.DestIP.ToString() : udpPacket.SourceIP.ToString();
                        break;
                    case Protocol.ARP:
                        ARPPacket arpPacket = (ARPPacket)in_packet;
                        type = ARP;
                        outbound = arpPacket.Outbound;
                        length = arpPacket.Length();
                        ip = outbound ? arpPacket.ATargetIP.ToString() : arpPacket.ASenderIP.ToString();
                        break;
                    case Protocol.DHCP:
                        // no packet structure for this type
                        break;
                    case Protocol.DNS:
                        DNSPacket dnsPacket = (DNSPacket)in_packet;
                        type = DNS;
                        outbound = dnsPacket.Outbound;
                        length = dnsPacket.Length();
                        ip = outbound ? dnsPacket.DestIP.ToString() : dnsPacket.SourceIP.ToString();
                        break;
                    case Protocol.EEth:
                        EETHPacket eethPacket = (EETHPacket)in_packet;
                        type = EETH;
                        outbound = eethPacket.Outbound;
                        length = eethPacket.Length();
                        ip = NO_IP;
                        break;
                    case Protocol.Ethernet:
                        EthPacket ethPacket = (EthPacket)in_packet;
                        type = ETHERNET;
                        outbound = ethPacket.Outbound;
                        length = ethPacket.Length();
                        ip = NO_IP;
                        break;
                    case Protocol.ICMP:
                        ICMPPacket icmpPacket = (ICMPPacket)in_packet;
                        type = ICMP;
                        outbound = icmpPacket.Outbound;
                        length = icmpPacket.Length();
                        ip = ip = outbound ? icmpPacket.DestIP.ToString() : icmpPacket.SourceIP.ToString();
                        break;
                    case Protocol.ICMPv6:
                        ICMPv6Packet icmpv6Packet = (ICMPv6Packet)in_packet;
                        type = ICMPV6;
                        outbound = icmpv6Packet.Outbound;
                        length = icmpv6Packet.Length();
                        ip = outbound ? icmpv6Packet.DestIP.ToString() : icmpv6Packet.SourceIP.ToString();
                        break;
                    case Protocol.IP:
                        IPPacket ipPacket = (IPPacket)in_packet;
                        type = IP;
                        outbound = ipPacket.Outbound;
                        length = ipPacket.Length();
                        ip = outbound ? ipPacket.DestIP.ToString() : ipPacket.SourceIP.ToString();
                        break;
                    case Protocol.SNMP:
                        // no packet structure available for this type
                        break;
                    default:
                        break;
                }

                if (type != "")
                {

                    bufferStatement("insert into connection_log values (" + (outbound ? DIRECTION_OUTBOUND : DIRECTION_INBOUND) + "," + 
                        DateTime.Now.Ticks + ",'" + ip + "'," + length + ",'" + type + "')"); // using Length here...
                                                                                // should TotalLength be used instead?
                }
                
            }
            catch (Exception e)
            {
                PassThru.LogCenter.Instance.Push(MODULE_NAME, e.Message);
            }

            return pmr;
        }

    }
}
