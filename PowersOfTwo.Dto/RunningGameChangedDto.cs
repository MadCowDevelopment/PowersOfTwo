namespace PowersOfTwo.Dto
{
    public class RunningGameChangedDto
    {
        public RunningGameDto RunningGame { get; private set; }
        public RunningGameChange Change { get; private set; }

        public RunningGameChangedDto(RunningGameDto runningGame, RunningGameChange change)
        {
            RunningGame = runningGame;
            Change = change;
        }
    }

    public enum RunningGameChange
    {
        Added,
        Removed
    }
}
