using System;
using System.Collections.Generic;
using System.Text;
using FM;

namespace PassThru
{
    /*
     * Dump data to pcap module
     * @summary
     */
    public class DumpToPcapModule : FirewallModule
    {
        public DumpToPcapModule(NetworkAdapter adapter)
            : base(adapter)
        {
        }
        PcapFileWriter file;

        public override ModuleError ModuleStart()
        {
            string f = "pcaptest-" + PcapCreator.Instance.GetNewDate() + ".pcap";
            file = new PcapFileWriter(f);
            ModuleError me = new ModuleError();
            me.errorType = ModuleErrorType.Success;
            return me;
        }

        public override ModuleError ModuleStop()
        {
            file.Close();
            ModuleError me = new ModuleError();
            me.errorType = ModuleErrorType.Success;
            return me;
        }

        public override unsafe PacketMainReturn interiorMain(ref Packet in_packet)
        {
            byte[] buffer = new byte[in_packet.Length()];
            byte* data = in_packet.Data();
            for (int x = 0; x < in_packet.Length(); x++)
            {
                buffer[x] = data[x];
            }
            file.AddPacket(data, (int)in_packet.Length());
            PacketMainReturn pmr = new PacketMainReturn("Dump to Pcap");
            pmr.returnType = PacketMainReturnType.Allow;
            return pmr;
        }
    }
}
