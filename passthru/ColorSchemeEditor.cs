using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace PassThru
{
    public partial class ColorSchemeEditor : Form
    {
        string themeName;
        SerializableDictionary<string, Color> theme;

        public ColorSchemeEditor()
        {
            themeName = "NewTheme-" + DateTime.Now.Ticks;
            theme = new SerializableDictionary<string, Color>();
            theme["FlatButtonBack"] = Color.Black;
            theme["FlatButtonFore"] = Color.White;
            theme["ButtonBack"] = Color.WhiteSmoke;
            theme["ButtonFore"] = Color.DarkBlue;
            theme["GridColor"] = Color.WhiteSmoke;
            theme["GridForeColor"] = Color.DarkBlue;
            theme["GridBackColor"] = Color.WhiteSmoke;
            theme["GridHeaderFore"] = Color.DarkBlue;
            theme["GridHeaderBack"] = Color.WhiteSmoke;
            theme["GridCellFore"] = Color.DarkBlue;
            theme["GridCellBack"] = Color.WhiteSmoke;
            theme["GridSelectCellBack"] = Color.LightBlue;
            theme["GridSelectCellFore"] = Color.DarkBlue;
            theme["Back"] = Color.WhiteSmoke;
            theme["Fore"] = Color.DarkBlue;
            InitializeComponent();
        }

        public ColorSchemeEditor(string themeName)
        {
            this.themeName = themeName;
            theme = ColorScheme.themes[themeName];
            InitializeComponent();
        }

        private void ColorSchemeEditor_Load(object sender, EventArgs e)
        {
            this.Text = themeName;
            textBox1.Text = themeName;
            DataSet ds = new DataSet();
            foreach (KeyValuePair<string, Color> i in theme)
                dataGridView1.Rows.Add((string)i.Key, ((int)i.Value.R).ToString(), ((int)i.Value.G).ToString(), ((int)i.Value.B).ToString());
            ColorScheme.SetColorScheme(this);
            ColorScheme.ThemeChanged += new System.Threading.ThreadStart(ColorScheme_ThemeChanged);
        }

        void ColorScheme_ThemeChanged()
        {
            ColorScheme.SetColorScheme(this);
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            themeName = textBox1.Text;
            this.Text = themeName;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    theme[(string)row.Cells[0].Value] = Color.FromArgb(int.Parse((string)row.Cells[1].Value), int.Parse((string)row.Cells[2].Value), int.Parse((string)row.Cells[3].Value));
                }
            }
            catch { }
            ColorScheme.themes[themeName] = theme;
            ColorScheme.Save();
            ColorScheme.ChangeTheme(themeName);
        }
    }
}
