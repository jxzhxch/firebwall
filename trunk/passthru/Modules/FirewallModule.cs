﻿using System;
using System.Collections.Generic;
using System.Net;
using System.Net.NetworkInformation;
using System.IO;

namespace PassThru
{
	[Flags]
	public enum PacketMainReturnType
	{
		Error,          //Reports an error in the packet processing
		Drop,           //Drops the packet
		Allow,          //Allows the packet to be passed on to the next module
		SendOutPacket,  //Requires out packet to be sent
		Log	            //Logs the packet
	}

	public class PacketMainReturn
    {
		/// <summary>
		/// Creates a PacketMainReturn with the basic unknown error message
		/// </summary>
		/// <param name="moduleName"></param>
		public PacketMainReturn(string moduleName) 
        {
			Module = moduleName;
			returnType = PacketMainReturnType.Error | PacketMainReturnType.Log;
			logMessage = "An error has occurred in " + moduleName + " with no other details.";
		}
        public PacketMainReturn(string moduleName, Exception e)
        {
            Module = moduleName;
            returnType = PacketMainReturnType.Error | PacketMainReturnType.Log;
            logMessage = "An error has occurred in " + moduleName + ". " + e.Message + "\r\n" + e.StackTrace;
        }
		public string Module = null;
		public byte[] SendPacket = null;
		public string logMessage = null;
		public PacketMainReturnType returnType;
	}

	public enum ModuleErrorType
	{
		Success,        //No error
		UnknownError    //I'm not sure what type of errors it'll run into yet
	}

	public class ModuleError
    {
		public byte[] errorBinary = null;
		public string errorMessage = null;
		public ModuleErrorType errorType;
		public string moduleName = null;
	}

	/// <summary>
	/// An abstract class for the firewall modules, making input and output uniform
	/// </summary>
	public abstract class FirewallModule
    {
		public FirewallModule(NetworkAdapter adapter) 
        {
			this.adapter = adapter;
		}
		public NetworkAdapter adapter = null;
		public System.Windows.Forms.UserControl uiControl = null;
		public string moduleName = null;

        public virtual System.Windows.Forms.UserControl GetControl()
        {
            return null;
        }

		/// <summary>
		/// Ran after the module is loaded, to prime it for processing if required
		/// </summary>
		/// <returns>Any error that occured during the starting of it</returns>
		public abstract ModuleError ModuleStart();

		/// <summary>
		/// Ran when the module is to be stopped, to clear up any uneeded resources
		/// </summary>
		/// <returns>Any error that occured during the stopping of it</returns>
		public abstract ModuleError ModuleStop();

		/// <summary>
		/// The wrapper function for processing packets
		/// </summary>
		/// <param name="in_packet">Packet to be processed</param>
		/// <returns>A PacketMainReturn object, either from the interiorMain or default error one</returns>
		public PacketMainReturn PacketMain(Packet in_packet) {
			try
			{
				PacketMainReturn pmr = interiorMain(in_packet);
				return pmr;
			}
			catch (Exception e)
			{
				return new PacketMainReturn(moduleName, e);
			}
		}

		/// <summary>
		/// The internal function for processing packets implemented by the module
		/// </summary>
		/// <param name="in_packet">Packet to be processed</param>
		/// <returns>PacketMainReturn object describing what to do with the packet and/or
		/// anything that is notable during the processing</returns>
		public abstract PacketMainReturn interiorMain(Packet in_packet);
	}

	public class Quad
    {
		public IPAddress dstIP = null;
		public int dstPort = -1;
		public IPAddress srcIP = null;
		public int srcPort = -1;

		public override bool Equals(object obj) 
        {
			Quad other = (Quad)obj;
			return (srcIP == other.srcIP && srcPort == other.srcPort && 
                    dstIP == other.dstIP && dstPort == other.dstPort) || 
                    (srcIP == other.dstIP && srcPort == other.dstPort && 
                    dstIP == other.srcIP && dstPort == other.srcPort);
		}

		public override int GetHashCode() 
        {
			return srcIP.GetHashCode() + dstIP.GetHashCode();
		}
	}
}
