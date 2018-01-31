using System.Collections.Generic;
using Chinook.Domain.Entities;
using Chinook.Domain.ViewModels;

namespace Chinook.Domain.Converters
{
    public class ArtistCoverter
    {
        public static ArtistViewModel Convert(Artist artist)
        {
            var artistViewModel = new ArtistViewModel()
            {
                ArtistId = artist.ArtistId,
                Name = artist.Name
            };
            return artistViewModel;
        }
        
        public static List<ArtistViewModel> ConvertList(List<Artist> artists)
        {
            List<ArtistViewModel> artistViewModels = new List<ArtistViewModel>();
            foreach(var a in artists)
            {
                var artistViewModel = new ArtistViewModel
                {
                    ArtistId = a.ArtistId,
                    Name = a.Name
                };
                artistViewModels.Add(artistViewModel);
            }

            return artistViewModels;
        }
    }
}