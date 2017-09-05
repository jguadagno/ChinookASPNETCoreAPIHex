using System.Collections.Generic;

namespace Chinook.Data.DataModels
{
    public class MediaType
    {
        public int MediaTypeId { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Track> Track { get; set; } = new HashSet<Track>();
    }
}
