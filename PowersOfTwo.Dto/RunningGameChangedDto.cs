namespace PowersOfTwo.Dto
{
    #region Enumerations

    public enum RunningGameChange
    {
        Added,
        Removed
    }

    #endregion Enumerations

    public class RunningGameChangedDto
    {
        #region Constructors

        public RunningGameChangedDto(RunningGameDto runningGame, RunningGameChange change)
        {
            RunningGame = runningGame;
            Change = change;
        }

        #endregion Constructors

        #region Public Properties

        public RunningGameChange Change
        {
            get; private set;
        }

        public RunningGameDto RunningGame
        {
            get; private set;
        }

        #endregion Public Properties
    }
}