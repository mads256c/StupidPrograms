using System;
using System.Threading.Tasks;
using System.Diagnostics;
using System.IO;

namespace RunAllPrograms
{
    public static class ProcessAsync
    {
        public static Task RunProcessAsync(string fileName)
        {
            // there is no non-generic TaskCompletionSource
            var tcs = new TaskCompletionSource<bool>();

            var process = new Process
            {
                StartInfo = { FileName = fileName },
                EnableRaisingEvents = true
            };

            process.Exited += (sender, args) =>
            {
                tcs.SetResult(true);
                process.Dispose();
            };

            process.Start();

            return tcs.Task;
        }
    }

    class Program
    {
        public static char[] separator = { ';' };
        static void Main(string[] args)
        {
            Console.WriteLine(Environment.GetEnvironmentVariable("path"));
            foreach (string path in (Environment.GetEnvironmentVariable("path") + @";C:\Windows\SysWOW64\").Split(separator, StringSplitOptions.RemoveEmptyEntries))
            {
                if (Directory.Exists(path))
                {
                    foreach (string file in Directory.GetFiles(path))
                    {
                            try
                            {
                                ProcessAsync.RunProcessAsync(file);
                                Console.WriteLine($"Ran {file}");
                            }
                            catch (Exception)
                            {

                            }
                    }
                }
            }
            Console.Read();
        }
    }
}
