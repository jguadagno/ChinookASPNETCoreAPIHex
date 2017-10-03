using System.Collections.Generic;

namespace Chinook.Data.DataModels
{
    public class Genre
    {
        public int GenreId { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Track> Tracks { get; set; } = new HashSet<Track>();
    }
}
