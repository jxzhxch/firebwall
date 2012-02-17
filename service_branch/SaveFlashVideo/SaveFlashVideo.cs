using System;
using System.Collections.Generic;
using System.Text;
using FM;
using System.IO;
using System.Threading;

namespace SaveFlashVideo
{
    public class SaveFlashVideo : FirewallModule
    {
        public SaveFlashVideo()
            : base()
        {
            MetaData.Name = "Save Flash Video";
            MetaData.Version = "0.0.0.1";
            MetaData.HelpString = "Video streaming services like Youtube stream flv files over the network.  They do not allow you to just download them like a normal video, but you can sniff them from the network.  This module does just that.  If it sees a flash video being transferred over the network, it saves it to a file.";
            MetaData.Description = "Dumps Flash Video streams to file";
            MetaData.Contact = "nightstrike9809@gmail.com";
            MetaData.Author = "Brian W. (schizo)";
        }

        public override ModuleError ModuleStart()
        {
            this.Enabled = true;
            WriteThread = new Thread(WriteLoop);
            WriteThread.Start();
            return new ModuleError() { errorType = ModuleErrorType.Success };
        }

        public override ModuleError ModuleStop()
        {
            WriteThread.Abort();
            return new ModuleError() { errorType = ModuleErrorType.Success };
        }

        Dictionary<Quad, ulong> openFiles = new Dictionary<Quad, ulong>();
        Dictionary<Quad, Queue<byte[]>> writeQueue = new Dictionary<Quad, Queue<byte[]>>();
        readonly object padlock = new object();
        Dictionary<Quad, UInt32> lastSeq = new Dictionary<Quad, uint>();
        Thread WriteThread;

        void WriteLoop()
        {
            Dictionary<Quad, Queue<byte[]>> tempQueue;
            string folder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            folder = folder + Path.DirectorySeparatorChar + "firebwall";
            folder = folder + Path.DirectorySeparatorChar + "modules" + Path.DirectorySeparatorChar;
            while (true)
            {                
                lock (padlock)
                {
                    tempQueue = new Dictionary<Quad, Queue<byte[]>>(writeQueue);
                    writeQueue.Clear();
                }
                foreach (KeyValuePair<Quad, Queue<byte[]>> kvp in tempQueue)
                {
                    if (kvp.Value.Count != 0)
                    {                       
                        FileStream bin = new FileStream(folder + kvp.Key.GetHashCode().ToString() + ".flv", FileMode.Append);
                        while (kvp.Value.Count != 0)
                        {
                            byte[] data = kvp.Value.Dequeue();
                            bin.Write(data, 0, data.Length);
                        }
                        bin.Close();
                    }
                }
                Thread.Sleep(500);
            }
        }

        public override PacketMainReturn interiorMain(ref Packet in_packet)
        {
            if (in_packet.ContainsLayer(Protocol.TCP) && !in_packet.Outbound)
            {
                TCPPacket tcp = (TCPPacket)in_packet;
                if (tcp.SourcePort == 80 && tcp.ACK)
                {
                    Quad q = new Quad() { dstIP = tcp.DestIP, dstPort = tcp.DestPort, srcIP = tcp.SourceIP, srcPort = tcp.SourcePort };
                    if (openFiles.ContainsKey(q))
                    {
                        if (true || openFiles[q] != 0)
                        {
                            //continue writing then update the openFiles dictionary
                            byte[] data = tcp.GetApplicationLayer_safe();
                            if (data.Length != 0 && lastSeq[q] != tcp.SequenceNumber)
                            {
                                lock (padlock)
                                {
                                    if (!writeQueue.ContainsKey(q))
                                        writeQueue[q] = new Queue<byte[]>();
                                    writeQueue[q].Enqueue(data);
                                }
                                openFiles[q] = openFiles[q] - (ulong)data.Length;
                            }
                            lastSeq[q] = tcp.SequenceNumber;
                        }
                        else
                        {
                            openFiles.Remove(q);
                        }
                    }
                    else
                    {
                        byte[] data = tcp.GetApplicationLayer_safe();
                        string str = ASCIIEncoding.ASCII.GetString(data);
                        if (str.Contains("video/x-flv"))
                        {
                            int t = str.IndexOf("Content-Length: ");
                            if (t > 0)
                            {
                                str = str.Substring(t + "Content-Length: ".Length);
                                ulong length = ulong.Parse(str.Substring(0, str.IndexOf("\r\n")));
                                if (length > 0)
                                {
                                    openFiles.Add(q, length);
                                    lastSeq[q] = tcp.SequenceNumber;
                                }
                            }
                        }
                    }
                }
            }
            return null;
        }
    }
}
