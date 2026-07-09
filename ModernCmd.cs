using System;
using System.Windows.Forms;

namespace ModernCmd
{
    public partial class ModernCmd : Form
    {
        public ModernCmd()
        {
            InitializeComponent();
        }

        private void RunCommand_Click(object sender, EventArgs e)
        {
            int errorlevel = Program.RunCommand(CommandInput.Text, CommandLogger);
            if (errorlevel != 0)
            {
                CommandLogger.Text += "运行命令时出错\r\n";
            }
        }

        private void AboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show(
                "软件名称：摩登命令行\n" +
                "作者：摩登命令行开发组\n" +
                "版本：v1.0.0\n" +
                "\n此软件顾名思义，是更高级的命令行，支持载入插件", 
                "信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
