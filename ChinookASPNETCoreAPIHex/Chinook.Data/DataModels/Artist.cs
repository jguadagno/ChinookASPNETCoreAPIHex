using System.Collections.Generic;

namespace Chinook.Data.DataModels
{
    public class Artist
    {
        public int ArtistId { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Album> Album { get; set; } = new HashSet<Album>();
    }
}
