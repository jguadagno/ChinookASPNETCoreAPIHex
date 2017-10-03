using System.Collections.Generic;
using Chinook.Data.DataModels;

namespace Chinook.API.ViewModels
{
    public class AlbumViewModel
    {
        public int AlbumId { get; set; }
        public string Title { get; set; }
        public int ArtistId { get; set; }
        public string ArtistName { get; set; }

        public Artist Artist { get; set; }
        public IList<Track> Tracks { get; set; }

    }
}
