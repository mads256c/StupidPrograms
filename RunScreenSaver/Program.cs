using Microsoft.Win32;
using System;
using System.Diagnostics;
using System.IO;
using System.Collections.Generic;

namespace RunScreenSaver
{
    public class Value
    {
        public Value(object val, RegistryValueKind regvalue)
        {
            value = val;
            valueKind = regvalue;
        }
        public object value;
        public RegistryValueKind valueKind;

    }

    static class Program
    {
        static Dictionary<string, Value> dictionary = new Dictionary<string, Value>()
        {
            {"DisplayString", new Value("haha", RegistryValueKind.String) },
            {"FontFace", new Value("Comic Sans MS", RegistryValueKind.String) },
            {"DisplayTime", new Value(0, RegistryValueKind.DWord) },
            {"SurfaceType", new Value(1, RegistryValueKind.DWord) },
            {"MeshQuality", new Value(0, RegistryValueKind.DWord) },
        };
        const string SearchDirectory = @"C:\Windows\WinSxs\";
        const string ScreensaverBinary = @"ssText3d.scr";
        const string RegistryPath = @"Software\Microsoft\Windows\CurrentVersion\Screensavers\ssText3d";

 

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
                    foreach(KeyValuePair<string, Value> entry in dictionary)
                    {
                        key.SetValue(entry.Key, entry.Value.value, entry.Value.valueKind);
                    }
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
