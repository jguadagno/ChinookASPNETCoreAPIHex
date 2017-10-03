using System.Collections.Generic;
using Chinook.Data.DataModels;

namespace Chinook.API.ViewModels
{
    public class TrackViewModel
    {
        public int TrackId { get; set; }
        public string Name { get; set; }
        public int AlbumId { get; set; }
        public string AlbumName { get; set; }
        public int MediaTypeId { get; set; }
        public string MediaTypeName { get; set; }
        public int? GenreId { get; set; }
        public string GenreName { get; set; }
        public string Composer { get; set; }
        public int Milliseconds { get; set; }
        public int Bytes { get; set; }
        public decimal UnitPrice { get; set; }

        public virtual IList<InvoiceLine> InvoiceLines { get; set; }
        public virtual IList<PlaylistTrack> PlaylistTracks { get; set; }
        public virtual Album Album { get; set; }
        public virtual Genre Genre { get; set; }
        public virtual MediaType MediaType { get; set; }
    }
}
