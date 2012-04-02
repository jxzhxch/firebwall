using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.ComponentModel;

namespace PassThru
{
    public static class ColorScheme
    {
        public static void SetColorScheme(Control control)
        {
            if (control is Form || control is UserControl || control is MainWindow || control is AdapterControl || control is AdapterDisplay)
            {
                control.BackColor = Color.Black;
                control.ForeColor = Color.White;
            }
            else if (control is Button)
            {
                if (((Button)control).FlatStyle == FlatStyle.Flat)
                {
                    control.BackColor = Color.Black;
                    control.ForeColor = Color.White;
                }
                else
                {
                    control.BackColor = Color.FromArgb(64, 32, 16);
                    control.ForeColor = Color.White;
                }
            }
            else
            {
                control.BackColor = Color.FromArgb(16, 0, 0);
                control.ForeColor = Color.White;
            }
            foreach (Control c in control.Controls)
                SetColorScheme(c);
        }
    }
}
