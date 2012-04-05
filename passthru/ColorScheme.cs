using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.ComponentModel;
using FM;

namespace PassThru
{
    public static class ColorScheme
    {
        public static SerializableDictionary<string, Color> currentTheme = null;
        public static SerializableDictionary<string, SerializableDictionary<string, Color>> themes = new SerializableDictionary<string, SerializableDictionary<string, Color>>();

        public static void LoadThemes()
        {
            //"mordor"
            SerializableDictionary<string, Color> mordorTheme = new SerializableDictionary<string, Color>();
            mordorTheme["FlatButtonBack"] = Color.Black;
            mordorTheme["FlatButtonFore"] = Color.White;
            mordorTheme["ButtonBack"] = Color.FromArgb(64, 0, 0);
            mordorTheme["ButtonFore"] = Color.White;
            mordorTheme["GridColor"] = Color.Black;
            mordorTheme["GridForeColor"] = Color.White;
            mordorTheme["GridBackColor"] = Color.FromArgb(64, 0, 0);
            mordorTheme["GridHeaderFore"] = Color.White;
            mordorTheme["GridHeaderBack"] = Color.FromArgb(64, 32, 16);
            mordorTheme["GridCellFore"] = Color.White;
            mordorTheme["GridCellBack"] = Color.FromArgb(64, 0, 0);
            mordorTheme["GridSelectCellBack"] = Color.FromArgb(128, 0, 0);
            mordorTheme["GridSelectCellFore"] = Color.White;
            mordorTheme["Back"] = Color.FromArgb(16, 0, 0);
            mordorTheme["Fore"] = Color.White;

            SerializableDictionary<string, Color> lightTheme = new SerializableDictionary<string, Color>();
            lightTheme["FlatButtonBack"] = Color.Black;
            lightTheme["FlatButtonFore"] = Color.White;
            lightTheme["ButtonBack"] = Color.WhiteSmoke;
            lightTheme["ButtonFore"] = Color.DarkBlue;
            lightTheme["GridColor"] = Color.WhiteSmoke;
            lightTheme["GridForeColor"] = Color.DarkBlue;
            lightTheme["GridBackColor"] = Color.WhiteSmoke;
            lightTheme["GridHeaderFore"] = Color.DarkBlue;
            lightTheme["GridHeaderBack"] = Color.WhiteSmoke;
            lightTheme["GridCellFore"] = Color.DarkBlue;
            lightTheme["GridCellBack"] = Color.WhiteSmoke;
            lightTheme["GridSelectCellBack"] = Color.LightBlue;
            lightTheme["GridSelectCellFore"] = Color.DarkBlue;
            lightTheme["Back"] = Color.WhiteSmoke;
            lightTheme["Fore"] = Color.DarkBlue;

            themes["Mordor"] = mordorTheme;
            themes["Light"] = lightTheme;
            currentTheme = lightTheme;
        }

        public static void SetColorScheme(Control control)
        {
            if (control is Button)
            {
                if (((Button)control).FlatStyle == FlatStyle.Flat)
                {
                    control.BackColor = currentTheme["FlatButtonBack"];
                    control.ForeColor = currentTheme["FlatButtonFore"];
                }
                else
                {
                    control.BackColor = currentTheme["ButtonBack"];
                    control.ForeColor = currentTheme["ButtonFore"];
                }
            }
            else if (control is DataGridView)
            {
                ((DataGridView)control).GridColor = currentTheme["GridColor"];
                ((DataGridView)control).ForeColor = currentTheme["GridForeColor"];
                ((DataGridView)control).BackgroundColor = currentTheme["GridBackColor"];
                ((DataGridView)control).ColumnHeadersDefaultCellStyle = new DataGridViewCellStyle() { ForeColor = currentTheme["GridHeaderFore"], BackColor = currentTheme["GridHeaderBack"], SelectionForeColor = currentTheme["GridHeaderFore"], SelectionBackColor = currentTheme["GridHeaderBack"] };
                ((DataGridView)control).DefaultCellStyle = new DataGridViewCellStyle() { ForeColor = currentTheme["GridCellFore"], BackColor = currentTheme["GridCellBack"], SelectionBackColor = currentTheme["GridSelectCellBack"], SelectionForeColor = currentTheme["GridSelectCellFore"] };
            }
            else
            {
                control.BackColor = currentTheme["Back"];
                control.ForeColor = currentTheme["Fore"];
            }
            foreach (Control c in control.Controls)
                SetColorScheme(c);
        }
    }
}
