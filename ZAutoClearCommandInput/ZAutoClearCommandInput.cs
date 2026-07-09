using System;
using PluginFrame;

namespace ZAutoClearCommandInput
{
    public class ZAutoClearCommandInput : IPlugin
    {
        public string Name => "自动清空命令输入框";
        public string Version => "1.0.0";
        public string Author => "摩登命令行开发组";
        public string Description => "用户每次输入完命令后会自动清空命令输入框";

        public void Main(ModernCmd.ModernCmd modernCmd)
        {
            modernCmd.RunCommand.Click += new EventHandler((s, e) =>
            {
                modernCmd.CommandInput.Text = "";
            });
        }
    }
}
