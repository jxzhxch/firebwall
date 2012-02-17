using System;
using System.Collections.Generic;
using System.Text;
using System.Net;

namespace FM
{
    /// <summary>
    /// IPv6 packet obj
    /// </summary>
    public unsafe class IPv6Packet : EthPacket
    {
        public IPv6Packet(EthPacket eth)
            : base(eth.data)
        {
            if (!isIPv6())
                throw new Exception("Not an IPv6 packet!");
            start = base.LayerStart() + base.LayerLength();
            length = (uint)((data->m_IBuffer[start] & 0xf) * 4);
        }

        public IPv6Packet(INTERMEDIATE_BUFFER* in_packet)
            : base(in_packet)
        {
            if (!isIPv6())
                throw new Exception("Not an IPv6 packet!");
            start = base.LayerStart() + base.LayerLength();
            length = (uint)40;
        }

        public override bool ContainsLayer(Protocol layer)
        {
            if (layer == Protocol.IPv6)
                return true;
            else
                return base.ContainsLayer(layer);
        }

        public override Protocol GetHighestLayer()
        {
            return Protocol.IPv6;
        }

        public override Packet MakeNextLayerPacket()
        {
            return this;
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
            return (data->m_IBuffer[start + 0x6] == 0x06);
        }

        public bool isUDP()
        {
            return (data->m_IBuffer[start + 0x6] == 0x11);
        }

        public bool isICMP()
        {
            return (data->m_IBuffer[start + 0x6] == 0x01);
        }

        public UInt32 VersionClassLabel
        {
            get
            {
                return (ushort)((data->m_IBuffer[start] << 24) | (data->m_IBuffer[start + 1] << 16) | (data->m_IBuffer[start + 2] << 8) | data->m_IBuffer[start + 3]);
            }
            set
            {
                data->m_IBuffer[start] = (byte)(value >> 24);
                data->m_IBuffer[start + 1] = (byte)(value >> 16);
                data->m_IBuffer[start + 2] = (byte)(value >> 8);
                data->m_IBuffer[start + 3] = (byte)(value & 0xff);
            }
        }

        public byte NextHeader
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

        public byte HopLimit
        {
            get { return data->m_IBuffer[start + 7]; }
            set { data->m_IBuffer[start + 7] = value; }
        }

        public ushort PayloadLength
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

        public IPAddress DestIP
        {
            get
            {
                byte[] ip = new byte[16];
                for (int x = 0; x < 16; x++)
                {
                    ip[x] = data->m_IBuffer[start + 0x18 + x];
                }
                return new IPAddress(ip);
            }
            set
            {
                byte[] ip = value.GetAddressBytes();
                for (int x = 0; x < 16; x++)
                    data->m_IBuffer[start + 0x18 + x] = ip[x];
            }
        }

        public byte IPVersion
        {
            get
            {
                return 6;
            }
        }

        public IPAddress SourceIP
        {
            get
            {
                byte[] ip = new byte[16];
                for (int x = 0; x < 16; x++)
                {
                    ip[x] = data->m_IBuffer[start + 0x8 + x];
                }
                return new IPAddress(ip);
            }
            set
            {
                byte[] ip = value.GetAddressBytes();
                for (int x = 0; x < 16; x++)
                    data->m_IBuffer[start + 0x8 + x] = ip[x];
            }
        }
    }
}
