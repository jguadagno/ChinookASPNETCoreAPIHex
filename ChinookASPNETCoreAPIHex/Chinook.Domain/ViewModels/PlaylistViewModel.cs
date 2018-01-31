using System.Collections.Generic;
using Chinook.Domain.Entities;

namespace Chinook.Domain.ViewModels
{
    public class PlaylistViewModel
    {
        public int PlaylistId { get; set; }
        public string Name { get; set; }
        public IList<Track> Tracks { get; set; }

        public IList<PlaylistTrackViewModel> PlaylistTracks { get; set; }

        public ICollection<TrackViewModel> Track { get; set; }
    }
}
