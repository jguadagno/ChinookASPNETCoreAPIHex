using System.Collections.Generic;
using Chinook.Data.DataModels;

namespace Chinook.API.ViewModels
{
    public class MediaTypeViewModel
    {
        public int MediaTypeId { get; set; }
        public string Name { get; set; }

        public IList<Track> Tracks { get; set; }
    }
}
