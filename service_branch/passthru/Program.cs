using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.IO;
using System.Threading;
using System.Net;
using NdisapiSpace;
using Win32APISPace;
using FM;

namespace PassThru
{    
	class Program
    {        
		public static MainWindow mainWindow;
        public static UpdateChecker uc = new UpdateChecker();
		static bool Running = true;
        static DataTransport dt;

        /// <summary>
        /// Makes sure to close everything properly as the whole thing closes
        /// </summary>
        /// <param name="o"></param>
        /// <param name="ea"></param>
		static void Close(object o, EventArgs ea) 
        {
			NetworkAdapter.ShutdownAll();
            mainWindow.Close();
			mainWindow.Exit();
            uc.Close();
			Running = false;
            dt.SendMessage(DataTransport.StaticTransportType.TrayIcon, null);
            dt.SendMessage(DataTransport.StaticTransportType.LogCenter, null);
            //LogCenter.Kill();
		}

        /// <summary>
        /// Entry point for the application
        /// </summary>
        /// <param name="args"></param>            
		static void Main(string[] args) 
        {
            dt = new DataTransport(DataTransport.StaticTransportType.Program);
            dt.NewStaticMessage += new DataTransport.StaticMessage(dt_NewStaticMessage);
            dt.NewMessage += new DataTransport.Message(dt_NewMessage);
            DataHub.Instance.RegisterStaticTransport(DataTransport.StaticTransportType.Program, dt);
            //tray = new TrayIcon();
            uc.Updater();
            mainWindow = new MainWindow();
            foreach (NetworkAdapter ni in NetworkAdapter.GetAllAdapters())
            {
                ni.StartProcessing();
            }
            Application.Run(mainWindow);
            while (Running)
                Thread.Sleep(100);
		}

        static void dt_NewMessage(Guid from, object data)
        {
            
        }

        static void dt_NewStaticMessage(DataTransport.StaticTransportType from, object data)
        {
            if (from == DataTransport.StaticTransportType.TrayIcon)
            {
                Close(null, null);
            }
        }
	}
}
