using Ionic.Zip;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace PowersOfTwo.Services.Replay
{
    public class ReplayLoader
    {
        private readonly JsonSerializerSettings _settings
            = new JsonSerializerSettings
              {
                  TypeNameHandling =
                      TypeNameHandling.All
              };

        public List<ReplayEvent> Load(string filename)
        {
            using (var zip = new ZipFile(filename))
            {
                var entry = zip.Entries.SingleOrDefault(p => p.FileName == "content.json");
                if (entry == null) throw new InvalidOperationException("Could not find content.json");

                StreamReader streamReader = null;
                MemoryStream memoryStream = null;
                try
                {
                    memoryStream = new MemoryStream();
                    entry.Extract(memoryStream);
                    var array = memoryStream.ToArray();
                    memoryStream = new MemoryStream(array);
                    memoryStream.Seek(0, SeekOrigin.Begin);

                    streamReader = new StreamReader(memoryStream);
                    var content = streamReader.ReadToEnd();
                    return JsonConvert.DeserializeObject<List<ReplayEvent>>(content, _settings);
                }
                finally
                {
                    if (memoryStream != null) memoryStream.Dispose();
                    if (streamReader != null) streamReader.Dispose();
                }
            }
        }

    }
}
