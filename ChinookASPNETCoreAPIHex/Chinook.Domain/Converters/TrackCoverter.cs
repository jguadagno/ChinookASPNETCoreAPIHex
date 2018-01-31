using System.Collections.Generic;
using Chinook.Domain.Entities;
using Chinook.Domain.ViewModels;

namespace Chinook.Domain.Converters
{
    public class TrackCoverter
    {
        public static TrackViewModel Convert(Track track)
        {
            var trackViewModel = new TrackViewModel()
            {
                TrackId = track.TrackId,
                Name = track.Name,
                AlbumId = track.AlbumId,
                MediaTypeId = track.MediaTypeId,
                GenreId = track.GenreId,
                Composer = track.Composer,
                Milliseconds = track.Milliseconds,
                Bytes = track.Bytes,
                UnitPrice = track.UnitPrice
            };
            return trackViewModel;
        }
        
        public static List<TrackViewModel> ConvertList(List<Track> albums)
        {
            List<TrackViewModel> albumViewModels = new List<TrackViewModel>();
            foreach(var t in albums)
            {
                var albumViewModel = new TrackViewModel
                {
                    TrackId = t.TrackId,
                    Name = t.Name,
                    AlbumId = t.AlbumId,
                    MediaTypeId = t.MediaTypeId,
                    GenreId = t.GenreId,
                    Composer = t.Composer,
                    Milliseconds = t.Milliseconds,
                    Bytes = t.Bytes,
                    UnitPrice = t.UnitPrice
                };
                albumViewModels.Add(albumViewModel);
            }

            return albumViewModels;
        }
    }
}