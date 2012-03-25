using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.NetworkInformation;
using System.IO;
using FM;

namespace PassThru
{
    /// <summary>
    /// Basic firewall module
    /// </summary>
    public class BasicFirewall : FirewallModule
    {
        public BasicFirewall(NetworkAdapter adapter)
            : base(adapter)
        {
            MetaData.Name = "Basic Firewall";
            MetaData.Version = "1.0.0.0";
            MetaData.HelpString = "Computers on networks often communicate using protocols that have ports.  TCP and UDP are the most common protocols.  You will see TCP being used very often, as it allows for stable and reliable connections.  UDP is less reliable and is used a bit less.  Both of these use ports to communicate.  Certain ports are used for certain things.  Some you may want open so you can share files or remotely control you computer, but in most cases, you want them closed."
                + "\r\n\r\nThis module uses rules to Allow or Drop packets depending on what port or ip they are for.  It is the one part that is in about every firewall."
                + "\r\n\r\nThis module works based on ordered rules.  The rules are displayed from top to bottom, and the order can be changed by clicking and dragging the rule.  Rules can be added with the Add Rule button, and removed with the Remove Rule button."
                + "\n\nArguments are designated on a rule-by-rule basis.  Some have required arguments, others do not.  If the arguments box is greyed out, "
                + "that particular rule has no arguments.  Otherwise, the required arguments will be denoted by the line of text above the arguments box.";
            MetaData.Description = "Blocks or allows packets based on IP/Port";
            MetaData.Contact = "nightstrike9809@gmail.com";
            MetaData.Author = "Brian W. (schizo)";
        }

        public enum PacketStatus
        {
            UNDETERMINED,
            BLOCKED,
            ALLOWED
        }

        public enum Direction
        {
            IN = 1,
            OUT = 1 << 1
        }

        public interface Rule
        {
            PacketStatus GetStatus(Packet pkt);
            string ToFileString();
            string GetLogMessage();
            string String
            {
                get;
            }
        }

        public enum RuleType
        {
            UDPALL,
            UDPPORT,
            TCPIPPORT,
            TCPPORT,
            TCPALL,
            IP,
            ALL
        }

        public class RuleFactory
        {
            public static Rule MakeRule(RuleType ruleType, PacketStatus ps, Direction dir, string args, bool log)
            {
                switch (ruleType)
                {
                    case RuleType.IP:
                        return new IPRule(ps, IPAddress.Parse(args), dir, log);
                    case RuleType.TCPALL:
                        return new TCPAllRule(ps, dir, log);
                    case RuleType.TCPIPPORT:
                        return new TCPIPPortRule(ps, IPAddress.Parse(args.Split(' ')[0]), int.Parse(args.Split(' ')[1]), dir, log);
                    case RuleType.TCPPORT:
                        return new TCPPortRule(ps, int.Parse(args), dir, log);
                    case RuleType.UDPALL:
                        return new UDPAllRule(ps, dir, log);
                    case RuleType.UDPPORT:
                        return new UDPPortRule(ps, int.Parse(args), dir, log);
                    case RuleType.ALL:
                        return new AllRule(ps, dir, log);
                }
                return null;
            }
        }

        [Serializable]
        public class IPRule : Rule
        {
            PacketStatus ps;
            Direction direction;
            bool log = true;
            IPAddress ip;
            string message = "";

            public IPRule(PacketStatus ps, IPAddress ip, Direction direction, bool log)
            {
                this.ps = ps;
                this.direction = direction;
                this.ip = ip;
                this.log = log;
            }

            public PacketStatus GetStatus(Packet pkt)
            {
                if (pkt.ContainsLayer(Protocol.IP))
                {
                    IPPacket tcppkt = (IPPacket)pkt;
                    if (pkt.Outbound && (direction & Direction.OUT) == Direction.OUT)
                    {
                        if (tcppkt.DestIP == ip)
                        {
                            if (log)
                                message = " IP packet from " + tcppkt.SourceIP.ToString() + " to " + tcppkt.DestIP.ToString();
                            return ps;
                        }
                    }
                    else if (!pkt.Outbound && (direction & Direction.IN) == Direction.IN)
                    {
                        if (tcppkt.DestIP == ip)
                        {
                            if (log)
                                message = " IP packet from " + tcppkt.SourceIP.ToString() + " to " + tcppkt.DestIP.ToString();
                            return ps;
                        }
                    }                 
                }
                return PacketStatus.UNDETERMINED;
            }

            public string ToFileString()
            {
                return null;
            }

            public string GetLogMessage()
            {
                if (!log) return null;
                if (ps == PacketStatus.ALLOWED)
                {
                    return "Allowed " + message;
                }
                return "Blocked " + message;
            }

            public string String
            {
                get { return ToString(); }
            }

            public override string ToString()
            {
                string ret = "";
                if (ps == PacketStatus.ALLOWED)
                {
                    ret = "Allows";
                }
                else
                {
                    ret = "Blocks";
                }
                ret += " IP " + ip.ToString(); ;
                if (direction == (Direction.IN | Direction.OUT))
                {
                    ret += " in and out";
                }
                else if (direction == Direction.OUT)
                {
                    ret += " out";
                }
                else if (direction == Direction.IN)
                {
                    ret += " in";
                }
                if (log)
                    ret += " and logs";
                return ret;
            }
        }

        [Serializable]
        public class AllRule : Rule
        {
            PacketStatus ps;
            Direction direction;
            bool log = true;

            public AllRule(PacketStatus ps, Direction direction, bool log)
            {
                this.ps = ps;
                this.direction = direction;
                this.log = log;
            }

            public PacketStatus GetStatus(Packet pkt)
            {
                if (pkt.Outbound && (direction & Direction.OUT) == Direction.OUT)
                {
                    if (log)
                        message = " packet";
                    return ps;
                }
                else if (!pkt.Outbound && (direction & Direction.IN) == Direction.IN)
                {
                    if (log)
                        message = " packet";
                    return ps;
                }
                return PacketStatus.UNDETERMINED;
            }

            string message = null;
            public string GetLogMessage()
            {
                if (!log) return null;
                if (ps == PacketStatus.ALLOWED)
                {
                    return "Allowed " + message;
                }
                return "Blocked " + message;
            }

            public string String
            {
                get { return ToString(); }
            }

            public override string ToString()
            {
                string ret = "";
                if (ps == PacketStatus.ALLOWED)
                {
                    ret = "Allows";
                }
                else
                {
                    ret = "Blocks";
                }
                ret += " all";
                if (direction == (Direction.IN | Direction.OUT))
                {
                    ret += " in and out";
                }
                else if(direction == Direction.OUT)
                {
                    ret += " out";
                }
                else if (direction == Direction.IN)
                {
                    ret += " in";
                }
                if (log)
                    ret += " and logs";
                return ret;
            }

            public string ToFileString()
            {
                return null;
            }
        }

        [Serializable]
        public class UDPAllRule : Rule
        {
            PacketStatus ps;
            Direction direction;
            bool log = true;

            public UDPAllRule(PacketStatus ps, Direction direction, bool log)
            {
                this.ps = ps;
                this.direction = direction;
                this.log = log;
            }

            public PacketStatus GetStatus(Packet pkt)
            {
                if (pkt.ContainsLayer(Protocol.UDP))
                {
                    UDPPacket udppkt = (UDPPacket)pkt;
                    if (pkt.Outbound && (direction & Direction.OUT) == Direction.OUT)
                    {
                        if (log)
                            message = " UDP packet from " + udppkt.SourceIP.ToString() + ":" + 
                                udppkt.SourcePort.ToString() + " to " + udppkt.DestIP.ToString() + 
                                ":" + udppkt.DestPort.ToString();
                        return ps;
                    }
                    else if (!pkt.Outbound && (direction & Direction.IN) == Direction.IN)
                    {
                        if (log)
                            message = " UDP packet from " + udppkt.SourceIP.ToString() + ":" + 
                                udppkt.SourcePort.ToString() + " to " + udppkt.DestIP.ToString() + ":" + udppkt.DestPort.ToString();
                        return ps;
                    }
                }
                return PacketStatus.UNDETERMINED;
            }

            string message = null;
            public string GetLogMessage()
            {
                if (!log) return null;
                if (ps == PacketStatus.ALLOWED)
                {
                    return "Allowed " + message;
                }
                return "Blocked " + message;
            }

            public string String
            {
                get { return ToString(); }
            }

            public override string ToString()
            {
                string ret = "";
                if (ps == PacketStatus.ALLOWED)
                {
                    ret = "Allows";
                }
                else
                {
                    ret = "Blocks";
                }
                ret += " all UDP ports";
                if (direction == (Direction.IN | Direction.OUT))
                {
                    ret += " in and out";
                }
                else if(direction == Direction.OUT)
                {
                    ret += " out";
                }
                else if (direction == Direction.IN)
                {
                    ret += " in";
                }
                if (log)
                    ret += " and logs";
                return ret;
            }

            public string ToFileString()
            {
                return null;
            }
        }

        [Serializable]
        public class UDPPortRule : Rule
        {
            PacketStatus ps;
            Direction direction;
            int port;
            bool log = true;

            public override string ToString()
            {
                string ret = "";
                if (ps == PacketStatus.ALLOWED)
                {
                    ret = "Allows";
                }
                else
                {
                    ret = "Blocks";
                }
                ret += " UDP port " + port.ToString();
                if (direction == (Direction.IN | Direction.OUT))
                {
                    ret += " in and out";
                }
                else if (direction == Direction.OUT)
                {
                    ret += " out";
                }
                else if (direction == Direction.IN)
                {
                    ret += " in";
                }
                if (log)
                    ret += " and logs";
                return ret;
            }

            public UDPPortRule(PacketStatus ps, int port, Direction direction, bool log)
            {
                this.ps = ps;
                this.direction = direction;
                this.port = port;
                this.log = log;
            }

            public PacketStatus GetStatus(Packet pkt)
            {
                if (pkt.ContainsLayer(Protocol.UDP))
                {
                    UDPPacket udppkt = (UDPPacket)pkt;
                    if (pkt.Outbound && (direction & Direction.OUT) == Direction.OUT)
                    {
                        if (udppkt.DestPort == port)
                        {
                            if (log)
                                message = " UDP packet from " + udppkt.SourceIP.ToString() + 
                                    ":" + udppkt.SourcePort.ToString() + " to " + udppkt.DestIP.ToString() + 
                                    ":" + udppkt.DestPort.ToString();
                            return ps;
                        }
                    }
                    else if (!pkt.Outbound && (direction & Direction.IN) == Direction.IN)
                    {
                        if (udppkt.DestPort == port)
                        {
                            if (log)
                                message = " UDP packet from " + udppkt.SourceIP.ToString() + 
                                    ":" + udppkt.SourcePort.ToString() + " to " + udppkt.DestIP.ToString() + 
                                    ":" + udppkt.DestPort.ToString();
                            return ps;
                        }
                    }
                }
                return PacketStatus.UNDETERMINED;
            }

            public string String
            {
                get { return ToString(); }
            }

            string message = null;
            public string GetLogMessage()
            {
                if (!log)
                    return null;
                if (ps == PacketStatus.ALLOWED)
                {
                    return "Allowed " + message;
                }
                return "Blocked " + message;
            }

            public string ToFileString()
            {
                return null;
            }
        }

        [Serializable]
        public class TCPIPPortRule : Rule
        {
            PacketStatus ps;
            Direction direction;
            int port;
            IPAddress ip;
            bool log = true;

            public override string ToString()
            {
                string ret = "";
                if (ps == PacketStatus.ALLOWED)
                {
                    ret = "Allows";
                }
                else
                {
                    ret = "Blocks";
                }
                ret += " TCP ip:port " + ip.ToString() + ":" + port.ToString();
                if (direction == (Direction.IN | Direction.OUT))
                {
                    ret += " in and out";
                }
                else if (direction == Direction.OUT)
                {
                    ret += " out";
                }
                else if (direction == Direction.IN)
                {
                    ret += " in";
                }
                if (log)
                    ret += " and logs";
                return ret;
            }

            public TCPIPPortRule(PacketStatus ps, IPAddress ip, int port, Direction direction, bool log)
            {
                this.ps = ps;
                this.direction = direction;
                this.port = port;
                this.ip = ip;
                this.log = log;
            }

            public PacketStatus GetStatus(Packet pkt)
            {
                if (pkt.ContainsLayer(Protocol.TCP))
                {
                    TCPPacket tcppkt = (TCPPacket)pkt;
                    if (tcppkt.SYN && !(tcppkt.ACK))
                    {
                        if (pkt.Outbound && (direction & Direction.OUT) == Direction.OUT)
                        {
                            if ((tcppkt.DestPort == port) && (tcppkt.DestIP == ip))
                            {
                                if (log)
                                    message = " TCP packet from " + tcppkt.SourceIP.ToString() + ":" +
                                        tcppkt.SourcePort.ToString() + " to " + tcppkt.DestIP.ToString() +
                                        ":" + tcppkt.DestPort.ToString();
                                return ps;
                            }
                        }
                        else if (!pkt.Outbound && (direction & Direction.IN) == Direction.IN)
                        {
                            if ((tcppkt.DestPort == port) && (tcppkt.DestIP == ip))
                            {
                                if (log)
                                    message = " TCP packet from " + tcppkt.SourceIP.ToString() +
                                        ":" + tcppkt.SourcePort.ToString() + " to " + tcppkt.DestIP.ToString() +
                                        ":" + tcppkt.DestPort.ToString();
                                return ps;
                            }
                        }
                    }
                }
                return PacketStatus.UNDETERMINED;
            }

            public string String
            {
                get { return ToString(); }
            }

            string message = null;
            public string GetLogMessage()
            {
                if (!log)
                    return null;
                if (ps == PacketStatus.ALLOWED)
                {
                    return "Allowed " + message;
                }
                return "Blocked " + message;
            }

            public string ToFileString()
            {
                return null;
            }
        }

        [Serializable]
        public class TCPPortRule : Rule
        {
            PacketStatus ps;
            Direction direction;
            int port;
            bool log = true;

            public override string ToString()
            {
                string ret = "";
                if (ps == PacketStatus.ALLOWED)
                {
                    ret = "Allows";
                }
                else
                {
                    ret = "Blocks";
                }
                ret += " TCP port " + port.ToString();
                if (direction == (Direction.IN | Direction.OUT))
                {
                    ret += " in and out";
                }
                else if (direction == Direction.OUT)
                {
                    ret += " out";
                }
                else if (direction == Direction.IN)
                {
                    ret += " in";
                }
                if (log)
                    ret += " and logs";
                return ret;
            }

            public TCPPortRule(PacketStatus ps, int port, Direction direction, bool log)
            {
                this.ps = ps;
                this.direction = direction;
                this.port = port;
                this.log = log;
            }

            public PacketStatus GetStatus(Packet pkt)
            {
                if (pkt.ContainsLayer(Protocol.TCP))
                {
                    TCPPacket tcppkt = (TCPPacket)pkt;
                    if (tcppkt.SYN && !(tcppkt.ACK))
                    {
                        if (pkt.Outbound && (direction & Direction.OUT) == Direction.OUT)
                        {
                            if (tcppkt.DestPort == port)
                            {
                                if (log)
                                    message = " TCP packet from " + tcppkt.SourceIP.ToString() +
                                        ":" + tcppkt.SourcePort.ToString() + " to " +
                                        tcppkt.DestIP.ToString() + ":" + tcppkt.DestPort.ToString();
                                return ps;
                            }
                        }
                        else if (!pkt.Outbound && (direction & Direction.IN) == Direction.IN)
                        {
                            if (tcppkt.DestPort == port)
                            {
                                if (log)
                                    message = " TCP packet from " + tcppkt.SourceIP.ToString() +
                                        ":" + tcppkt.SourcePort.ToString() + " to " + tcppkt.DestIP.ToString() +
                                        ":" + tcppkt.DestPort.ToString();
                                return ps;
                            }
                        }
                    }
                }
                return PacketStatus.UNDETERMINED;
            }

            public string String
            {
                get { return ToString(); }
            }

            string message = null;
            public string GetLogMessage()
            {
                if (!log)
                    return null;
                if (ps == PacketStatus.ALLOWED)
                {
                    return "Allowed " + message;
                }
                return "Blocked " + message;
            }

            public string ToFileString()
            {
                return null;
            }
        }

        [Serializable]
        public class TCPAllRule : Rule
        {
            PacketStatus ps;
            Direction direction;
            bool log = true;

            public string String
            {
                get { return ToString(); }
            }

            public override string ToString()
            {
                string ret = "";
                if (ps == PacketStatus.ALLOWED)
                {
                    ret = "Allows";
                }
                else
                {
                    ret = "Blocks";
                }
                ret += " all TCP ports";
                if (direction == (Direction.IN | Direction.OUT))
                {
                    ret += " in and out";
                }
                else if (direction == Direction.OUT)
                {
                    ret += " out";
                }
                else if (direction == Direction.IN)
                {
                    ret += " in";
                }
                if (log)
                    ret += " and logs";
                return ret;
            }

            public TCPAllRule(PacketStatus ps, Direction direction, bool log)
            {
                this.ps = ps;
                this.direction = direction;
                this.log = log;
            }

            public PacketStatus GetStatus(Packet pkt)
            {
                if (pkt.ContainsLayer(Protocol.TCP))
                {
                    TCPPacket tcppkt = (TCPPacket)pkt;
                    if (tcppkt.SYN && !(tcppkt.ACK))
                    {
                        if (pkt.Outbound && (direction & Direction.OUT) == Direction.OUT)
                        {
                            if (log)
                                message = " TCP packet from " + tcppkt.SourceIP.ToString() + ":" +
                                          tcppkt.SourcePort.ToString() + " to " + tcppkt.DestIP.ToString() +
                                          ":" + tcppkt.DestPort.ToString();
                            return ps;
                        }
                        else if (!pkt.Outbound && (direction & Direction.IN) == Direction.IN)
                        {
                            if (log)
                                message = " TCP packet from " + tcppkt.SourceIP.ToString() + ":" +
                                    tcppkt.SourcePort.ToString() + " to " + tcppkt.DestIP.ToString() + ":" +
                                    tcppkt.DestPort.ToString();
                            return ps;
                        }
                    }
                }
                return PacketStatus.UNDETERMINED;
            }

            string message = null;
            public string GetLogMessage()
            {
                if (!log)
                    return null;
                if (ps == PacketStatus.ALLOWED)
                {
                    return "Allowed " + message;
                }
                return "Blocked " + message;
            }

            public string ToFileString()
            {
                return null;
            }
        }

        readonly object padlock = new object();
        public List<Rule> rules = new List<Rule>();

        public override ModuleError ModuleStart()
        {
            LoadConfig();
            lock (padlock)
            {
                if (PersistentData == null)
                {
                    rules.Add(new TCPAllRule(PacketStatus.BLOCKED, Direction.IN, true));
                }
                else
                {
                    rules = (List<Rule>)PersistentData;
                }
            }

            ModuleError me = new ModuleError();
            me.errorType = ModuleErrorType.Success;
            return me;
        }

        public override System.Windows.Forms.UserControl GetControl()
        {
            return new BasicFirewallControl(this) { Dock = System.Windows.Forms.DockStyle.Fill };
        }

        public override ModuleError ModuleStop()
        {
            PersistentData = rules;
            SaveConfig();
            ModuleError me = new ModuleError();
            me.errorType = ModuleErrorType.Success;
            return me;
        }

        public override PacketMainReturn interiorMain(ref Packet in_packet)
        {
            lock (padlock)
            {
                PacketStatus status = PacketStatus.UNDETERMINED;
                foreach (Rule r in rules)
                {
                    status = r.GetStatus(in_packet);
                    if (status == PacketStatus.BLOCKED)
                    {
                        PacketMainReturn pmr = new PacketMainReturn("Basic Firewall");
                        pmr.returnType = PacketMainReturnType.Drop;
                        if (r.GetLogMessage() != null)
                        {
                            pmr.returnType |= PacketMainReturnType.Log;
                            pmr.logMessage = r.GetLogMessage();
                        }
                        return pmr;
                    }
                    else if (status == PacketStatus.ALLOWED)
                    {
                        return null;
                    }
                }
            }
            return null;
        }

        public void InstanceGetRuleUpdates(List<Rule> r)
        {
            lock (padlock)
            {
                rules = new List<Rule>(r);
            }
        }

        Quad MakeQuad(Packet in_packet)
        {
            if (in_packet.ContainsLayer(Protocol.TCP))
            {
                TCPPacket tcp = (TCPPacket)in_packet;
                Quad q = new Quad
                {
                    dstIP = tcp.DestIP,
                    dstPort = tcp.DestPort,
                    srcPort = tcp.SourcePort,
                    srcIP = tcp.SourceIP
                };
                return q;
            }
            return null;
        }
    }
}
