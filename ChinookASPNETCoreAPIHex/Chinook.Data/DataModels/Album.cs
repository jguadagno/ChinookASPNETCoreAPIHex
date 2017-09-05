using System.Collections.Generic;

namespace Chinook.Data.DataModels
{
    public class Album
    {
        public int AlbumId { get; set; }
        public string Title { get; set; }
        public int ArtistId { get; set; }

        public virtual ICollection<Track> Track { get; set; } = new HashSet<Track>();
        public virtual Artist Artist { get; set; }
    }
}
