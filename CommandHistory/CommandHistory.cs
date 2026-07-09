using System;
using PluginFrame;
using System.Windows.Forms;
using System.Collections.Generic;
using ModernCmd;

namespace CommandHistory
{
    public class CommandHistory : IPlugin
    {
        public string Name => "历史命令";
        public string Version => "1.1.2";
        public string Author => "摩登命令行开发组";
        public string Description => "提供了快捷输入历史命令的按钮\n右键点击历史命令按钮来查看历史命令菜单";

        public void Main(ModernCmd.ModernCmd modernCmd)
        {
            ContextMenuStrip menuStrip = new ContextMenuStrip();
            List<string> commandHistories = new List<string>();
            modernCmd.RunCommand.Click += new EventHandler((s, e) =>
            {
                string command = modernCmd.CommandInput.Text;
                if (!commandHistories.Contains(command) & !string.IsNullOrEmpty(command) & !string.IsNullOrWhiteSpace(command))
                {
                    commandHistories.Add(command);
                    ToolStripMenuItem cmdHistory = new ToolStripMenuItem();
                    cmdHistory.Name = command;
                    cmdHistory.Text = command;
                    cmdHistory.Click += new EventHandler((s_, e_) =>
                    {
                        modernCmd.CommandInput.Text = cmdHistory.Text;
                    });
                    menuStrip.Items.Add(cmdHistory);
                }
            });
            Button commandHistory = new Button();
            commandHistory.Location = new System.Drawing.Point(modernCmd.RunCommand.Location.X + 110, modernCmd.RunCommand.Location.Y);
            commandHistory.Name = "commandHistory";
            commandHistory.Size = modernCmd.RunCommand.Size;
            commandHistory.TabIndex = 1;
            commandHistory.Text = "历史命令";
            commandHistory.UseVisualStyleBackColor = true;
            commandHistory.ContextMenuStrip = menuStrip;
            modernCmd.Controls.Add(commandHistory);
        }
    }
}
