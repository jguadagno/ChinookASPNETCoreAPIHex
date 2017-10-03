using System.Collections.Generic;
using Chinook.Data.DataModels;

namespace Chinook.API.ViewModels
{
    public class PlaylistTrackViewModel
    {
        public int PlaylistId { get; set; }
        public int TrackId { get; set; }

        public Playlist Playlist { get; set; }
        public Track Track { get; set; }
    }
}
