/*************************************************************************/
/*				Copyright (c) 2000-2010 NT Kernel Resources.		     */
/*                           All Rights Reserved.                        */
/*                          http://www.ntkernel.com                      */
/*                           ndisrd@ntkernel.com                         */
/*                                                                       */
/* Module Name:  PassThru main module                                    */
/*                                                                       */
/* Abstract: Defines the entry point for the console application         */
/*                                                                       */
/*************************************************************************/

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

namespace PassThru
{    
	class Program
    {        
		public static MainWindow mainWindow;
        public static UpdateChecker uc = new UpdateChecker();
		static bool Running = true;

        /// <summary>
        /// Makes sure to close everything properly as the whole thing closes
        /// </summary>
        /// <param name="o"></param>
        /// <param name="ea"></param>
		public static void Close(object o, EventArgs ea) 
        {
			NetworkAdapter.ShutdownAll();
            mainWindow.Close();
			mainWindow.Exit();
            uc.Close();
			Running = false;
			LogCenter.ti.Dispose();
            LogCenter.Kill();
		}

        /// <summary>
        /// Entry point for the application
        /// </summary>
        /// <param name="args"></param>            
		static void Main(string[] args) 
        {                
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
	}
}
