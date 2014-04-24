﻿using PowersOfTwo.ViewModels;

namespace PowersOfTwo.Services.Replay
{
    public class GameStartedEvent : ReplayEvent
    {
        #region Constructors

        public GameStartedEvent(string player1, int points)
        {
            Player1 = player1;
            Points = points;
        }

        public GameStartedEvent(string player1, string player2, int points)
        {
            Player1 = player1;
            Player2 = player2;
            Points = points;
        }

        protected GameStartedEvent()
        {
        }

        #endregion Constructors

        #region Public Properties

        public string Player1
        {
            get; set;
        }

        public string Player2
        {
            get; set;
        }

        public int Points
        {
            get; set;
        }

        #endregion Public Properties

        #region Public Methods

        public override void Replay(IReplayTarget replayTarget)
        {
            replayTarget.Player1 = new PlayerViewModel(Player1) { Points = Points };
            if (string.IsNullOrEmpty(Player1)) replayTarget.Player2 = new PlayerViewModel(Player2) { Points = Points };
        }

        #endregion Public Methods
    }
}