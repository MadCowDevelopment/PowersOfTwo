using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace PowersOfTwo.Services.Replay
{
    public class ReplayDiscoverer
    {
        public async Task<IEnumerable<ReplayInformation>> GetReplayFiles()
        {
            var task = new Task<IEnumerable<ReplayInformation>> (
                () => Directory.GetFiles(Folders.Replays, "*.potr").Select(filename => new ReplayInformation(filename)));
            task.Start();
            return await task;
        }
    }
}
