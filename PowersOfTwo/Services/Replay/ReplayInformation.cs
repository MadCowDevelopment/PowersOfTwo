using System.IO;

namespace PowersOfTwo.Services.Replay
{
    public class ReplayInformation
    {
        #region Constructors

        public ReplayInformation(string fullpath)
        {
            Fullpath = fullpath;
            Filename = Path.GetFileName(Fullpath);
        }

        #endregion Constructors

        #region Public Properties

        public string Filename
        {
            get; private set;
        }

        public string Fullpath
        {
            get; private set;
        }

        #endregion Public Properties
    }
}