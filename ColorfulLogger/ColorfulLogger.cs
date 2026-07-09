using PluginFrame;
using ModernCmd;
using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace ColorfulLogger
{
    public class ColorfulLogger : IPlugin, ICommand
    {
        public string Name => "多彩日志器";
        public string Version => "1.1.0";
        public string Author => "摩登命令行开发组";
        public string Description => "增加了改变日志器颜色的功能，目前共有5种颜色可选";
        public string CommandName => "color";

        public void Main(ModernCmd.ModernCmd modernCmd)
        {
            try
            {
                StreamReader config = new StreamReader("configs/ColorfulLogger.config");
                string color = config.ReadToEnd().Split('\n')[1].Replace("\r", "");
                if (color == "white")
                {
                    modernCmd.CommandLogger.ForeColor = Color.White;
                }
                else if (color == "green")
                {
                    modernCmd.CommandLogger.ForeColor = Color.LightGreen;
                }
                else if (color == "red")
                {
                    modernCmd.CommandLogger.ForeColor = Color.Red;
                }
                else if (color == "yellow")
                {
                    modernCmd.CommandLogger.ForeColor = Color.Yellow;
                }
                else if (color == "blue")
                {
                    modernCmd.CommandLogger.ForeColor = Color.LightBlue;
                }
                config.Close();
            }
            catch
            {
                if (!Directory.Exists("configs"))
                {
                    Directory.CreateDirectory("configs");
                }
            }

            ToolStripMenuItem ColorfulLogger = new ToolStripMenuItem();
            ColorfulLogger.Name = "colorfulLogger";
            ColorfulLogger.Text = "更改日志器颜色";
            modernCmd.FunctionsToolStripMenuItem.DropDownItems.Add(ColorfulLogger);

            ToolStripMenuItem ColorWhite = new ToolStripMenuItem();
            ColorWhite.Name = "ColorGreen";
            ColorWhite.Text = "白色";
            ColorWhite.Click += new EventHandler((s, e) =>
            {
                modernCmd.CommandLogger.ForeColor = Color.White;
                StreamWriter config = new StreamWriter("configs/ColorfulLogger.config");
                config.WriteLine($"-*- {Name}插件的配置文件，用于保存上次设置的颜色数据 -*-");
                config.WriteLine("white");
                config.Close();
            });
            ColorfulLogger.DropDownItems.Add(ColorWhite);

            ToolStripMenuItem ColorGreen = new ToolStripMenuItem();
            ColorGreen.Name = "ColorGreen";
            ColorGreen.Text = "绿色";
            ColorGreen.Click += new EventHandler((s, e) =>
            {
                modernCmd.CommandLogger.ForeColor = Color.LightGreen;
                StreamWriter config = new StreamWriter("configs/ColorfulLogger.config");
                config.WriteLine($"-*- {Name}插件的配置文件，用于保存上次设置的颜色数据 -*-");
                config.WriteLine("green");
                config.Close();
            });
            ColorfulLogger.DropDownItems.Add(ColorGreen);

            ToolStripMenuItem ColorRed = new ToolStripMenuItem();
            ColorRed.Name = "ColorRed";
            ColorRed.Text = "红色";
            ColorRed.Click += new EventHandler((s, e) =>
            {
                modernCmd.CommandLogger.ForeColor = Color.Red;
                StreamWriter config = new StreamWriter("configs/ColorfulLogger.config");
                config.WriteLine($"-*- {Name}插件的配置文件，用于保存上次设置的颜色数据 -*-");
                config.WriteLine("red");
                config.Close();
            });
            ColorfulLogger.DropDownItems.Add(ColorRed);

            ToolStripMenuItem ColorYellow = new ToolStripMenuItem();
            ColorYellow.Name = "ColorYellow";
            ColorYellow.Text = "黄色";
            ColorYellow.Click += new EventHandler((s, e) =>
            {
                modernCmd.CommandLogger.ForeColor = Color.Yellow;
                StreamWriter config = new StreamWriter("configs/ColorfulLogger.config");
                config.WriteLine($"-*- {Name}插件的配置文件，用于保存上次设置的颜色数据 -*-");
                config.WriteLine("yellow");
                config.Close();
            });
            ColorfulLogger.DropDownItems.Add(ColorYellow);

            ToolStripMenuItem ColorBlue = new ToolStripMenuItem();
            ColorBlue.Name = "ColorBlue";
            ColorBlue.Text = "蓝色";
            ColorBlue.Click += new EventHandler((s, e) =>
            {
                modernCmd.CommandLogger.ForeColor = Color.LightBlue;
                StreamWriter config = new StreamWriter("configs/ColorfulLogger.config");
                config.WriteLine($"-*- {Name}插件的配置文件，用于保存上次设置的颜色数据 -*-");
                config.WriteLine("blue");
                config.Close();
            });
            ColorfulLogger.DropDownItems.Add(ColorBlue);
        }

        public int Run(string[] args, TextBox CommandLogger)
        {
            string color = args[0];
            if (color == "color") return 0;
            if (color == "white")
            {
                CommandLogger.ForeColor = Color.White;
                StreamWriter config = new StreamWriter("configs/ColorfulLogger.config");
                config.WriteLine($"-*- {Name}插件的配置文件，用于保存上次设置的颜色数据 -*-");
                config.WriteLine(color);
                config.Close();
            }
            else if (color == "green")
            {
                CommandLogger.ForeColor = Color.LightGreen;
                StreamWriter config = new StreamWriter("configs/ColorfulLogger.config");
                config.WriteLine($"-*- {Name}插件的配置文件，用于保存上次设置的颜色数据 -*-");
                config.WriteLine(color);
                config.Close();
            }
            else if (color == "red")
            {
                CommandLogger.ForeColor = Color.Red;
                StreamWriter config = new StreamWriter("configs/ColorfulLogger.config");
                config.WriteLine($"-*- {Name}插件的配置文件，用于保存上次设置的颜色数据 -*-");
                config.WriteLine(color);
                config.Close();
            }
            else if (color == "yellow")
            {
                CommandLogger.ForeColor = Color.Yellow;
                StreamWriter config = new StreamWriter("configs/ColorfulLogger.config");
                config.WriteLine($"-*- {Name}插件的配置文件，用于保存上次设置的颜色数据 -*-");
                config.WriteLine(color);
                config.Close();
            }
            else if (color == "blue")
            {
                CommandLogger.ForeColor = Color.LightBlue;
                StreamWriter config = new StreamWriter("configs/ColorfulLogger.config");
                config.WriteLine($"-*- {Name}插件的配置文件，用于保存上次设置的颜色数据 -*-");
                config.WriteLine(color);
                config.Close();
            }
            else
            {
                CommandLogger.Text += "目前没有该颜色\r\n";
                return 1;
            }
            return 0;
        }
    }
}
