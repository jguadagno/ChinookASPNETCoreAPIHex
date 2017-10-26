using System.Collections.Generic;

namespace Chinook.MockData.DataModels
{
    public class Album
    {
        public int AlbumId { get; set; }
        public string Title { get; set; }
        public int ArtistId { get; set; }

        public virtual ICollection<Track> Tracks { get; set; } = new HashSet<Track>();
        public virtual Artist Artist { get; set; }
    }
}
