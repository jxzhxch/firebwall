using System;
using System.Collections.Generic;
using System.Text;
using System.Net;

namespace FM
{
    /// <summary>
    /// IPPacket obj
    /// </summary>
    public unsafe class IPPacket : EthPacket
    {
        public IPPacket(EthPacket eth)
            : base(eth.data)
        {
            if (!isIP())
                throw new Exception("Not an IP packet!");
            start = base.LayerStart() + base.LayerLength();
            length = (uint)((data->m_IBuffer[start] & 0xf) * 4);
        }

        public byte TTL
        {
            get
            {
                return data->m_IBuffer[start + 8];
            }
            set
            {
                data->m_IBuffer[start + 8] = value;
            }
        }

        public byte NextProtocol
        {
            get
            {
                return data->m_IBuffer[start + 9];
            }
            set
            {
                data->m_IBuffer[start + 9] = value;
            }
        }

        public ushort HeaderChecksum
        {
            get
            {
                return (ushort)((data->m_IBuffer[start + 10] << 8) | data->m_IBuffer[start + 11]);
            }
            set
            {
                data->m_IBuffer[start + 10] = (byte)(value >> 8);
                data->m_IBuffer[start + 11] = (byte)(value & 0xff);
            }
        }

        public byte Flags
        {
            get
            {
                return data->m_IBuffer[start + 6];
            }
            set
            {
                data->m_IBuffer[start + 6] = value;
            }
        }

        public ushort FragmentOffset
        {
            get
            {
                return (ushort)((data->m_IBuffer[start + 6] << 8) | data->m_IBuffer[start + 7]);
            }
            set
            {
                data->m_IBuffer[start + 6] = (byte)(value >> 8);
                data->m_IBuffer[start + 7] = (byte)(value & 0xff);
            }
        }

        public ushort Identification
        {
            get
            {
                return (ushort)((data->m_IBuffer[start + 4] << 8) | data->m_IBuffer[start + 5]);
            }
            set
            {
                data->m_IBuffer[start + 4] = (byte)(value >> 8);
                data->m_IBuffer[start + 5] = (byte)(value & 0xff);
            }
        }

        public ushort TotalLength
        {
            get
            {
                return (ushort)((data->m_IBuffer[start + 2] << 8) | data->m_IBuffer[start + 3]);
            }
            set
            {
                data->m_IBuffer[start + 2] = (byte)(value >> 8);
                data->m_IBuffer[start + 3] = (byte)(value & 0xff);
            }
        }

        // generate the checksum of the IP packet
        public ushort GenerateIPChecksum
        {
            get
            {
                return GetIPChecksum();
            }
        }

        // adaptation from http://www.netfor2.com/ipsum.htm
        private ushort GetIPChecksum()
        {
            UInt32 sum = 0;

            for (uint i = this.start; i < this.start + 10; i += 2)
            {
                sum += (UInt32)(data->m_IBuffer[i] << 8) | data->m_IBuffer[i + 1];
            }
            for (uint i = this.start + 12; i < this.start + this.length; i += 2)
            {
                sum += (UInt32)(data->m_IBuffer[i] << 8) | data->m_IBuffer[i + 1];
            }            

            // 1's compliment
            while ((sum >> 16) != 0)
                sum = ((sum & 0xFFFF) + (sum >> 16));

            sum = ~sum;

            return (ushort)sum;
        }

        public byte DiffServicesField
        {
            get
            {
                return data->m_IBuffer[start + 1];
            }
            set
            {
                data->m_IBuffer[start + 1] = value;
            }
        }

        public byte VersionAndLength
        {
            get
            {
                return data->m_IBuffer[start];
            }
            set
            {
                data->m_IBuffer[start] = value;
            }
        }

        public IPPacket(INTERMEDIATE_BUFFER* in_packet)
            : base(in_packet)
        {
            if (!isIP())
                throw new Exception("Not an IP packet!");
            start = base.LayerStart() + base.LayerLength();
            length = (uint)((data->m_IBuffer[start] & 0xf) * 4);
        }

        public override bool ContainsLayer(Protocol layer)
        {
            if (layer == Protocol.IP)
                return true;
            else
                return base.ContainsLayer(layer);
        }

        public override Protocol GetHighestLayer()
        {
            return Protocol.IP;
        }

        public override Packet MakeNextLayerPacket()
        {
            if (isTCP())
            {
                return new TCPPacket(data).MakeNextLayerPacket();
            }
            else if (isUDP())
                return new UDPPacket(data).MakeNextLayerPacket();
            else if (isICMP())
                return new ICMPPacket(data).MakeNextLayerPacket();
            else
                return this;
        }

        uint start = 0;

        public override uint LayerStart()
        {
            return start;
        }

        uint length = 0;

        public override uint LayerLength()
        {
            return length;
        }

        public bool isTCP()
        {
            return (data->m_IBuffer[start + 0x9] == 0x06);
        }

        public bool isUDP()
        {
            return (data->m_IBuffer[start + 0x9] == 0x11);
        }

        public bool isICMP()
        {
            return (data->m_IBuffer[start + 0x9] == 0x01);
        }

        public IPAddress DestIP
        {
            get
            {
                byte[] ip = new byte[4];
                for (int x = 0; x < 4; x++)
                {
                    ip[x] = data->m_IBuffer[start + 0x10 + x];
                }
                return new IPAddress(ip);
            }
            set
            {
                byte[] ip = value.GetAddressBytes();
                for (int x = 0; x < 4; x++)
                {
                    data->m_IBuffer[start + 0x10 + x] = ip[x];
                }
            }
        }

        public byte IPVersion
        {
            get
            {
                return (byte)(data->m_IBuffer[start] >> 4);
            }
        }

        public IPAddress SourceIP
        {
            get
            {
                byte[] ip = new byte[4];
                for (int x = 0; x < 4; x++)
                {
                    ip[x] = data->m_IBuffer[start + 0xc + x];
                }
                return new IPAddress(ip);
            }
            set
            {
                byte[] ip = value.GetAddressBytes();
                for (int x = 0; x < 4; x++)
                {
                    data->m_IBuffer[start + 0xc + x] = ip[x];
                }
            }
        }
    }
}
