using System.Collections.Generic;
using Chinook.Domain.Entities;

namespace Chinook.Domain.ViewModels
{
    public class MediaTypeViewModel
    {
        public int MediaTypeId { get; set; }
        public string Name { get; set; }

        public IList<TrackViewModel> Tracks { get; set; }
    }
}
