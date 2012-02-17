using System;
using System.Collections.Generic;
using System.Text;
using FM;

namespace PassThru.Modules
{
    /*
     * Ethernet Encryption Module
     * @summary
     */
    public class EthEncryption : FirewallModule
    {
        public EthEncryption(NetworkAdapter adapter)
            : base(adapter)
        {
        }

        public override ModuleError ModuleStart()
        {
            throw new NotImplementedException();
        }

        public override ModuleError ModuleStop()
        {
            throw new NotImplementedException();
        }

        public override PacketMainReturn interiorMain(ref Packet in_packet)
        {
            throw new NotImplementedException();
        }
    }
}
