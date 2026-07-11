using ModernCmd;
using PluginFrame;
using System;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace OriginalCmdEmbedding
{
    public class OriginalCmdEmbedding : IPlugin, ICommand
    {
        public string Name => "原始命令行嵌入";
        public string Version => "1.0.2";
        public string Author => "摩登命令行开发组";
        public string Description => "使摩登命令行兼容原始的windows命令行";
        public string CommandName => "cmd";
        public string CommandDescription => "键入cmd <命令>来运行一个dos命令，如cmd echo test";

        public void Main(ModernCmd.ModernCmd modernCmd)
        {
            
        }

        public int Run(string[] args, TextBox commandLogger)
        {
            Process cmd = new Process();
            cmd.StartInfo.FileName = "cmd.exe";
            cmd.StartInfo.CreateNoWindow = true;
            cmd.StartInfo.RedirectStandardError = true;
            cmd.StartInfo.RedirectStandardInput = true;
            cmd.StartInfo.RedirectStandardOutput = true;
            cmd.StartInfo.UseShellExecute = false;
            cmd.Start();

            string argsString = string.Join(" ", args);
            cmd.StandardInput.WriteLine(argsString + "&exit");
            cmd.StandardInput.Close();

            StringBuilder outputBuilder = new StringBuilder();
            StringBuilder errorBuilder = new StringBuilder();
            cmd.OutputDataReceived += (sender, e) =>
            {
                if (!string.IsNullOrEmpty(e.Data))
                {
                    outputBuilder.AppendLine(e.Data);
                }
            };
            cmd.ErrorDataReceived += (sender, e) =>
            {
                if (!string.IsNullOrEmpty(e.Data))
                {
                    errorBuilder.AppendLine(e.Data);
                }
            };

            cmd.BeginOutputReadLine();
            cmd.BeginErrorReadLine();
            if (!cmd.WaitForExit(5000))
            {
                cmd.Kill();
                commandLogger.Text += $"[{Name}]: 执行{argsString}超时，已终止进程\r\n";
                return 1;
            }

            string allOutput = outputBuilder.ToString() + errorBuilder.ToString();
            string[] lines = allOutput.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
            int skipCount = 0;
            foreach (var line in lines)
            {
                if (line.Contains("Microsoft") || string.IsNullOrWhiteSpace(line))
                {
                    skipCount++;
                }
                else
                {
                    break;
                }
            }
            string finalOutput = string.Join("\r\n", lines.Skip(skipCount + 1));

            commandLogger.Text += $"[{Name}]: 正在执行{argsString}\r\n{finalOutput}\r\n";
            return 0;
        }
    }
}
