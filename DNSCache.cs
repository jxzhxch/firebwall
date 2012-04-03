using System;
using System.Collections.Generic;
using System.Text;
using FM;
using System.Net;

namespace DNSCache
{
    public class DNSCache : FirewallModule
    {
        public DNSCache() : base()
        {
            MetaData.Name = "DNSCache";
            MetaData.Author = "Brian W. (schizo)";
            MetaData.Contact = "nightstrike9809@gmail.com";
            MetaData.Description = "Caches your DNS query responses, so in the rare chance that DNS servers go down or get lagged, you will be mostly unaffected.";
            MetaData.HelpString = "";
            MetaData.Version = "0.1.0.0";
        }

        public SerializableDictionary<string, IPAddress> cache = new SerializableDictionary<string, IPAddress>();

        public SerializableDictionary<string, IPAddress> GetCache()
        {
            lock (cache)
            {
                return new SerializableDictionary<string, IPAddress>(cache);
            }
        }

        public void ClearCache()
        {
            lock (cache)
            {
                cache.Clear();
            }
        }

        public override System.Windows.Forms.UserControl GetControl()
        {
            return new DNSCacheUI(this) { Dock = System.Windows.Forms.DockStyle.Fill };
        }

        public override ModuleError ModuleStart()
        {
            lock (cache)
            {
                this.LoadConfig();
                if (this.PersistentData != null)
                {
                    cache = (SerializableDictionary<string, IPAddress>)PersistentData;
                }
                else
                    cache = new SerializableDictionary<string, IPAddress>();
                return new ModuleError() { errorType = ModuleErrorType.Success };
            }
        }

        public override ModuleError ModuleStop()
        {
            lock (cache)
            {
                PersistentData = cache;
                this.SaveConfig();
                return new ModuleError() { errorType = ModuleErrorType.Success };
            }
        }

        public event System.Threading.ThreadStart CacheUpdate;

        public override PacketMainReturn interiorMain(ref Packet in_packet)
        {
            if (in_packet.Outbound && in_packet.ContainsLayer(Protocol.DNS))
            {
                DNSPacket dns = (DNSPacket)in_packet;
                IPAddress answer;
                lock (cache)
                {
                    if (dns.Queries.Length > 0 && cache.TryGetValue(dns.Queries[0].ToString(), out answer))
                    {
                        DNSPacket.DNSAnswer[] answers = new DNSPacket.DNSAnswer[1];
                        DNSPacket.DNSAnswer ans = new DNSPacket.DNSAnswer();
                        ans.Name = new List<byte>();
                        ans.Name.Add(0xc0);
                        ans.Name.Add(0x0c);
                        ans.Class = 0x01;
                        ans.Type = 0x01;
                        ans.TTL = 0xffff;
                        ans.RDLength = 4;
                        ans.RData = answer.GetAddressBytes();
                        answers[0] = ans;
                        dns.Answers = answers;
                        dns.DNSFlags = 0x8180;
                        dns.UDPLength = (ushort)(8 + dns.LayerLength());
                        dns.TotalLength = (ushort)(20 + dns.UDPLength);
                        dns.UDPChecksum = dns.GenerateUDPChecksum;
                        dns.Outbound = false;
                        IPAddress temp = dns.SourceIP;
                        dns.SourceIP = dns.DestIP;
                        dns.DestIP = temp;
                        ushort t = dns.SourcePort;
                        dns.SourcePort = dns.DestPort;
                        dns.DestPort = t;
                        dns.HeaderChecksum = dns.GenerateIPChecksum;
                        dns.UDPChecksum = dns.GenerateUDPChecksum;
                        return new PacketMainReturn("DNSCache") { returnType = PacketMainReturnType.Edited };
                    }
                }
            }
            else if (!in_packet.Outbound && in_packet.ContainsLayer(Protocol.DNS))
            {
                lock (cache)
                {
                    DNSPacket dns = (DNSPacket)in_packet;
                    foreach (DNSPacket.DNSAnswer ans in dns.Answers)
                    {
                        if (ans.RDLength == 4 && ans.Type == 0x01 && ans.Class == 0x01)
                        {
                            cache[dns.Queries[0].ToString()] = new IPAddress(ans.RData);
                            break;
                        }                        
                    }
                }
                if (CacheUpdate != null)
                    CacheUpdate();                
            }
            return null;
        }
    }
}
