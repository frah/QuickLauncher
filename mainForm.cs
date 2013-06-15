using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Threading;
using System.Text.RegularExpressions;

namespace QuickLauncher
{
    public partial class mainForm : CSWinFormExAeroToClient.GlassForm
    {
        private HotKey hotKey;
        private Dictionary<string, string> apps;

        public mainForm()
        {
            InitializeComponent();
        }

        private void mainForm_Load(object sender, EventArgs e)
        {
            /* Initialize HotKey */
            hotKey = new HotKey(MOD_KEY.CONTROL | MOD_KEY.SHIFT, Keys.Space);
            hotKey.HotKeyPush += new EventHandler(hotKey_HotKeyPush);

            this.ExtendFrameEnabled = true;
            this.BlurBehindWindowEnabled = false;
            //this.GlassMargins = new CSWinFormExAeroToClient.NativeMethods.MARGINS(15, 100, 0, 23);
            this.GlassMargins = new CSWinFormExAeroToClient.NativeMethods.MARGINS(-1);
            this.Invalidate();

            initWorker.RunWorkerAsync();
        }

        private void ShowBalloon(string text, int time, ToolTipIcon icon)
        {
            notifyIcon.BalloonTipIcon = icon;
            notifyIcon.BalloonTipText = text;
            notifyIcon.ShowBalloonTip(time);
        }
        private void ShowBalloon(string text, int time)
        {
            ShowBalloon(text, time, ToolTipIcon.Info);
        }
        private void ShowBalloon(string text, ToolTipIcon icon)
        {
            ShowBalloon(text, 3000, icon);
        }
        private void ShowBalloon(string text)
        {
            ShowBalloon(text, 3000);
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

        private void launcherText_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                string path = containsApp(launcherText.Text);
                if (path != null)
                {
                    try
                    {
                        System.Diagnostics.Process.Start(path);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("アプリケーション起動時に例外が発生しました\r\n" + ex.ToString(), "QuickLauncher", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    formHide();
                }
            }
        }

        private string containsApp(string key)
        {
            foreach (var a in apps)
            {
                if (string.Compare(a.Key, key, true) == 0) return a.Value;
            }
            return null;
        }

        private void quitMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("終了してもよろしいですか？", "QuickLauncher", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                notifyIcon.Visible = false;
                notifyIcon.Dispose();
                hotKey.Dispose();
                Application.Exit();
                Environment.Exit(0);
            }
        }

        private void initWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            ShowBalloon("アプリケーション一覧のロードを開始します...");

            /* List up applications */
            AutoCompleteStringCollection complSource = new AutoCompleteStringCollection();
            apps = new Dictionary<string, string>();
            var regex = new Regex(
                @"(.*\\(スタートアップ|Startup)\\.*|.*(About|Readme|Sample|Setup|Uninstall|アンインストール|削除|Change ?Log|Help).*?)",
                RegexOptions.IgnoreCase | RegexOptions.Compiled);

            foreach (var p in new Environment.SpecialFolder[] { Environment.SpecialFolder.CommonPrograms, Environment.SpecialFolder.Programs })
            {
                foreach (var ext in new string[] { "*.lnk", "*.appref-ms" })
                {
                    var a = from app in Directory.EnumerateFiles(
                        Environment.GetFolderPath(p), ext, SearchOption.AllDirectories)
                            where !regex.IsMatch(app)
                            select app;
                    foreach (var ap in a)
                    {
                        try
                        {
                            apps.Add(Path.GetFileNameWithoutExtension(ap), ap);
                        }
                        catch (ArgumentException ae)
                        {
                            //Console.WriteLine(ae);
                        }
                    }
                }
            }
            complSource.AddRange(apps.Keys.ToArray());
            e.Result = complSource;
        }

        private void initWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                ShowBalloon("アプリケーション一覧のロードに失敗しました", ToolTipIcon.Error);
                Console.WriteLine(e.Error);
            }
            else
            {
                launcherText.AutoCompleteCustomSource = e.Result as AutoCompleteStringCollection;
                ShowBalloon(launcherText.AutoCompleteCustomSource.Count+"件のアプリケーションのロードを完了しました");
            }
        }
    }
}
