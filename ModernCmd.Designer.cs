namespace ModernCmd
{
    partial class ModernCmd
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.CommandLogger = new System.Windows.Forms.TextBox();
            this.RunCommand = new System.Windows.Forms.Button();
            this.CommandInput = new System.Windows.Forms.TextBox();
            this.MenuStrip = new System.Windows.Forms.MenuStrip();
            this.FunctionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.HelpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.AboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.PluginsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // CommandLogger
            // 
            this.CommandLogger.BackColor = System.Drawing.SystemColors.WindowText;
            this.CommandLogger.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.CommandLogger.Cursor = System.Windows.Forms.Cursors.No;
            this.CommandLogger.ForeColor = System.Drawing.SystemColors.Window;
            this.CommandLogger.Location = new System.Drawing.Point(12, 80);
            this.CommandLogger.Multiline = true;
            this.CommandLogger.Name = "CommandLogger";
            this.CommandLogger.ReadOnly = true;
            this.CommandLogger.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.CommandLogger.Size = new System.Drawing.Size(542, 311);
            this.CommandLogger.TabIndex = 0;
            // 
            // RunCommand
            // 
            this.RunCommand.Location = new System.Drawing.Point(479, 40);
            this.RunCommand.Name = "RunCommand";
            this.RunCommand.Size = new System.Drawing.Size(75, 23);
            this.RunCommand.TabIndex = 1;
            this.RunCommand.Text = "运行命令";
            this.RunCommand.UseVisualStyleBackColor = true;
            this.RunCommand.Click += new System.EventHandler(this.RunCommand_Click);
            // 
            // CommandInput
            // 
            this.CommandInput.BackColor = System.Drawing.SystemColors.Menu;
            this.CommandInput.Font = new System.Drawing.Font("Arial Rounded MT Bold", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CommandInput.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.CommandInput.Location = new System.Drawing.Point(12, 42);
            this.CommandInput.Name = "CommandInput";
            this.CommandInput.Size = new System.Drawing.Size(461, 21);
            this.CommandInput.TabIndex = 2;
            // 
            // MenuStrip
            // 
            this.MenuStrip.BackColor = System.Drawing.SystemColors.GrayText;
            this.MenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.FunctionsToolStripMenuItem,
            this.HelpToolStripMenuItem,
            this.PluginsToolStripMenuItem});
            this.MenuStrip.Location = new System.Drawing.Point(0, 0);
            this.MenuStrip.Name = "MenuStrip";
            this.MenuStrip.Size = new System.Drawing.Size(650, 25);
            this.MenuStrip.TabIndex = 3;
            this.MenuStrip.Text = "menuStrip";
            // 
            // FunctionsToolStripMenuItem
            // 
            this.FunctionsToolStripMenuItem.ForeColor = System.Drawing.SystemColors.MenuText;
            this.FunctionsToolStripMenuItem.Name = "FunctionsToolStripMenuItem";
            this.FunctionsToolStripMenuItem.Size = new System.Drawing.Size(44, 21);
            this.FunctionsToolStripMenuItem.Text = "功能";
            // 
            // HelpToolStripMenuItem
            // 
            this.HelpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.AboutToolStripMenuItem});
            this.HelpToolStripMenuItem.ForeColor = System.Drawing.SystemColors.MenuText;
            this.HelpToolStripMenuItem.Name = "HelpToolStripMenuItem";
            this.HelpToolStripMenuItem.Size = new System.Drawing.Size(44, 21);
            this.HelpToolStripMenuItem.Text = "帮助";
            // 
            // AboutToolStripMenuItem
            // 
            this.AboutToolStripMenuItem.Name = "AboutToolStripMenuItem";
            this.AboutToolStripMenuItem.Size = new System.Drawing.Size(100, 22);
            this.AboutToolStripMenuItem.Text = "关于";
            this.AboutToolStripMenuItem.Click += new System.EventHandler(this.AboutToolStripMenuItem_Click);
            // 
            // PluginsToolStripMenuItem
            // 
            this.PluginsToolStripMenuItem.ForeColor = System.Drawing.SystemColors.MenuText;
            this.PluginsToolStripMenuItem.Name = "PluginsToolStripMenuItem";
            this.PluginsToolStripMenuItem.Size = new System.Drawing.Size(44, 21);
            this.PluginsToolStripMenuItem.Text = "插件";
            // 
            // ModernCmd
            // 
            this.AcceptButton = this.RunCommand;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.GrayText;
            this.ClientSize = new System.Drawing.Size(650, 403);
            this.Controls.Add(this.CommandInput);
            this.Controls.Add(this.RunCommand);
            this.Controls.Add(this.CommandLogger);
            this.Controls.Add(this.MenuStrip);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.HelpButton = true;
            this.MainMenuStrip = this.MenuStrip;
            this.MaximizeBox = false;
            this.Name = "ModernCmd";
            this.Text = "ModernCmd";
            this.MenuStrip.ResumeLayout(false);
            this.MenuStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.TextBox CommandLogger;
        public System.Windows.Forms.Button RunCommand;
        public System.Windows.Forms.TextBox CommandInput;
        public System.Windows.Forms.MenuStrip MenuStrip;
        public System.Windows.Forms.ToolStripMenuItem FunctionsToolStripMenuItem;
        public System.Windows.Forms.ToolStripMenuItem HelpToolStripMenuItem;
        public System.Windows.Forms.ToolStripMenuItem AboutToolStripMenuItem;
        public System.Windows.Forms.ToolStripMenuItem PluginsToolStripMenuItem;
    }
}

