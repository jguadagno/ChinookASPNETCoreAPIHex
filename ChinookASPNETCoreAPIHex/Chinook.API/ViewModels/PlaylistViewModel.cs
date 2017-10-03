using System.Collections.Generic;
using Chinook.Data.DataModels;

namespace Chinook.API.ViewModels
{
    public class PlaylistViewModel
    {
        public int PlaylistId { get; set; }
        public string Name { get; set; }
        public IList<Track> Tracks { get; set; }

        public IList<PlaylistTrack> PlaylistTracks { get; set; }

        public ICollection<Track> Track { get; set; }
    }
}
