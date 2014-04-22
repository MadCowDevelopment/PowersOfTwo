using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace PowersOfTwo.Services.Replay
{
    public class ReplayPlayer
    {
        private readonly IEnumerable<ReplayEvent> _events;
        private readonly CancellationTokenSource _cancellationTokenSource = new CancellationTokenSource();
        private bool _paused;

        public ReplayPlayer(IEnumerable<ReplayEvent> events)
        {
            _events = events;
            Delay = 1000;
        }

        public void Play(IReplayTarget replayTarget)
        {
            var cancellationToken = _cancellationTokenSource.Token;
            var task = new Task(
                () =>
                {
                    foreach (var replayEvent in _events)
                    {
                        Thread.Sleep(Delay);
                        while (_paused)
                        {
                            if (cancellationToken.IsCancellationRequested) break;
                            Thread.Sleep(100);
                        }

                        if (cancellationToken.IsCancellationRequested) break;

                        replayEvent.Replay(replayTarget);
                    }
                }, cancellationToken);
            task.Start();
        }

        public void Pause()
        {
            _paused = !_paused;
        }

        public void Stop()
        {
            _cancellationTokenSource.Cancel();
        }

        public int Delay { get; set; }
    }
}
