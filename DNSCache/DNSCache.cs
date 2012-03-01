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
        }

        public override ModuleError ModuleStart()
        {
            return new ModuleError() { errorType = ModuleErrorType.Success };
        }

        public override ModuleError ModuleStop()
        {
            return new ModuleError() { errorType = ModuleErrorType.Success };
        }

        public override PacketMainReturn interiorMain(ref Packet in_packet)
        {
            if (in_packet.Outbound && in_packet.ContainsLayer(Protocol.DNS))
            {
                DNSPacket dns = (DNSPacket)in_packet;
                bool hasPoisoned = false;
                foreach(DNSPacket.DNSQuestion q in dns.Queries)
                {
                    if (q.ToString().Contains("reddit"))
                    {
                        DNSPacket.DNSAnswer a = new DNSPacket.DNSAnswer();
                        a.Name = new List<byte>();
                        a.Name.Add(0xc0);
                        a.Name.Add(0x0c);
                        a.Type = 0x0001;
                        a.Class = 0x0001;
                        a.TTL = 0xff;
                        a.RDLength = 4;
                        a.RData = IPAddress.Parse("66.172.10.29").GetAddressBytes();
                        DNSPacket.DNSAnswer[] ans = new DNSPacket.DNSAnswer[1];
                        ans[0] = a;
                        dns.Answers = ans;
                        dns.DNSFlags = 0x8180;
                        hasPoisoned = true;
                        dns.UDPLength = (ushort)(8 + dns.LayerLength());
                        dns.TotalLength = (ushort)(20 + dns.UDPLength);
                        dns.UDPChecksum = dns.GenerateUDPChecksum;
                        break;
                    }
                }
                if (hasPoisoned)
                {
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
            return null;
        }
    }
}
