using System;
using System.IO;

namespace PowersOfTwo.Services
{
    public static class Folders
    {
        private static readonly string AppDataPath = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
            "MadCowDevelopment",
            "PowersOfTwo");

        public static string AppData
        {
            get
            {
                Directory.CreateDirectory(AppDataPath);
                return AppDataPath;
            }
        }
    }
}
