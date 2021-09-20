using System.Collections.Generic;
using System.Threading.Tasks;
using Pairs.Models;

namespace Pairs.Data
{
    public class SpeakerRepository
    {
        private readonly IList<Speaker> speakers;

        public SpeakerRepository()
        {
            speakers = new List<Speaker>();
        }

        public Task<IList<Speaker>> ListAsync() => Task.FromResult(speakers);
    }
}
