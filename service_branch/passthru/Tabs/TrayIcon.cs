using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Reflection;
using FM;

namespace PassThru
{
    /// <summary>
    /// Class to represent the tray icon and manage whats displayed from it
    /// </summary>
	public class TrayIcon
    {
        // var updated whenever options checkbox changes
        public static bool displayTrayLogs;

        static DataTransport dt;

        static void Close(object o, EventArgs ea)
        {
            dt.SendMessage(DataTransport.StaticTransportType.Program, new object());
        }

        /// <summary>
        /// Makes the actual NotifyIcon, and hooks up all the events for it
        /// </summary>
		public TrayIcon() 
        {
            dt = new DataTransport(DataTransport.StaticTransportType.TrayIcon);
            dt.NewMessage += new DataTransport.Message(dt_NewMessage);
            dt.NewStaticMessage += new DataTransport.StaticMessage(dt_NewStaticMessage);
            DataHub.Instance.RegisterStaticTransport(DataTransport.StaticTransportType.TrayIcon, dt);
			ContextMenu cm = new ContextMenu();
			MenuItem closeButton = new MenuItem("Exit", new EventHandler(Close));
			cm.MenuItems.Add(closeButton);
			tray = new NotifyIcon();
			tray.ContextMenu = cm;
            Assembly target = Assembly.GetExecutingAssembly();
            tray.Icon = new System.Drawing.Icon(target.GetManifestResourceStream("PassThru.Resources.newTray.ico"));
			tray.Visible = true;
			tray.BalloonTipClosed += new EventHandler(tray_BalloonTipClosed);
			tray.DoubleClick += new EventHandler(tray_DoubleClick);
            displayTrayLogs = true;
		}

        void dt_NewStaticMessage(DataTransport.StaticTransportType from, object data)
        {
            if (from == DataTransport.StaticTransportType.Program)
            {
                this.Dispose();
            }
        }

        void dt_NewMessage(Guid from, object data)
        {
            
        }

		NotifyIcon tray;

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
            //if (lines.Count == 5)
            //{
            //    lines.Dequeue();
            //}
            //lines.Enqueue(line);

            //if (lines.Count > 0)
            //{
            //    for (int x = lines.Count - 1; x >= 0; x--)
            //    {
            //        tray.BalloonTipText += lines.ToArray()[x] + "\r\n";
            //    }                
            //}
		}

        /// <summary>
        /// Disposes of the tray
        /// </summary>
		void Dispose() 
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
