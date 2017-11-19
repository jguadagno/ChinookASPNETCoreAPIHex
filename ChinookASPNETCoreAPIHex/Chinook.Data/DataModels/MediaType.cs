using System.Collections.Generic;

namespace Chinook.Data.DataModels
{
    public sealed class MediaType
    {
        public int MediaTypeId { get; set; }
        public string Name { get; set; }

        public ICollection<Track> Tracks { get; set; } = new HashSet<Track>();
    }
}
