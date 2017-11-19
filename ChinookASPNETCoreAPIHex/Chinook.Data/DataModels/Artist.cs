using System.Collections.Generic;

namespace Chinook.Data.DataModels
{
    public sealed class Artist
    {
        public int ArtistId { get; set; }
        public string Name { get; set; }

        public ICollection<Album> Albums { get; set; } = new HashSet<Album>();
    }
}
