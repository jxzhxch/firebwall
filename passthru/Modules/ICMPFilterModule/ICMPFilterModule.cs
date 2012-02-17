using System;
using System.Collections.Generic;
using System.Text;
using FM;

namespace PassThru
{
    /// <summary>
    /// ICMP filter module
    /// 
    /// Allows filtering by type/code
    /// </summary>
    public class ICMPFilterModule : FirewallModule
    {
        public ICMPFilterModule()
            : base()
        {
            Help();
        }

        // constructor 
        public ICMPFilterModule(NetworkAdapter adapter)
            : base(adapter)
        {
            Help();
        }

        // return local user control
        public override System.Windows.Forms.UserControl GetControl()
        {
            return new ICMPFilterDisplay(this) { Dock = System.Windows.Forms.DockStyle.Fill };
        }

        // Action for ModuleStart
        public override ModuleError ModuleStart()
        {
            LoadConfig();
            if (PersistentData == null)
                data = new ICMPData();
            else
                data = (ICMPData)PersistentData;

            ModuleError moduleError = new ModuleError();
            moduleError.errorType = ModuleErrorType.Success;
            return moduleError;
        }

        // Action for ModuleStop
        public override ModuleError ModuleStop()
        {
            if (!data.Save)
                data.RuleTable = new SerializableDictionary<string, List<string>>();

            PersistentData = data;
            SaveConfig();
            ModuleError moduleError = new ModuleError();
            moduleError.errorType = ModuleErrorType.Success;
            return moduleError;
        }

        /// <summary>
        /// Object used to serialize the data we need to persist
        /// </summary>
        [Serializable]
        public class ICMPData
        {
            private SerializableDictionary<string, List<string>> ruleTable = new SerializableDictionary<string, List<string>>();
            public SerializableDictionary<string, List<string>> RuleTable 
                        { get { return ruleTable; } set { ruleTable = new SerializableDictionary<string,List<string>>(value); } }

            private bool denyAll = false;
            public bool DenyAll 
                        { get { return denyAll; } set { denyAll = value; } }

            public bool Save = true;
        }

        public ICMPData data;
        
        // main routine
        public override PacketMainReturn interiorMain(ref Packet in_packet)
        {
            // if the packet is ICMP
            if (in_packet.GetHighestLayer() == Protocol.ICMP)
            {
                ICMPPacket packet = (ICMPPacket)in_packet;
                // check if the packet is allowed and deny all is false
                if (isAllowed(packet.Type.ToString(), packet.Code.ToString()) && 
                    !data.DenyAll)
                {
                    return null;
                }
                // else, log and drop it
                else
                {
                    PacketMainReturn pmr;
                    pmr = new PacketMainReturn("ICMPFilter Module");
                    pmr.returnType = PacketMainReturnType.Drop | PacketMainReturnType.Log;
                    pmr.logMessage = "ICMP from " + packet.SourceIP.ToString() + " for " +
                        packet.DestIP.ToString() + " was dropped.";
                    return pmr;
                }
            }
            return null;
        }

        /*
         * Method used to check whether an ICMP packet should be
         * allowed through.
         * 
         * @param type is the ICMP type
         * @param code is the ICMP code
         */
        private bool isAllowed(string type, string code)
        {
            bool isAllowed = true;

            // if the table contains the type, check if it
            // also contains the code
            if (data.RuleTable.ContainsKey(type))
            {
                List<string> temp;
                data.RuleTable.TryGetValue(type, out temp);
                // invert logic; if found, disallow, if not, allow
                isAllowed = !(temp.Contains(code));
            }
            return isAllowed;
        }

        // module metadata
        private void Help()
        {
            MetaData.Name = "ICMP Filter";
            MetaData.Version = "1.0.0.0";
            MetaData.Description = "Blocks ICMP packets of a given type/code";
            MetaData.Contact = "shodivine@gmail.com";
            MetaData.Author = "Bryan A. (drone)";
            MetaData.HelpString = "This module can be used to block all (or particular) ICMP packets from flowing in or out of your network."
                + "ICMP packets are used to exchange error messages between networked computers, but it's also used with tools such as nmap "
                + "and nessus to gain valuable information about systems behind a gateway.\n\n"
                + "ICMP Segment Structure\n "
                + "\t|——————————————————————————————|\n"
                + "\t|  BITS  |  0-7  |  8-15  |  16-23  |  24-31 |\n"
                + "\t|——————————————————————————————|\n"
                + "\t|    0   |  Type |  Code  |  Checksum       |\n"
                + "\t|   32   |      Rest of Header                   |\n"
                + "\t|——————————————————————————————|\n"
                + "A full list of supported control messages can be found on the module page (View ICMP).";
        }
    }
}