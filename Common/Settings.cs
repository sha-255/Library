using System.IO;
using System;

namespace AdvertisingAgency.Common
{
    public static class Settings
    {
        public static string SqlConnection { get; private set; }
        private static string Path = Environment.CurrentDirectory;
        private static string SubPath = @"appSettings.txt";
        private static string FullPath = Path + @$"\{SubPath}";
        private static bool Exists = File.Exists(FullPath);

        public static void Apply()
        {
            if (!Exists)
                File.Create("appSettings.txt");
            SqlConnection = new StreamReader(FullPath).ReadToEnd();
        }
    }
}