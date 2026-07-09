using System;
using PluginFrame;
using ModernCmd;
using System.Windows.Forms;

namespace ClearLogger
{
    public class ClearLogger : IPlugin, ICommand
    {
        public string Name => "清空日志器";
        public string Version => "1.1.0";
        public string Author => "摩登命令行开发组";
        public string Description => "提供了清空日志器的功能";
        public string CommandName => "cls";

        public void Main(ModernCmd.ModernCmd modernCmd)
        {
            ToolStripMenuItem ClearLogger = new ToolStripMenuItem();
            ClearLogger.Name = "ClearLogger";
            ClearLogger.Text = "清空日志器";
            ClearLogger.Click += new EventHandler((s, e) =>
            {
                modernCmd.CommandLogger.Text = "";
            });
            modernCmd.FunctionsToolStripMenuItem.DropDownItems.Add(ClearLogger);
        }

        public int Run(string[] args, TextBox CommandLogger)
        {
            CommandLogger.Text = "";
            return 0;
        }
    }
}
