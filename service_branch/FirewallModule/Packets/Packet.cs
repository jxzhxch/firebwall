using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.Net.NetworkInformation;
using System.Net;

namespace FM
{
    //
    // INTERMEDIATE_BUFFER contains packet buffer, packet NDIS flags, WinpkFilter specific flags
    //
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct LIST_ENTRY
    {
        public IntPtr Flink;
        public IntPtr Blink;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public unsafe struct INTERMEDIATE_BUFFER
    {
        public LIST_ENTRY m_qLink;
        public uint m_dwDeviceFlags;
        public uint m_Length;
        public uint m_Flags;
        public fixed byte m_IBuffer[1514];
    }

    /// <summary>
    /// Different protocols that are supported or that will be
    /// </summary>
    public enum Protocol
    {
        EEth,
        Ethernet,
        IP,
        IPv6,
        ARP,
        TCP,
        UDP,
        ICMP,
        DNS,
        DHCP
    }

    /// <summary>
    /// Interface for a network Packet
    /// Packets are upward processing!
    /// </summary>
    public abstract unsafe class Packet
    {
        public const uint PACKET_FLAG_ON_RECEIVE = 0x00000002;
        public const uint PACKET_FLAG_ON_SEND = 0x00000001;

        public abstract bool ContainsLayer(Protocol layer);

        public abstract byte* Data();

        public abstract INTERMEDIATE_BUFFER* IB
        {
            get;
        }

        public abstract Protocol GetHighestLayer();

        public abstract uint Length();

        public abstract uint LayerStart();

        public abstract uint LayerLength();

        public abstract Packet MakeNextLayerPacket();

        public abstract bool CodeGenerated
        {
            get;
        }

        public abstract void ClearGeneratedPacket();

        public abstract bool Outbound
        {
            get;
            set;
        }

        // time a packet is captured.
        // should be logged with DateTime.UtcNow
        private DateTime packetTime;
        public DateTime PacketTime
        {
            get { return packetTime; }
            set { packetTime = value; }
        }
    }

    /// <summary>
    /// Encrypted ethernet packet
    /// (Not yet implemented)
    /// </summary>
    public unsafe class EETHPacket : EthPacket
    {
        public EETHPacket(EthPacket eth)
            : base(eth.data)
        {
        }

        public EETHPacket(INTERMEDIATE_BUFFER* in_packet)
            : base(in_packet)
        {
        }

        public override bool ContainsLayer(Protocol layer)
        {
            if (layer == Protocol.EEth)
                return true;
            else
                return base.ContainsLayer(layer);
        }

        public override Protocol GetHighestLayer()
        {
            return Protocol.EEth;
        }

        public override Packet MakeNextLayerPacket()
        {
            return this;
        }
    }

    /// <summary>
    /// ICMP packet obj
    /// </summary>
    public unsafe class ICMPPacket : IPPacket
    {
        // (g|s)et code
        public int Code
        {
            get
            {
                return (data->m_IBuffer[start + 1]);
            }
            set
            {
                data->m_IBuffer[start + 1] = (byte)value;
            }
        }

        // (g|s)et type
        public int Type
        {
            get
            {
                return (data->m_IBuffer[start]);
            }
            set
            {
                data->m_IBuffer[start] = (byte)value;
            }
        }

        // (g|s)et checksum
        public ushort Checksum
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

        // (g|s)et data
        public byte[] ICMPData
        {
            get
            {
                return GetICMPData();
            }
            set 
            {
                SetICMPData(value);
            }
        }

        // get icmp data
        private byte[] GetICMPData()
        {
            // data seg starts +8 bytes in
            uint dataStart = start + 8;
            // data seg is approx 32 bytes long
            uint dataEnd = dataStart + 32;
            
            byte[] d = new byte[dataEnd - dataStart];
            for ( uint i = dataStart; i < dataEnd; ++i )
                d[i - dataStart] = data->m_IBuffer[i];
            return d;
        }

        // set icmp data
        private void SetICMPData(byte[] arr)
        {
            // data seg starts +8 bytes in
            uint dataStart = start + 8;
            // data seg is approx 32 bytes long
            uint dataEnd = dataStart + 32;

            for (uint i = dataStart; i < arr.Length; ++i)
                data->m_IBuffer[i] = arr[i - dataStart];
        }

        // accepts intermediate buff, checks if ICMP
        public ICMPPacket(INTERMEDIATE_BUFFER* in_packet)
            : base(in_packet)
        {
            if (!isICMP())
                throw new Exception("Not an ICMP packet!");
            start = base.LayerStart() + base.LayerLength();
            
        }

        // accepts IPPacket, checks if ICMP
        public ICMPPacket(IPPacket eth)
            : base(eth.data)
        {
            if (!isICMP())
                throw new Exception("Not an ICMP packet!");
            start = base.LayerStart() + base.LayerLength();
        }

        // return if ICMP; else return to base layer to find layer
        public override bool ContainsLayer(Protocol layer)
        {
            if (layer == Protocol.ICMP)
                return true;
            else
                return base.ContainsLayer(layer);
        }

        // return ICMP (highest)
        public override Protocol GetHighestLayer()
        {
            return Protocol.ICMP;
        }

        // return this (highest)
        public override Packet MakeNextLayerPacket()
        {
            return this;
        }

        uint start = 0;

        public override uint LayerStart()
        {
            return start;
        }

        public override uint LayerLength()
        {
            return 8;
        }
    }

    /// <summary>
    /// ARP packet obj
    /// </summary>
    public unsafe class ARPPacket : EthPacket
    {
        public ARPPacket(EthPacket eth)
            : base(eth.data)
        {
            if (!isARP())
                throw new Exception("Not an ARP packet!");
            start = base.LayerStart() + base.LayerLength();
            length = this.Length() - base.LayerLength();
            if (eth.CodeGenerated)
            {
                HardwareType = 1;
                ProtocolType = 0x0800;
                HardwareSize = 0x06;
                ProtocolSize = 4;
                ARPOpcode = 0x0002;
            }
        }

        public ushort ARPOpcode
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

        public byte ProtocolSize
        {
            get
            {
                return data->m_IBuffer[start + 5];
            }
            set
            {
                data->m_IBuffer[start + 5] = value;
            }
        }

        public byte HardwareSize
        {
            get
            {
                return data->m_IBuffer[start + 4];
            }
            set
            {
                data->m_IBuffer[start + 4] = value;
            }
        }

        public ushort ProtocolType
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

        public ushort HardwareType
        {
            get
            {
                return (ushort)((data->m_IBuffer[start] << 8) | data->m_IBuffer[start + 1]);
            }
            set
            {
                data->m_IBuffer[start] = (byte)(value >> 8);
                data->m_IBuffer[start + 1] = (byte)(value & 0xff);
            }
        }

        public ARPPacket(INTERMEDIATE_BUFFER* in_packet)
            : base(in_packet)
        {
            if (!isARP())
                throw new Exception("Not an ARP packet!");
            start = base.LayerStart() + base.LayerLength();
            length = this.Length() - base.LayerLength();
        }

        public override bool ContainsLayer(Protocol layer)
        {
            if (layer == Protocol.ARP)
                return true;
            else
                return base.ContainsLayer(layer);
        }

        public override Protocol GetHighestLayer()
        {
            return Protocol.ARP;
        }

        public override Packet MakeNextLayerPacket()
        {
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

        public bool isRequest
        {
            get
            {
                return (data->m_IBuffer[start + 6] == 0x00 && data->m_IBuffer[start + 7] == 0x01);
            }
            set
            {
                if (value)
                {
                    data->m_IBuffer[start + 6] = 0x00;
                    data->m_IBuffer[start + 7] = 0x01;
                }
                else
                {
                    data->m_IBuffer[start + 6] = 0x00;
                    data->m_IBuffer[start + 7] = 0x02;
                }
            }
        }

        /// <summary>
        /// Returns sender's IP
        /// 
        /// This IP will be all zeros if it's an ARP probe
        /// </summary>
        public IPAddress ASenderIP
        {
            get
            {
                byte[] ip = new byte[4];
                for (int x = 0; x < 4; x++)
                    ip[x] = data->m_IBuffer[start + 0xe + x];
                return new IPAddress(ip);
            }
            set
            {
                byte[] ip = value.GetAddressBytes();
                for (int x = 0; x < 4; x++)
                    data->m_IBuffer[start + 0xe + x] = ip[x];
            }
        }

        public byte[] ASenderMac
        {
            get
            {
                byte[] ip = new byte[6];
                for (int x = 0; x < 6; x++)
                    ip[x] = data->m_IBuffer[start + 0x8 + x];
                return ip;
            }
            set
            {
                for (int x = 0; x < 6; x++)
                    data->m_IBuffer[start + 0x8 + x] = value[x];
            }
        }

        public IPAddress ATargetIP
        {
            get
            {
                byte[] ip = new byte[4];
                for (int x = 0; x < 4; x++)
                    ip[x] = data->m_IBuffer[start + 0x18 + x];
                return new IPAddress(ip);
            }
            set
            {
                byte[] ip = value.GetAddressBytes();
                for (int x = 0; x < 4; x++)
                    data->m_IBuffer[start + 0x18 + x] = ip[x];
            }
        }

        public byte[] ATargetMac
        {
            get
            {
                byte[] ip = new byte[6];
                for (int x = 0; x < 6; x++)
                    ip[x] = data->m_IBuffer[start + 0x12 + x];
                return ip;
            }
            set
            {
                for (int x = 0; x < 6; x++)
                    data->m_IBuffer[start + 0x12 + x] = value[x];
            }
        }
    }

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
                if (IPVersion == 0x4)
                {
                    byte[] ip = new byte[4];
                    for (int x = 0; x < 4; x++)
                    {
                        ip[x] = data->m_IBuffer[start + 0x10 + x];
                    }
                    return new IPAddress(ip);
                }
                else
                    return null;
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
                if (IPVersion == 0x4)
                {
                    byte[] ip = new byte[4];
                    for (int x = 0; x < 4; x++)
                    {
                        ip[x] = data->m_IBuffer[start + 0xc + x];
                    }
                    return new IPAddress(ip);
                }
                else
                    return null;
            }
        }
    }

    /// <summary>
    /// UDP Packet obj
    /// </summary>
    public unsafe class UDPPacket : IPPacket
    {
        public UDPPacket(INTERMEDIATE_BUFFER* in_packet)
            : base(in_packet)
        {
            if (!isUDP())
                throw new Exception("Not a UDP packet!");
            start = base.LayerStart() + base.LayerLength();
        }

        public UDPPacket(IPPacket eth)
            : base(eth.data)
        {
            if (!isUDP())
                throw new Exception("Not a UDP packet!");
            start = base.LayerStart() + base.LayerLength();
        }

        public override bool ContainsLayer(Protocol layer)
        {
            if (layer == Protocol.UDP)
                return true;
            else
                return base.ContainsLayer(layer);
        }

        public override Protocol GetHighestLayer()
        {
            return Protocol.UDP;
        }

        public override Packet MakeNextLayerPacket()
        {
            if (isDNS())
            {
                return new DNSPacket(data).MakeNextLayerPacket();
            }
            return this;
        }

        uint start = 0;

        public override uint LayerStart()
        {
            return start;
        }

        public override uint LayerLength()
        {
            return 8;
        }

        // check if the UDP packet has an empty header.
        // This is usually the case with port scans.
        public bool isEmpty()
        {
            return ((data->m_IBuffer[start + 8] << 8) == 0x00);
        }

        // check if the packet is a UDP DNS packet
        public bool isDNS()
        {
            return (SourcePort == 53 || DestPort == 53);
        }

        public ushort DestPort
        {
            get
            {
                return (ushort)((data->m_IBuffer[start + 2] << 8) + data->m_IBuffer[start + 3]);
            }
            set
            {
                data->m_IBuffer[start + 2] = (byte)(value >> 8);
                data->m_IBuffer[start + 3] = (byte)(value & 0xff);
            }
        }

        public ushort UDPChecksum
        {
            get
            {
                return (ushort)((data->m_IBuffer[start + 6] << 8) + data->m_IBuffer[start + 7]);
            }
            set
            {
                data->m_IBuffer[start + 6] = (byte)(value >> 8);
                data->m_IBuffer[start + 7] = (byte)(value & 0xff);
            }
        }

        public ushort UDPLength
        {
            get
            {
                return (ushort)((data->m_IBuffer[start + 4] << 8) + data->m_IBuffer[start + 5]);
            }
            set
            {
                data->m_IBuffer[start + 4] = (byte)(value >> 8);
                data->m_IBuffer[start + 5] = (byte)(value & 0xff);
            }
        }

        public ushort SourcePort
        {
            get
            {
                return (ushort)((data->m_IBuffer[start] << 8) + data->m_IBuffer[start + 1]);
            }
            set
            {
                data->m_IBuffer[start] = (byte)(value >> 8);
                data->m_IBuffer[start + 1] = (byte)(value & 0xff);
            }
        }
    }
}
