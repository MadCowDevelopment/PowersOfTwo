using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Timers;
using Ionic.Zip;
using Newtonsoft.Json;

namespace PowersOfTwo.Services.Replay
{
    public class ReplayRecorder
    {
        private readonly List<ReplayEvent> _events = new List<ReplayEvent>();

        private readonly JsonSerializerSettings _settings
            = new JsonSerializerSettings
              {
                  TypeNameHandling =
                      TypeNameHandling.All
              };

        public void Record(ReplayEvent action)
        {
            _events.Add(action);
        }

        public void Save(string filename)
        {
            var serializedString = JsonConvert.SerializeObject(
                _events,
                typeof(List<ReplayEvent>),
                Formatting.Indented,
                _settings);

            File.Delete(filename);

            using (var zip = new ZipFile(filename))
            {
                zip.AddEntry("content.json", serializedString, Encoding.UTF8);
                zip.Save(filename);
            }
        }
    }
}
