using PluginFrame;
using System.Diagnostics;
using ModernCmd;
using System.Windows.Forms;

namespace OriginalCmdEmbedding
{
    public class OriginalCmdEmbedding : IPlugin, ICommand
    {
        public string Name => "原始命令行嵌入";
        public string Version => "1.0.1";
        public string Author => "摩登命令行开发组";
        public string Description => "使摩登命令行兼容原始的windows命令行";
        public string CommandName => "cmd";

        public void Main(ModernCmd.ModernCmd modernCmd)
        {
            
        }

        public int Run(string[] args, TextBox CommandLogger)
        {
            Process cmd = new Process();
            cmd.StartInfo.FileName = "cmd.exe";
            cmd.StartInfo.CreateNoWindow = true;
            cmd.StartInfo.RedirectStandardError = true;
            cmd.StartInfo.RedirectStandardInput = true;
            cmd.StartInfo.RedirectStandardOutput = true;
            cmd.StartInfo.UseShellExecute = false;
            cmd.Start();
            string argsString = "";
            foreach (string arg in args)
            {
                argsString += " " + arg;
            }
            cmd.StandardInput.WriteLine(argsString + "&exit");
            string[] OutPutList = cmd.StandardOutput.ReadToEnd().Split('\n');
            string OutPut = "";
            for (int i = 4; i < OutPutList.Length; i++)
            {
                OutPut += OutPutList[i] + "\r\n";
            }
            cmd.Close();
            CommandLogger.Text += $"[{Name}]：正在执行{argsString}\r\n{OutPut}\r\n";
            return 0;
        }
    }
}
