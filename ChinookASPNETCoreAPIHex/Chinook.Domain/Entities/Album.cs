using System.Collections.Generic;

namespace Chinook.Domain.Entities
{
    public sealed class Album
    {
        public int AlbumId { get; set; }
        public string Title { get; set; }
        public int ArtistId { get; set; }
        public string ArtistName { get; set; }
        public ICollection<Track> Tracks { get; set; }
        public Artist Artist { get; set; }
    }
}
