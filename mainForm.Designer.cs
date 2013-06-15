namespace QuickLauncher
{
    partial class mainForm
    {
        /// <summary>
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージ リソースが破棄される場合 true、破棄されない場合は false です。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows フォーム デザイナーで生成されたコード

        /// <summary>
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(mainForm));
            this.launcherText = new System.Windows.Forms.TextBox();
            this.notifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.notifyIconMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.quitMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.initWorker = new System.ComponentModel.BackgroundWorker();
            this.notifyIconMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // launcherText
            // 
            this.launcherText.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.launcherText.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.launcherText.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.launcherText.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.launcherText.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.launcherText.Font = new System.Drawing.Font("MS UI Gothic", 30F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.launcherText.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.launcherText.Location = new System.Drawing.Point(15, 15);
            this.launcherText.Margin = new System.Windows.Forms.Padding(5);
            this.launcherText.MaxLength = 100;
            this.launcherText.Name = "launcherText";
            this.launcherText.Size = new System.Drawing.Size(500, 40);
            this.launcherText.TabIndex = 0;
            this.launcherText.TabStop = false;
            this.launcherText.WordWrap = false;
            this.launcherText.KeyUp += new System.Windows.Forms.KeyEventHandler(this.launcherText_KeyUp);
            // 
            // notifyIcon
            // 
            this.notifyIcon.BalloonTipTitle = "QuickLauncher";
            this.notifyIcon.ContextMenuStrip = this.notifyIconMenu;
            this.notifyIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon.Icon")));
            this.notifyIcon.Text = "QuickLauncher";
            this.notifyIcon.Visible = true;
            // 
            // notifyIconMenu
            // 
            this.notifyIconMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.quitMenuItem});
            this.notifyIconMenu.Name = "notifyIconMenu";
            this.notifyIconMenu.Size = new System.Drawing.Size(101, 26);
            // 
            // quitMenuItem
            // 
            this.quitMenuItem.Name = "quitMenuItem";
            this.quitMenuItem.ShortcutKeyDisplayString = "";
            this.quitMenuItem.Size = new System.Drawing.Size(100, 22);
            this.quitMenuItem.Text = "終了";
            this.quitMenuItem.Click += new System.EventHandler(this.quitMenuItem_Click);
            // 
            // initWorker
            // 
            this.initWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.initWorker_DoWork);
            this.initWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.initWorker_RunWorkerCompleted);
            // 
            // mainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.ClientSize = new System.Drawing.Size(600, 70);
            this.ControlBox = false;
            this.Controls.Add(this.launcherText);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "mainForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "launcher";
            this.Load += new System.EventHandler(this.mainForm_Load);
            this.Shown += new System.EventHandler(this.mainForm_Shown);
            this.notifyIconMenu.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox launcherText;
        private System.Windows.Forms.NotifyIcon notifyIcon;
        private System.Windows.Forms.ContextMenuStrip notifyIconMenu;
        private System.Windows.Forms.ToolStripMenuItem quitMenuItem;
        private System.ComponentModel.BackgroundWorker initWorker;
    }
}

