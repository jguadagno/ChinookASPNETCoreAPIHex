using System.Collections.Generic;
using Chinook.Domain.Entities;

namespace Chinook.Domain.ViewModels
{
    public class ArtistViewModel
    {
        public int ArtistId { get; set; }
        public string Name { get; set; }

        public IList<AlbumViewModel> Albums { get; set; }
    }
}
