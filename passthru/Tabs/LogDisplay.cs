using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace PassThru.Tabs
{
    public partial class LogDisplay : UserControl
    {
        public LogDisplay()
        {
            InitializeComponent();
        }

        /*
         * Object handles logging of a log event to the window
         * @param le is the log event object to be logged
         */
        public void AddLogEvent(object le)
        {
            // if the logger is busy, invoke it
            if (textBox1.InvokeRequired)
            {
                System.Threading.ParameterizedThreadStart d = new System.Threading.ParameterizedThreadStart(AddLogEvent);
                textBox1.Invoke(d, new object[] { le });
            }
            // else log the message
            else
            {
                LogEvent e = (LogEvent)le;
                textBox1.Text = e.time.ToString() + " " + e.Module + ": " + e.Message + "\r\n" + textBox1.Text;
            }
        }

        // receives a log event and pushes it to AddLogEvent
        public void Instance_PushLogEvent(LogEvent e)
        {
            AddLogEvent(e);
        }
    }
}
