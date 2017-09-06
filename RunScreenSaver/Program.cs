using Microsoft.Win32;
using System;
using System.Diagnostics;
using System.IO;

namespace RunScreenSaver
{
    static class Program
    {
        const string SearchDirectory = @"C:\Windows\WinSxs\";
        const string ScreensaverBinary = @"ssText3d.scr";
        const string RegistryPath = @"Software\Microsoft\Windows\CurrentVersion\Screensavers\ssText3d";

        const string ScreensaverText = "haha";
        const string FontFace = "Comic Sans MS";

        static void Main(string[] args)
        {
            Console.WriteLine("Finding screensaver, this might take some time.");
            string[] files = Directory.GetFiles(SearchDirectory, ScreensaverBinary, SearchOption.AllDirectories);
            foreach (string file in files)
            {
                Console.WriteLine(file);
            }
            if (files.Length > 0)
            {
                RegistryKey key = Registry.CurrentUser.OpenSubKey(RegistryPath, true);
                if (key == null)
                {
                    key = Registry.CurrentUser.CreateSubKey(RegistryPath, true);
                }
                if (key != null)
                {
                    key.SetValue("DisplayString", ScreensaverText, RegistryValueKind.String);
                    key.SetValue("FontFace", FontFace, RegistryValueKind.String);
                    key.SetValue("DisplayTime", 0, RegistryValueKind.DWord);
                    key.SetValue("SurfaceType", 1, RegistryValueKind.DWord);
                    key.SetValue("MeshQuality", 0, RegistryValueKind.DWord);
                }
                else
                {
                    Console.WriteLine("Key was null. Fucking Windows!");
                }
                while (true)
                {
                    if (Process.GetProcessesByName(ScreensaverBinary).Length < 1)
                    {
                        Process test = new Process
                        {
                            StartInfo = new ProcessStartInfo() { FileName = files[0] },
                        };
                        test.Start();
                    }
                }
            }
            else
            {
                Console.WriteLine($"Could not find screensaver: {ScreensaverBinary}");
            }
            Console.Read();
        }
    }
}
