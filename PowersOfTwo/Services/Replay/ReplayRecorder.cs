using System;
using System.IO;
using System.Text;

using Ionic.Zip;

using Newtonsoft.Json;

namespace PowersOfTwo.Services.Replay
{
    public class ReplayRecorder
    {
        #region Fields

        private readonly ReplayData _replayData = new ReplayData();
        private readonly JsonSerializerSettings _settings = new JsonSerializerSettings
              {
                  TypeNameHandling =
                      TypeNameHandling.All
              };

        #endregion Fields

        #region Public Methods

        public void Record(ReplayEvent replayEvent)
        {
            _replayData.AddEvent(replayEvent);
        }

        public void Save()
        {
            if (_replayData == null || _replayData.Events.Count < 2)
                throw new InvalidOperationException("No events recorded.");

            var filename = Path.Combine(Folders.Replays, _replayData.GetFilename());

            var serializedString = JsonConvert.SerializeObject(
                _replayData,
                typeof(ReplayData),
                Formatting.Indented,
                _settings);

            File.Delete(filename);

            using (var zip = new ZipFile(filename))
            {
                zip.AddEntry("content.json", serializedString, Encoding.UTF8);
                zip.Save(filename);
            }
        }

        #endregion Public Methods
    }
}