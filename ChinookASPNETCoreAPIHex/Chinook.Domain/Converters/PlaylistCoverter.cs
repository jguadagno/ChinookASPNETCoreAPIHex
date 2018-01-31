using System.Collections.Generic;
using Chinook.Domain.Entities;
using Chinook.Domain.ViewModels;

namespace Chinook.Domain.Converters
{
    public class PlaylistCoverter
    {
        public static PlaylistViewModel Convert(Playlist playlist)
        {
            var playlistViewModel = new PlaylistViewModel()
            {
                PlaylistId = playlist.PlaylistId,
                Name = playlist.Name
            };
            return playlistViewModel;
        }
        
        public static List<PlaylistViewModel> ConvertList(List<Playlist> playlists)
        {
            List<PlaylistViewModel> playlistViewModels = new List<PlaylistViewModel>();
            foreach(var p in playlists)
            {
                var playlistViewModel = new PlaylistViewModel
                {
                    PlaylistId = p.PlaylistId,
                    Name = p.Name
                };
                playlistViewModels.Add(playlistViewModel);
            }

            return playlistViewModels;
        }
    }
}