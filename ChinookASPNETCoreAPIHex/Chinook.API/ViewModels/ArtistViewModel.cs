using Chinook.Data.DataModels;
using System.Collections.Generic;

namespace Chinook.API.ViewModels
{
    public class ArtistViewModel
    {
        public int ArtistId { get; set; }
        public string Name { get; set; }

        public IList<Album> Albums { get; set; }
    }
}
