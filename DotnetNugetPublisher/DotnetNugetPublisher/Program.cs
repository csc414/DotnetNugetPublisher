using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;

namespace DotnetNugetPublisher
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                Console.WriteLine("参数无效，请在解决方案资源管理器选中一个项目再进行操作。");
                return;
            }

            var projectDir = args[args.Length - 1].TrimEnd('\\');
            if (!Directory.Exists(projectDir))
            {
                Console.WriteLine("{0}目录不存在，请在解决方案资源管理器选中一个项目再进行操作。", projectDir);
                return;
            }

            var pushAdditional = string.Join(" ", args, 0, args.Length - 1);
            var packAdditional = ConfigurationManager.AppSettings["AdditionalOptions"];
            var configuration = ConfigurationManager.AppSettings["Configuration"];
            var source = ConfigurationManager.AppSettings["Source"];
            var apiKey = ConfigurationManager.AppSettings["ApiKey"];
            var dir = new DirectoryInfo(Path.Combine(Environment.CurrentDirectory, "Temp"));
            if (!dir.Exists)
                dir.Create();
            var publishDir = dir.CreateSubdirectory(Guid.NewGuid().ToString("N"));
            ExcuteCommand($"dotnet pack -c {configuration} -o {publishDir.FullName}{packAdditional} {projectDir}", (p, message) => Console.WriteLine(message));
            var files = publishDir.GetFiles();
            if(files.Count() > 0)
            {
                foreach (var file in files)
                {
                    ExcuteCommand($"dotnet nuget push {file.FullName} -s {source} -k {apiKey} {pushAdditional}", (p, message) => {
                        Console.WriteLine(message);
                        if (message.StartsWith("error:"))
                            p.Kill();
                    });
                }
            }
            publishDir.Delete(true);
        }

        static void ExcuteCommand(string command, Action<Process, string> outputAction = null)
        {
            using (var process = new Process())
            {
                ProcessStartInfo startInfo = new ProcessStartInfo();
                var commands = command.Split(new[] { ' ' }, 2);
                startInfo.FileName = commands[0];
                startInfo.Arguments = commands[1];
                startInfo.UseShellExecute = false; //不使用系统外壳程序启动  
                startInfo.CreateNoWindow = true; //不创建窗口  
                startInfo.RedirectStandardOutput = true;
                process.StartInfo = startInfo;
                process.OutputDataReceived += (sender, e) =>
                {
                    if (e.Data == null)
                        return;
                    outputAction?.Invoke(process, e.Data);
                };
                process.Start();
                process.BeginOutputReadLine();
                process.WaitForExit(30000);
            }
        }
    }
}
