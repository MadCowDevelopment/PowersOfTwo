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

        private readonly ManualResetEvent _pauseResetEvent = new ManualResetEvent(true);
        private readonly ReplayData _replayData;
        private readonly IReplayTarget _replayTarget;

        private CancellationTokenSource _cancellationTokenSource;
        private bool _playing;
        private double SpeedFactor { get; set; }

        #endregion Fields

        #region Constructors

        public ReplayPlayer(ReplayData replayData, IReplayTarget replayTarget)
        {
            _replayData = replayData;
            _replayTarget = replayTarget;

            SpeedFactor = 1;

            InitializeTotalDuration();
        }

        #endregion Constructors

        #region Public Properties

        public int CurrentFrame
        {
            get;
            set;
        }

        private TimeSpan TotalDuration
        {
            get; set;
        }

        public string TotalTime
        {
            get { return TotalDuration.ToString(@"mm\:ss"); }
        }

        public string CurrentTime
        {
            get
            {
                var currentFrame = CurrentFrame >= TotalFrames ? TotalFrames - 1 : CurrentFrame;
                return (_replayData.Events[currentFrame].RecordTime -
                        _replayData.Events.First().RecordTime).ToString(@"mm\:ss");
            }
        }

        public string PlaybackSpeed
        {
            get { return SpeedFactor.ToString("#0.###x"); }
        }

        public int TotalFrames
        {
            get { return _replayData.Events.Count; }
        }

        #endregion Public Properties

        #region Public Methods

        public void DecreaseSpeed()
        {
            if (SpeedFactor <= 0.0625) return;
            SpeedFactor /= 2;
        }

        public void IncreaseSpeed()
        {
            if (SpeedFactor >= 16) return;
            SpeedFactor *= 2;
        }

        public void Initialize()
        {
            CurrentFrame = 0;
            ReplayCurrentFrame();
        }

        public void Pause()
        {
            _pauseResetEvent.Reset();
        }

        public void Play()
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
                    for (CurrentFrame = CurrentFrame; CurrentFrame < TotalFrames; CurrentFrame++)
                    {
                        _pauseResetEvent.WaitOne();
                        if (cancellationToken.IsCancellationRequested) break;

                        ReplayCurrentFrame();

                        if (CurrentFrame + 1 < _replayData.Events.Count)
                        {
                            var currentEventTime = _replayData.Events[CurrentFrame].RecordTime;
                            var nextEventTime = _replayData.Events[CurrentFrame + 1].RecordTime;
                            var sleepTime = (int)((nextEventTime - currentEventTime).TotalMilliseconds / SpeedFactor);
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

        public void Seek(int frame)
        {
            CurrentFrame = frame;
            ResetStateToFrame();
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

        private void ReplayCurrentFrame()
        {
            lock (_replayData.Events)
            {
                _replayData.Events[CurrentFrame].Replay(_replayTarget);
            }
        }

        private void ResetStateToFrame()
        {
            lock (_replayData.Events)
            {
                int index = CurrentFrame;

                if (_replayData.IsSinglePlayer)
                {
                    var cellEventFound = false;
                    var pointEventFound = false;
                    do
                    {
                        var currentEvent = _replayData.Events[index];
                        if (currentEvent is PointsChangedEvent && !pointEventFound)
                        {
                            (currentEvent as PointsChangedEvent).Replay(_replayTarget);
                            pointEventFound = true;
                        }
                        else if (currentEvent is CellsChangedEvent && !cellEventFound)
                        {
                            (currentEvent as CellsChangedEvent).Replay(_replayTarget);
                            cellEventFound = true;
                        }

                        index--;
                    } while (index > 0 && (!cellEventFound || !pointEventFound));
                }
                else
                {
                    var cellEventFound1 = false;
                    var pointEventFound1 = false;
                    var cellEventFound2 = false;
                    var pointEventFound2 = false;
                    do
                    {
                        var currentEvent = _replayData.Events[index];

                        if (currentEvent is PointsChangedEvent)
                        {
                            var pointsChangedEvent = currentEvent as PointsChangedEvent;
                            if (pointsChangedEvent.PlayerNumber == 1 && !pointEventFound1)
                            {
                                pointsChangedEvent.Replay(_replayTarget);
                                pointEventFound1 = true;
                            }
                            else if (pointsChangedEvent.PlayerNumber == 2 && !pointEventFound2)
                            {
                                pointsChangedEvent.Replay(_replayTarget);
                                pointEventFound2 = true;
                            }
                        }
                        else if (currentEvent is CellsChangedEvent)
                        {
                            var cellsChangedEvent = currentEvent as CellsChangedEvent;
                            if (cellsChangedEvent.PlayerNumber == 1 && !cellEventFound1)
                            {
                                cellsChangedEvent.Replay(_replayTarget);
                                cellEventFound1 = true;
                            }
                            else if (cellsChangedEvent.PlayerNumber == 2 && !cellEventFound2)
                            {
                                cellsChangedEvent.Replay(_replayTarget);
                                cellEventFound2 = true;
                            }
                        }

                        index--;
                    } while (index > 0 &&
                             (!cellEventFound1 || !pointEventFound1 || !cellEventFound2 || !pointEventFound2));
                }
            }
        }

        #endregion Private Methods
    }
}