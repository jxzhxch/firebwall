using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.ComponentModel;
using FM;
using System.IO;

namespace PassThru
{
    public static class ColorScheme
    {
        public static string currentTheme = null;
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
            currentTheme = "Light";

            string folder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            folder = folder + Path.DirectorySeparatorChar + "firebwall";
            if (!Directory.Exists(folder))
                Directory.CreateDirectory(folder);
            if (File.Exists(folder + Path.DirectorySeparatorChar + "currentTheme.cfg"))
                currentTheme = File.ReadAllText(folder + Path.DirectorySeparatorChar + "currentTheme.cfg");
            folder = folder + Path.DirectorySeparatorChar + "themes";
            if (!Directory.Exists(folder))
                Directory.CreateDirectory(folder);

            foreach (string dir in Directory.GetFiles(folder))
            {
                if (currentTheme == null)
                {
                    currentTheme = dir;
                }
                string themeName = dir.Substring(dir.LastIndexOf(Path.DirectorySeparatorChar) + 1);
                themes[themeName] = new SerializableDictionary<string, Color>();
                foreach (string line in File.ReadAllLines(dir))
                {
                    string[] split = line.Split(':');
                    themes[themeName][split[0]] = Color.FromArgb(int.Parse(split[1]), int.Parse(split[2]), int.Parse(split[3]));
                }
            }

            Save();
        }

        public static List<string> GetThemes()
        {
            return new List<string>(themes.Keys);
        }

        public static string GetCurrentTheme()
        {
            return currentTheme;
        }

        public static void ChangeTheme(string newTheme)
        {
            currentTheme = newTheme;
            Save();
            if (ThemeChanged != null)
                ThemeChanged();
        }

        public static event System.Threading.ThreadStart ThemeChanged;

        public static void Save()
        {
            string folder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            folder = folder + Path.DirectorySeparatorChar + "firebwall";
            if (!Directory.Exists(folder))
                Directory.CreateDirectory(folder);
            File.WriteAllText(folder + Path.DirectorySeparatorChar + "currentTheme.cfg", currentTheme);
            folder = folder + Path.DirectorySeparatorChar + "themes";
            if (!Directory.Exists(folder))
                Directory.CreateDirectory(folder);

            foreach (KeyValuePair<string, SerializableDictionary<string, Color>> theme in themes)
            {
                string ThemeString = "";
                foreach (KeyValuePair<string, Color> value in theme.Value)
                {
                    ThemeString += value.Key + ":" + value.Value.R.ToString() + ":" + value.Value.G.ToString() + ":" + value.Value.B.ToString() + "\n";
                }
                File.WriteAllText(folder + Path.DirectorySeparatorChar + theme.Key, ThemeString);
            }
        }

        public static void SetColorScheme(Control control)
        {
            if (control is Button)
            {
                if (((Button)control).FlatStyle == FlatStyle.Flat)
                {
                    control.BackColor = themes[currentTheme]["FlatButtonBack"];
                    control.ForeColor = themes[currentTheme]["FlatButtonFore"];
                }
                else
                {
                    control.BackColor = themes[currentTheme]["ButtonBack"];
                    control.ForeColor = themes[currentTheme]["ButtonFore"];
                }
            }
            else if (control is DataGridView)
            {
                ((DataGridView)control).GridColor = themes[currentTheme]["GridColor"];
                ((DataGridView)control).ForeColor = themes[currentTheme]["GridForeColor"];
                ((DataGridView)control).BackgroundColor = themes[currentTheme]["GridBackColor"];
                ((DataGridView)control).ColumnHeadersDefaultCellStyle = new DataGridViewCellStyle() { ForeColor = themes[currentTheme]["GridHeaderFore"], BackColor = themes[currentTheme]["GridHeaderBack"], SelectionForeColor = themes[currentTheme]["GridHeaderFore"], SelectionBackColor = themes[currentTheme]["GridHeaderBack"] };
                ((DataGridView)control).DefaultCellStyle = new DataGridViewCellStyle() { ForeColor = themes[currentTheme]["GridCellFore"], BackColor = themes[currentTheme]["GridCellBack"], SelectionBackColor = themes[currentTheme]["GridSelectCellBack"], SelectionForeColor = themes[currentTheme]["GridSelectCellFore"] };
            }
            else
            {
                control.BackColor = themes[currentTheme]["Back"];
                control.ForeColor = themes[currentTheme]["Fore"];
            }
            foreach (Control c in control.Controls)
                SetColorScheme(c);
        }
    }
}
