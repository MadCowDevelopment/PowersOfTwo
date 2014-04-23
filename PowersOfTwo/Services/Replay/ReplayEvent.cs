using System;

namespace PowersOfTwo.Services.Replay
{
    public abstract class ReplayEvent
    {
        public DateTime RecordTime { get; private set; }

        protected ReplayEvent()
        {
            RecordTime = DateTime.Now;
        }

        public abstract void Replay(IReplayTarget replayTarget);
    }
}