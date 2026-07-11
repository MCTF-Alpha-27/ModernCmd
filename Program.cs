using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using Microsoft.VisualBasic.Devices;
using PluginFrame;

namespace ModernCmd
{
    public interface ICommand
    {
        string CommandName { get; }
        string CommandDescription { get; }
        int Run(string[] args, TextBox commandLogger);
    }

    public class TestCommand : ICommand
    {
        public string CommandName => "test";
        public string CommandDescription => "一个测试命令";

        public int Run(string[] args, TextBox commandLogger)
        {
            commandLogger.Text += "Hello World\r\n";
            return 0;
        }
    }

    public class HelpCommand : ICommand
    {
        public string CommandName => "help";
        public string CommandDescription => "键入help <命令>显示一个命令的描述，单独键入help显示所有命令";

        public int Run(string[] args, TextBox commandLogger)
        {
            int count = 0;
            if (args.Length == 0)
            {
                commandLogger.Text += $"共有{ModernCmd.AllCommands.Count}个命令，其中{ModernCmd.PluginCommands.Count}个来自插件:\r\n";
                foreach (string command in ModernCmd.AllCommands)
                {
                    count++;
                    if (ModernCmd.PluginCommands.Contains(command))
                    {
                        commandLogger.Text += $"{count}. {command} (来自插件)\r\n";
                        continue;
                    }
                    commandLogger.Text += $"{count}. {command}\r\n";
                }
            }
            else
            {
                string commandName = args[0];
                Type[] types = Assembly.GetExecutingAssembly().GetTypes();
                foreach (Type type in types)
                {
                    if (type.GetInterface("ICommand") != null)
                    {
                        ICommand cmdInstance = Activator.CreateInstance(type) as ICommand;
                        if (cmdInstance.CommandName == commandName)
                        {
                            commandLogger.Text += $"{cmdInstance.CommandName}: {cmdInstance.CommandDescription}\r\n";
                            return 0;
                        }
                    }
                }
                try
                {
                    string[] pluginFiles = Program.GetAllPluginFiles();
                    foreach (string pluginFile in pluginFiles)
                    {
                        Assembly assembly = Assembly.LoadFrom(pluginFile);
                        Type[] pluginTypes = assembly.GetTypes();
                        foreach (Type type in pluginTypes)
                        {
                            if (type.GetInterface("ICommand") != null)
                            {
                                ICommand cmdInstance = Activator.CreateInstance(type) as ICommand;
                                if (cmdInstance.CommandName == commandName)
                                {
                                    commandLogger.Text += $"{cmdInstance.CommandName}: {cmdInstance.CommandDescription}\r\n";
                                    return 0;
                                }
                            }
                        }
                    }
                }
                catch (Exception e)
                {
                    commandLogger.Text += "运行插件时发生错误\r\n";
                    commandLogger.Text += $"{e.Message}\r\n{e.StackTrace}\r\n";
                    return 1;
                }
                MessageBox.Show($"没有找到名为{commandName}的命令，请检查输入是否有误\n如果该命令是一个插件提供的，请检查插件是否成功启用", "命令未找到", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return 1;
            }
            return 0;
        }
    }

    internal static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            ModernCmd modernCmd = new ModernCmd();
            Type[] selfTypes = Assembly.GetExecutingAssembly().GetTypes();
            foreach (Type type in selfTypes)
            {
                if (type.GetInterface("ICommand") != null)
                {
                    ICommand command = Activator.CreateInstance(type) as ICommand;
                    ModernCmd.AllCommands.Add(command.CommandName);
                }
            }
            try
            {
                string[] pluginFiles = GetAllPluginFiles();
                foreach (string pluginFile in pluginFiles)
                {
                    Type[] types = Assembly.LoadFrom(pluginFile).GetTypes();
                    List<string> pluginCommands = new List<string>();
                    foreach (Type type in types)
                    {
                        if (type.GetInterface("ICommand") != null)
                        {
                            ICommand command = Activator.CreateInstance(type) as ICommand;
                            pluginCommands.Add(command.CommandName);
                            ModernCmd.PluginCommands.Add(command.CommandName);
                            ModernCmd.AllCommands.Add(command.CommandName);
                        }
                    }
                    foreach (Type type in types)
                    {
                        if (type.GetInterface("IPlugin") != null)
                        {
                            IPlugin plugin = Activator.CreateInstance(type) as IPlugin;
                            ToolStripMenuItem pluginItems = new ToolStripMenuItem();
                            pluginItems.Name = type.Name;
                            pluginItems.Text = plugin.Name;
                            ToolStripMenuItem pluginInfos = new ToolStripMenuItem();
                            pluginInfos.Name = type.Name + "_info";
                            pluginInfos.Text = "插件信息";
                            ToolStripMenuItem disablePlugin = new ToolStripMenuItem();
                            disablePlugin.Name = type.Name;
                            disablePlugin.Text = "禁用此插件";
                            Computer computer = new Computer();
                            disablePlugin.Click += new EventHandler((sender, e) =>
                            {
                                computer.FileSystem.RenameFile(pluginFile, pluginFile.Split('\\')[1] + ".disabled");
                                MessageBox.Show("已禁用此插件，重启后生效", "需要重启",
                                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                            });
                            if (pluginCommands.Count == 0)
                            {
                                pluginCommands.Add("无");
                            }
                            pluginInfos.Click += new EventHandler((sender, e) =>
                            {
                                MessageBox.Show(
                                    $"插件注册名：{type.Name}\n" +
                                    $"插件名称：{plugin.Name}\n" +
                                    $"插件版本：v{plugin.Version}\n" +
                                    $"插件作者：{plugin.Author}\n" +
                                    $"插件命令：{string.Join(", ", pluginCommands)}\n" +
                                    $"\n{plugin.Description}",
                                    "插件信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            });
                            pluginItems.DropDownItems.Add(pluginInfos);
                            modernCmd.PluginsToolStripMenuItem.DropDownItems.Add(pluginItems);
                            pluginItems.DropDownItems.Add(disablePlugin);
                            modernCmd.PluginsToolStripMenuItem.DropDownItems.Add(pluginItems);
                            plugin.Main(modernCmd);
                        }
                    }
                }
                string[] disabledPluginFiles = Directory.GetFiles("plugins", "*.disabled");
                foreach (string disabledPluginFile in disabledPluginFiles)
                {
                    Computer computer = new Computer();
                    ToolStripMenuItem disabledPlugin = new ToolStripMenuItem();
                    disabledPlugin.Name = disabledPluginFile;
                    disabledPlugin.Text = disabledPluginFile.Split('\\')[1].Replace(".disabled", "") + "（已禁用）";
                    ToolStripMenuItem enablePlugin = new ToolStripMenuItem();
                    enablePlugin.Name = disabledPluginFile;
                    enablePlugin.Text = "启用此插件";
                    enablePlugin.Click += new EventHandler((sender, e) =>
                    {
                        computer.FileSystem.RenameFile(disabledPluginFile, disabledPluginFile.Split('\\')[1].Replace(".disabled", ""));
                        MessageBox.Show("已启用此插件，重启后生效", "需要重启",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                    });
                    disabledPlugin.DropDownItems.Add(enablePlugin);
                    modernCmd.PluginsToolStripMenuItem.DropDownItems.Add(disabledPlugin);
                }
            }
            catch (Exception e)
            {
                modernCmd.CommandLogger.Text += "运行插件时发生错误\r\n";
                modernCmd.CommandLogger.Text += $"{e.Message}\r\n{e.StackTrace}\r\n";
            }
            Application.Run(modernCmd);
        }

        public static string[] GetAllPluginFiles()
        {
            return Directory.GetFiles("plugins", "*.dll");
        }

        public static int RunCommand(string cmd, TextBox commandLogger)
        {
            if (string.IsNullOrEmpty(cmd) || string.IsNullOrWhiteSpace(cmd))
            {
                return 0;
            }
            string[] command = cmd.Split(' ');
            Type[] types = Assembly.GetExecutingAssembly().GetTypes();
            foreach (Type type in types)
            {
                if (type.GetInterface("ICommand") != null)
                {
                    ICommand cmdInstance = Activator.CreateInstance(type) as ICommand;
                    if (cmdInstance.CommandName == command[0])
                    {
                        if (command.Length == 1)
                        {
                            return cmdInstance.Run(Array.Empty<string>(), commandLogger);
                        }
                        return cmdInstance.Run(cmd.Replace(command[0] + " ", "").Split(' '), commandLogger);
                    }
                }
            }
            try
            {
                string[] pluginFiles = GetAllPluginFiles();
                foreach (string pluginFile in pluginFiles)
                {
                    Assembly assembly = Assembly.LoadFrom(pluginFile);
                    Type[] pluginTypes = assembly.GetTypes();
                    foreach (Type type in pluginTypes)
                    {
                        if (type.GetInterface("ICommand") != null)
                        {
                            ICommand cmdInstance = Activator.CreateInstance(type) as ICommand;
                            if (cmdInstance.CommandName == command[0])
                            {
                                if (command.Length == 1)
                                {
                                    return cmdInstance.Run(Array.Empty<string>(), commandLogger);
                                }
                                return cmdInstance.Run(cmd.Replace(command[0] + " ", "").Split(' '), commandLogger);
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                commandLogger.Text += "运行插件时发生错误\r\n";
                commandLogger.Text += $"{e.Message}\r\n{e.StackTrace}\r\n";
            }
            MessageBox.Show($"没有找到名为{cmd}的命令，请检查输入是否有误\n如果该命令是一个插件提供的，请检查插件是否成功启用", "命令未找到", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return 1;
        }
    }
}
