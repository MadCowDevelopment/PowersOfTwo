using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace PowersOfTwo.Services.Replay
{
    public class ReplayPlayer
    {
        private readonly List<ReplayEvent> _events;
        private readonly CancellationTokenSource _cancellationTokenSource = new CancellationTokenSource();
        private bool _paused;
        private int _currentFrame;
        private TimeSpan _currentTime;

        public ReplayPlayer(IEnumerable<ReplayEvent> events)
        {
            _events = events.ToList();
            SpeedFactor = 1;

            InitializeTotalDuration();
        }

        private void InitializeTotalDuration()
        {
            if (_events.Count <= 2) TotalDuration = TimeSpan.FromSeconds(0);
        }

        public void Play(IReplayTarget replayTarget)
        {
            var cancellationToken = _cancellationTokenSource.Token;
            var task = new Task(
                () =>
                {
                    _currentFrame = 0;
                    for (int i = _currentFrame; i < _events.Count; i++)
                    {
                        while (_paused)
                        {
                            if (cancellationToken.IsCancellationRequested) break;
                            Thread.Sleep(100);
                        }

                        if (cancellationToken.IsCancellationRequested) break;

                        _events[i].Replay(replayTarget);

                        if (i + 1 < _events.Count)
                        {
                            var currentEventTime = _events[i].RecordTime;
                            var nextEventTime = _events[i + 1].RecordTime;
                            var sleepTime = (int)((nextEventTime - currentEventTime).TotalMilliseconds / SpeedFactor);
                            Thread.Sleep(sleepTime);
                        }
                    }
                }, cancellationToken);
            task.Start();
        }

        public TimeSpan TotalDuration { get; private set; }

        public int TotalFrames { get; private set; }

        public TimeSpan CurrentTime
        {
            get
            {
                return _currentTime;
            }

            set
            {
                _currentTime = value;
            }
        }

        public void Pause()
        {
            _paused = !_paused;
        }

        public void Stop()
        {
            _cancellationTokenSource.Cancel();
        }

        public double SpeedFactor { get; set; }
    }
}
