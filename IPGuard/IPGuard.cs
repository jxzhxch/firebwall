using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.IO;
using FM;
using System.Text.RegularExpressions;

/// Class mimics the behavior of tools like PeerBlock.  Give it some lists
/// and it'll block all outgoing (or incoming) requests from the IP addresses.
namespace PassThru
{
    public class IPGuard : FirewallModule
    {
        // all the blocked ranges
        private List<IPAddressRange> block_ranges = new List<IPAddressRange>();

        // list of the available lists to be loaded
        private List<String> available_block_lists;
        public List<String> Available_Block_Lists
                { get { return available_block_lists; } set { available_block_lists = new List<String>(value); } }

        private IPGuardUI guardUI;

        public IPGuard() : base()
        {
            Help();
        }

        public IPGuard(INetworkAdapter adapt)
            : base(adapt)
        {
            Help();
        }
        
        /// <summary>
        /// Get me a UI!
        /// </summary>
        /// <returns>A UI!</returns>
        public override System.Windows.Forms.UserControl GetControl()
        {
            // generate the UI and load the available lists
            guardUI = new IPGuardUI(this) { Dock = System.Windows.Forms.DockStyle.Fill };
            loadLists();
            return guardUI;
        }
        
        /// <summary>
        /// Start the mod, initialize available_block_lists
        /// </summary>
        /// <returns></returns>
        public override ModuleError ModuleStart()
        {
            available_block_lists = new List<String>();
            return new ModuleError() { errorType = ModuleErrorType.Success };
        }

        /// <summary>
        /// Stop the mod
        /// </summary>
        /// <returns></returns>
        public override ModuleError ModuleStop()
        {
            return new ModuleError() { errorType = ModuleErrorType.Success };
        }

        /// <summary>
        /// Main
        /// </summary>
        /// <param name="in_packet"></param>
        /// <returns></returns>
        public override PacketMainReturn interiorMain(ref Packet in_packet)
        {
            PacketMainReturn pmr;
            if (in_packet.ContainsLayer(Protocol.TCP))
            {
                // cast the packet and check for SYN/outbound
                TCPPacket packet = (TCPPacket)in_packet;
                if (packet.SYN && packet.Outbound)
                {
                    // check if it's blocked
                    for (int i = 0; i < block_ranges.Count; ++i)
                    {
                        // if its heading towards a blacklisted IP
                        if (block_ranges[i].IsInRange(packet.DestIP))
                        {
                            pmr = new PacketMainReturn("IPGuard");
                            // check if we should log it
                            if (guardUI.isLog())
                            {
                                pmr.returnType = PacketMainReturnType.Drop | PacketMainReturnType.Log;
                                pmr.logMessage = "IPGuard has blocked outgoing packets to " + packet.DestIP;
                            }
                            else
                                pmr.returnType = PacketMainReturnType.Drop;
                            return pmr;
                        }
                    }
                }
            }
            return null;
        }

        /// <summary>
        /// Load all the text files in /firebwall/modules/IPGuard
        /// </summary>
        private void loadLists()
        {
            // do all the pathing shit
            string folder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            folder = folder + Path.DirectorySeparatorChar + "firebwall";
            if (!Directory.Exists(folder))
                Directory.CreateDirectory(folder);
            folder = folder + Path.DirectorySeparatorChar + "modules" + Path.DirectorySeparatorChar + "IPGuard";
            if (!Directory.Exists(folder))
                Directory.CreateDirectory(folder);
            string filepath = folder;

            // get all the txt files in here
            string[] files = Directory.GetFiles(filepath, "*.txt");

            // add them to the list of available lists and update the UI
            foreach (string s in files)
            {
                // if the list isn't available and isn't already loaded
                if ( !(available_block_lists.Contains(s)) && 
                     !(guardUI.isLoaded(s)))
                    available_block_lists.Add(s);
            }
            guardUI.available();
        }
        
        /// <summary>
        /// reads in the block list and builds the obj.  Hopefully the user is smart and either
        /// downloaded the list from the de facto location, or has it formatted properly.
        /// Documented that in the help string.
        /// </summary>
        public void buildRanges(String file)
        {
            // open the file
            try
            {
                using (StreamReader sr = new StreamReader(file))
                {
                    String line;
                    
                    while ((line = sr.ReadLine()) != null)
                    {
                        // PARSINGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGG
                        if (!line.StartsWith("#") && line.Length > 2)
                        {
                            string addrs = line.Substring(
                                line.LastIndexOf(":")+1, (line.Length - line.LastIndexOf(":")-1));
                            block_ranges.Add(new IPAddressRange(IPAddress.Parse(addrs.Substring(0, addrs.IndexOf("-"))),
                                                                IPAddress.Parse(addrs.Substring(addrs.IndexOf("-")+1, (addrs.Length - addrs.IndexOf("-")-1)))));
                        }
                    }
                }
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e.Message);
            }
        }

        // utility: use this if you wanna see what's goin' on in your blocks
        public void TELL_ME_WHATS_IN_THERE()
        {
            System.Diagnostics.Debug.WriteLine("OKAY CAPTAIN HERE IT IS");
            foreach (IPAddressRange i in block_ranges)
            {
                System.Diagnostics.Debug.WriteLine(i.getLower() + " - " + i.getUpper());
            }
        }
        
        /// <summary>
        /// method used to rebuild the blocked IP ranges when lists are removed.  
        /// there's really no 'quick' way to discern which block matches to which list
        /// aside from either a) dumping the entire thing when a list is removed and rebuilding
        /// it or b) using a hash mapping of file -> list of ranges, but then that makes 
        /// iterating through the block ranges slower. 
        /// </summary>
        public void rebuild()
        {
            // get all the loaded lists
            List<Object> tmp = guardUI.getLoadedLists();
            // clear out the current list
            block_ranges.Clear();
            // REBULID IT
            foreach (Object s in tmp)
                buildRanges(s.ToString());
        }

        // block lists are in ranges; use this to quickly discover
        // if a given IP is within the blocked range
        private class IPAddressRange
        {
            private AddressFamily addressFamily;
            private byte[] lowerBytes;
            private byte[] upperBytes;

            public IPAddressRange(IPAddress lower, IPAddress upper)
            {
                this.addressFamily = lower.AddressFamily;
                this.lowerBytes = lower.GetAddressBytes();
                this.upperBytes = upper.GetAddressBytes();
            }

            /// <summary>
            /// Check if an IP address falls within the given range
            /// </summary>
            /// <param name="addr">address to check</param>
            /// <returns>true/false</returns>
            public bool IsInRange(IPAddress addr)
            {
                // if it's not in the same addr
                if (addr.AddressFamily != this.addressFamily)
                {
                    return false;
                }

                byte[] addrBytes = addr.GetAddressBytes();

                bool lBound = true;
                bool uBound = true;

                // iterate over ip bytes in the range
                for (int i = 0; i < this.lowerBytes.Length &&
                        (lBound || uBound); ++i)
                {
                    if ((lBound && addrBytes[i] < lowerBytes[i]) ||
                        (uBound && addrBytes[i] > upperBytes[i]))
                    {
                        return false;
                    }

                    lBound &= (addrBytes[i] == lowerBytes[i]);
                    uBound &= (addrBytes[i] == upperBytes[i]);
                }
                return true;
            }

            /// <summary>
            /// Return the lower IP addr
            /// </summary>
            /// <returns>IPAddress</returns>
            public IPAddress getLower()
            {
                return new IPAddress(lowerBytes);
            }

            /// <summary>
            /// Return the upper IP addr
            /// </summary>
            /// <returns>IPAddress</returns>
            public IPAddress getUpper()
            {
                return new IPAddress(upperBytes);
            }
        }

        /// <summary>
        /// metadata 
        /// </summary>
        private void Help()
        {
            MetaData.Name = "IPGuard";
            MetaData.Author = "Bryan A.";
            MetaData.Contact = "shodivine@gmail.com";
            MetaData.Description = "Blocks IPs from given lists.";
            MetaData.Version = "0.0.0.1";
            MetaData.HelpString = "None at this time.";
        }
    }
}
