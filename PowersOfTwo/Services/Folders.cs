using System;
using System.IO;

namespace PowersOfTwo.Services
{
    public static class Folders
    {
        #region Fields

        private static readonly string AppDataPath = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
            "MadCowDevelopment",
            "PowersOfTwo");
        private static readonly string ReplaysPath = Path.Combine(AppDataPath, "Replays");

        #endregion Fields

        public static string AppData
        {
            get
            {
                Directory.CreateDirectory(AppDataPath);
                return AppDataPath;
            }
        }

        public static string Replays
        {
            get
            {
                Directory.CreateDirectory(ReplaysPath);
                return ReplaysPath;
            }
        }
    }
}