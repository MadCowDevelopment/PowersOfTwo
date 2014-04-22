namespace PowersOfTwo.Services.Replay
{
    public abstract class ReplayEvent
    {
        public abstract void Replay(IReplayTarget replayTarget);
    }
}