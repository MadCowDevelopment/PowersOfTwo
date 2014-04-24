using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using PowersOfTwo.Framework;

namespace PowersOfTwo.Services.Replay
{
    public class ReplayPlayer : ObservableObject
    {
        #region Fields

        private CancellationTokenSource _cancellationTokenSource;
        private readonly ManualResetEvent _pauseResetEvent = new ManualResetEvent(true);
        private readonly ReplayData _replayData;

        private double _speedFactor = 1;

        private bool _playing;

        #endregion Fields

        #region Constructors

        public ReplayPlayer(ReplayData replayData)
        {
            _replayData = replayData;

            InitializeTotalDuration();
        }

        #endregion Constructors

        #region Public Properties

        public int CurrentFrame
        {
            get;
            set;
        }

        public TimeSpan TotalDuration
        {
            get;
            private set;
        }

        public int TotalFrames
        {
            get { return _replayData.Events.Count; }
        }

        #endregion Public Properties

        #region Public Methods

        public void Pause()
        {
            _pauseResetEvent.Reset();
        }

        public void Play(IReplayTarget replayTarget)
        {
            if (_playing)
            {
                _pauseResetEvent.Set();
                return;
            }

            _playing = true;

            _cancellationTokenSource = new CancellationTokenSource();
            var cancellationToken = _cancellationTokenSource.Token;
            var task = new Task(
                () =>
                {
                    for (CurrentFrame = 0; CurrentFrame < TotalFrames; CurrentFrame++)
                    {
                        _pauseResetEvent.WaitOne();
                        if (cancellationToken.IsCancellationRequested) break;

                        _replayData.Events[CurrentFrame].Replay(replayTarget);

                        if (CurrentFrame + 1 < _replayData.Events.Count)
                        {
                            var currentEventTime = _replayData.Events[CurrentFrame].RecordTime;
                            var nextEventTime = _replayData.Events[CurrentFrame + 1].RecordTime;
                            var sleepTime = (int)((nextEventTime - currentEventTime).TotalMilliseconds / _speedFactor);
                            Thread.Sleep(sleepTime);
                        }
                    }
                }, cancellationToken);

            task.ContinueWith(p =>
            {
                _playing = false;
            });

            task.Start();
        }

        public void Stop()
        {
            _pauseResetEvent.Set();
            _cancellationTokenSource.Cancel();
        }

        #endregion Public Methods

        #region Private Methods

        private void InitializeTotalDuration()
        {
            if (_replayData.Events.Count() <= 2) TotalDuration = TimeSpan.FromSeconds(0);
            else TotalDuration = _replayData.Events.Last().RecordTime - _replayData.Events.First().RecordTime;
        }

        #endregion Private Methods


        public void IncreaseSpeed()
        {
            if (_speedFactor >= 16) return;
            _speedFactor *= 2;
        }

        public void DecreaseSpeed()
        {
            if (_speedFactor <= 0.0625) return;
            _speedFactor /= 2;
        }
    }
}