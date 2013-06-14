using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace QuickLauncher
{
    public partial class mainForm : Form
    {
        private HotKey hotKey;
        private AutoCompleteStringCollection complSource;

        public mainForm()
        {
            InitializeComponent();
        }

        private void mainForm_Load(object sender, EventArgs e)
        {
            /* Initialize HotKey */
            hotKey = new HotKey(MOD_KEY.CONTROL | MOD_KEY.SHIFT, Keys.Space);
            hotKey.HotKeyPush += new EventHandler(hotKey_HotKeyPush);

            /* List up applications */
            complSource = new AutoCompleteStringCollection();
            var apps = from app in Directory.EnumerateFiles(
                Environment.GetFolderPath(Environment.SpecialFolder.StartMenu),
                           //"*.(lnk|appref-ms)",
                "*.lnk",
                SearchOption.AllDirectories
                )
                       select new
                       {
                           Name = Path.GetFileNameWithoutExtension(app),
                           Path = app
                       };

            foreach (var a in apps)
            {
                complSource.Add(a.Name);
            }
            launcherText.AutoCompleteCustomSource = complSource;
        }

        private void hotKey_HotKeyPush(object sender, EventArgs e)
        {
            if (this.Visible == true)
            {
                formHide();
            } else {
                this.WindowState = FormWindowState.Normal;
                this.Show();

                this.Activate();
                launcherText.Focus();
            }
        }

        private void mainForm_Shown(object sender, EventArgs e)
        {
            formHide();
        }

        private void formHide()
        {
            launcherText.ResetText();
            this.Hide();
        }
    }
}
