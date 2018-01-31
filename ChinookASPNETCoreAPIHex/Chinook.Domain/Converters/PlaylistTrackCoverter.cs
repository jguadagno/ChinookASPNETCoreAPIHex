using System.Collections.Generic;
using Chinook.Domain.Entities;
using Chinook.Domain.ViewModels;

namespace Chinook.Domain.Converters
{
    public class PlaylistTrackCoverter
    {
        public static PlaylistTrackViewModel Convert(PlaylistTrack playlistTrack)
        {
            var playlistTrackViewModel = new PlaylistTrackViewModel
            {
                PlaylistId = playlistTrack.PlaylistId,
                TrackId = playlistTrack.TrackId
            };

            return playlistTrackViewModel;
        }
        
        public static List<PlaylistTrackViewModel> ConvertList(List<PlaylistTrack> playlistTracks)
        {
            List<PlaylistTrackViewModel> playlistTrackViewModels = new List<PlaylistTrackViewModel>();
            foreach(var p in playlistTracks)
            {
                var playlistTrackViewModel = new PlaylistTrackViewModel
                {
                    PlaylistId = p.PlaylistId,
                    TrackId = p.TrackId
                };
                playlistTrackViewModels.Add(playlistTrackViewModel);
            }

            return playlistTrackViewModels;
        }
    }
}