using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using FM;

namespace PassThru.Modules
{
    public partial class ModuleConfiguration : UserControl
    {
        List<FirewallModule> modules = new List<FirewallModule>();
        NetworkAdapter na;
        string dragged = null;
        
        public ModuleConfiguration(NetworkAdapter na)
        {
            this.na = na;
            for (int x = 0; x < na.modules.Count; x++)
            {
                modules.Add(na.modules.GetModule(x));
            }
            InitializeComponent();
        }

        void UpdateView()
        {
            if (this.checkedListBoxModules.InvokeRequired)
            {
                System.Threading.ThreadStart ts = new System.Threading.ThreadStart(UpdateView);
                this.checkedListBoxModules.Invoke(ts);
            }
            else
            {
                checkedListBoxModules.Items.Clear();
                for (int x = 0; x < modules.Count; x++)
                {
                    checkedListBoxModules.Items.Add(modules[x].MetaData.Name, modules[x].Enabled);
                }
            }
        }

        private void ModuleConfiguration_Load(object sender, EventArgs e)
        {
            UpdateView();
            switch (FM.LanguageConfig.GetCurrentLanguage())
            {
                case FM.LanguageConfig.Language.NONE:
                case FM.LanguageConfig.Language.ENGLISH:
                    buttonEnable.Text = "Enable/Disable";
                    buttonOpenConfiguration.Text = "Open Configuration";
                    buttonHelp.Text = "Help";
                    buttonMoveDown.Text = "Move Down";
                    buttonMoveUp.Text = "Move Up";
                    break;
                case FM.LanguageConfig.Language.PORTUGUESE:
                    buttonEnable.Text = "Activar / Desactivar";
                    buttonOpenConfiguration.Text = "Abrir Configuração";
                    buttonHelp.Text = "ajudar";
                    buttonMoveDown.Text = "mover para Baixo";
                    buttonMoveUp.Text = "mover para cima";
                    break;
                case FM.LanguageConfig.Language.RUSSIAN:
                    buttonEnable.Text = "Включение / выключение";
                    buttonOpenConfiguration.Text = "Открытая конфигурация";
                    buttonHelp.Text = "Помогите";
                    buttonMoveDown.Text = "спускать";
                    buttonMoveUp.Text = "вверх";
                    break;
                case FM.LanguageConfig.Language.SPANISH:
                    buttonEnable.Text = "Activar / Desactivar";
                    buttonOpenConfiguration.Text = "Abrir Configuración";
                    buttonHelp.Text = "ayuda";
                    buttonMoveDown.Text = "Bajar";
                    buttonMoveUp.Text = "Subir";
                    break;
                case FM.LanguageConfig.Language.CHINESE:
                    buttonEnable.Text = "启用/禁用";
                    buttonOpenConfiguration.Text = "打开配置";
                    buttonHelp.Text = "帮助";
                    buttonMoveDown.Text = "下移";
                    buttonMoveUp.Text = "动起来";
                    break;
                case FM.LanguageConfig.Language.GERMAN:
                    buttonEnable.Text = "Aktivieren / Deaktivieren";
                    buttonOpenConfiguration.Text = "Konfiguration öffnen";
                    buttonHelp.Text = "Hilfe";
                    buttonMoveDown.Text = "Nach unten";
                    buttonMoveUp.Text = "Nach oben";
                    break;
            }
        }

        private void buttonEnable_Click(object sender, EventArgs e)
        {
            try
            {
                int temp = checkedListBoxModules.SelectedIndex;
                modules[checkedListBoxModules.SelectedIndex].Enabled = !modules[checkedListBoxModules.SelectedIndex].Enabled;
                if (modules[checkedListBoxModules.SelectedIndex].Enabled)
                {
                    modules[checkedListBoxModules.SelectedIndex].ModuleStart();
                }
                else
                {
                    modules[checkedListBoxModules.SelectedIndex].ModuleStop();
                }
                UpdateView();
                checkedListBoxModules.SelectedIndex = temp;
            }
            catch { }          
        }

        private void checkedListBoxModules_ItemCheck_1(object sender, ItemCheckEventArgs e)
        {
            modules[e.Index].Enabled = (e.NewValue == CheckState.Checked);
            if (modules[e.Index].Enabled)
            {
                modules[e.Index].ModuleStart();
            }
            else
            {
                modules[e.Index].ModuleStop();
            }
        }

        private void buttonOpenConfiguration_Click(object sender, EventArgs e)
        {
            try
            {
                UserControl uc = modules[checkedListBoxModules.SelectedIndex].GetControl();
                if (uc != null)
                {
                    Form f = new Form();
                    f.Size = new System.Drawing.Size(640, 480);
                    System.Reflection.Assembly target = System.Reflection.Assembly.GetExecutingAssembly();
                    f.Icon = new System.Drawing.Icon(target.GetManifestResourceStream("PassThru.Resources.newIcon.ico"));
                    f.Text = na.InterfaceInformation.Name + ": " + modules[checkedListBoxModules.SelectedIndex].MetaData.Name + " - " + modules[checkedListBoxModules.SelectedIndex].MetaData.Version;
                    f.Controls.Add(uc);
                    f.Show();
                }
            }
            catch { }
        }

        private void buttonMoveUp_Click(object sender, EventArgs e)
        {
            try
            {
                if (checkedListBoxModules.SelectedIndex != 0)
                {
                    na.modules.InsertPIndex(checkedListBoxModules.SelectedIndex, checkedListBoxModules.SelectedIndex - 1);
                    int newIndex = checkedListBoxModules.SelectedIndex - 1;
                    modules.Clear();
                    for (int x = 0; x < na.modules.Count; x++)
                    {
                        modules.Add(na.modules.GetModule(x));
                    }
                    UpdateView();
                    checkedListBoxModules.SelectedIndex = newIndex;
                }
            }
            catch
            { }
        }

        private void buttonMoveDown_Click(object sender, EventArgs e)
        {
            try
            {
                if (checkedListBoxModules.SelectedIndex != modules.Count - 1)
                {
                    na.modules.InsertPIndex(checkedListBoxModules.SelectedIndex, checkedListBoxModules.SelectedIndex + 1);
                    int newIndex = checkedListBoxModules.SelectedIndex + 1;
                    modules.Clear();
                    for (int x = 0; x < na.modules.Count; x++)
                    {
                        modules.Add(na.modules.GetModule(x));
                    }
                    UpdateView();
                    checkedListBoxModules.SelectedIndex = newIndex;
                }
            }
            catch
            { }
        }

        private void buttonHelp_Click(object sender, EventArgs e)
        {
            try
            {
                Form f = new Form();
                f.Size = new System.Drawing.Size(640, 480);
                System.Reflection.Assembly target = System.Reflection.Assembly.GetExecutingAssembly();
                f.Icon = new System.Drawing.Icon(target.GetManifestResourceStream("PassThru.Resources.newIcon.ico"));
                f.Text = "Help";
                Help uc = new Help();
                uc.Dock = DockStyle.Fill;
                f.Controls.Add(uc);
                f.Show();
            }
            catch { }
        }
    }
}
