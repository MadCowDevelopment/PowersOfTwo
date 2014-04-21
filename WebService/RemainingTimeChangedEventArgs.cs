using System;

namespace WebService
{
    internal class RemainingTimeChangedEventArgs : EventArgs
    {
        #region Constructors

        public RemainingTimeChangedEventArgs(GameInformation gameInformation, int remainingTime)
        {
            GameInformation = gameInformation;
            RemainingTime = remainingTime;
        }

        #endregion Constructors

        #region Public Properties

        public GameInformation GameInformation
        {
            get; set;
        }

        public int RemainingTime
        {
            get; set;
        }

        #endregion Public Properties
    }
}