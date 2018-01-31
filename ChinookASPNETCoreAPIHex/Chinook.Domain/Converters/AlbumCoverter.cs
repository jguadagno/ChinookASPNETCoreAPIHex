using System.Collections.Generic;
using Chinook.Domain.Entities;
using Chinook.Domain.ViewModels;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Chinook.Domain.Converters
{
    public class AlbumCoverter
    {
        public static AlbumViewModel Convert(Album album)
        {
            var albumViewModel = new AlbumViewModel
            {
                AlbumId = album.AlbumId,
                ArtistId = album.ArtistId,
                Title = album.Title
            };

            return albumViewModel;
        }
        
        public static List<AlbumViewModel> ConvertList(List<Album> albums)
        {
            List<AlbumViewModel> albumViewModels = new List<AlbumViewModel>();
            foreach(var a in albums)
            {
                var albumViewModel = new AlbumViewModel
                {
                    AlbumId = a.AlbumId,
                    ArtistId = a.ArtistId,
                    Title = a.Title
                };
                albumViewModels.Add(albumViewModel);
            }

            return albumViewModels;
        }
    }
}