using System.Collections.Generic;

namespace Chinook.Data.DataModels
{
    public class Playlist
    {
        public int PlaylistId { get; set; }
        public string Name { get; set; }

        public virtual ICollection<PlaylistTrack> PlaylistTrack { get; set; } = new HashSet<PlaylistTrack>();
    }
}
