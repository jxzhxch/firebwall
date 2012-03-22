﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Reflection;
using System.IO;

namespace PassThru
{
    /// <summary>
    /// Class to represent the tray icon and manage whats displayed from it
    /// </summary>
	public class TrayIcon
    {
        static bool showPopups;

        // var updated whenever options checkbox changes
        public static bool displayTrayLogs
        {
            get { return showPopups; }
            set { showPopups = value; SaveConfig(); }
        }

        /// <summary>
        /// Makes the actual NotifyIcon, and hooks up all the events for it
        /// </summary>
		public TrayIcon() 
        {                
			ContextMenu cm = new ContextMenu();
			MenuItem closeButton = new MenuItem("Exit", new EventHandler(Program.Close));
			cm.MenuItems.Add(closeButton);
			tray = new NotifyIcon();
			tray.ContextMenu = cm;
            Assembly target = Assembly.GetExecutingAssembly();
            tray.Icon = new System.Drawing.Icon(target.GetManifestResourceStream("PassThru.Resources.newTray.ico"));
			tray.Visible = true;
			tray.BalloonTipClosed += new EventHandler(tray_BalloonTipClosed);
			tray.DoubleClick += new EventHandler(tray_DoubleClick);
            LoadConfig();
		}
		NotifyIcon tray;

        public static void SaveConfig()
        {
            string folder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            folder = folder + Path.DirectorySeparatorChar + "firebwall";
            if (!Directory.Exists(folder))
                Directory.CreateDirectory(folder);
            string file = folder + Path.DirectorySeparatorChar + "tray.cfg";
            File.WriteAllText(file, displayTrayLogs.ToString());
        }

        public static void LoadConfig()
        {
            string folder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            folder = folder + Path.DirectorySeparatorChar + "firebwall";
            if (!Directory.Exists(folder))
                Directory.CreateDirectory(folder);
            string file = folder + Path.DirectorySeparatorChar + "tray.cfg";
            showPopups = true;
            if (File.Exists(file))
            {
                string config = File.ReadAllText(file);
                if (config == "False")
                    showPopups = false;
            }
        }

        /// <summary>
        /// Queue of lines to display in the pop up balloon
        /// </summary>
        Queue<string> lines = new Queue<string>();

        /// <summary>
        /// Adds a line to the display queue
        /// </summary>
        /// <param name="line"></param>
		public void AddLine(string line) 
        {
            // only display if checked
            if (displayTrayLogs)
            {
                tray.BalloonTipText = line;
                tray.ShowBalloonTip(5000);
            }
		}

        /// <summary>
        /// Disposes of the tray
        /// </summary>
		public void Dispose() 
        {
			tray.Dispose();
		}

        /// <summary>
        /// Clears the balloon tip when it closes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
		void tray_BalloonTipClosed(object sender, EventArgs e) 
        {
			lines.Clear();
			tray.BalloonTipText = "";
		}

        /// <summary>
        /// Controls the main window with double clicks on the tray icon
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
		void tray_DoubleClick(object sender, EventArgs e) 
        {
			if (Program.mainWindow.Visible)
					Program.mainWindow.Visible = false;
			else
					Program.mainWindow.Visible = true;

			Program.mainWindow.Activate();
		}
	}
}