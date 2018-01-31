using System.Collections.Generic;
using Chinook.Domain.Entities;
using Chinook.Domain.ViewModels;

namespace Chinook.Domain.Converters
{
    public class GenreCoverter
    {
        public static GenreViewModel Convert(Genre genre)
        {
            var genreViewModel = new GenreViewModel()
            {
                GenreId = genre.GenreId,
                Name = genre.Name
            };

            return genreViewModel;
        }
        
        public static List<GenreViewModel> ConvertList(List<Genre> genres)
        {
            List<GenreViewModel> genreViewModels = new List<GenreViewModel>();
            foreach(var g in genres)
            {
                var genreViewModel = new GenreViewModel
                {
                    GenreId = g.GenreId,
                    Name = g.Name
                };
                genreViewModels.Add(genreViewModel);
            }

            return genreViewModels;
        }
    }
}