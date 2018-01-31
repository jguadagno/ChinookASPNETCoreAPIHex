using System.Linq;
using System.Threading.Tasks;
using System.Threading;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Chinook.Domain.Repositories;
using Chinook.Domain.Entities;
namespace Chinook.Data.Repositories
{
    public class PlaylistRepository : IPlaylistRepository
    {
        private readonly ChinookContext _context;

        public PlaylistRepository(ChinookContext context)
        {
            _context = context;
        }

        private async Task<bool> PlaylistExists(int id, CancellationToken ct = default(CancellationToken))
        {
            return await GetByIdAsync(id, ct) != null;
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public async Task<List<Playlist>> GetAllAsync(CancellationToken ct = default(CancellationToken))
        {
            IList<Playlist> list = new List<Playlist>();
            var playlists = await _context.Playlist.ToListAsync(ct);
           
            foreach (var i in playlists)
            {
                var playlist = new Playlist
                {
                    PlaylistId = i.PlaylistId,
                    Name = i.Name
                };
                list.Add(playlist);
            }
            return list.ToList();
        }

        public async Task<Playlist> GetByIdAsync(int id, CancellationToken ct = default(CancellationToken))
        {
            var old = await _context.Playlist.FindAsync(id);
            var playlist = new Playlist
            {
                PlaylistId = old.PlaylistId,
                Name = old.Name
            };
            return playlist;
        }

        public async Task<List<Track>> GetTrackByPlaylistIdAsync(int id, CancellationToken ct = default(CancellationToken))
        {
            IList<Track> list = new List<Track>();
            var playlistTracks = _context.PlaylistTrack.Where(p => p.PlaylistId == id);
            foreach (var playlistTrack in playlistTracks)
            {
                var track = await _context.Track.FindAsync(playlistTrack.TrackId);
                var trackEntity = new Track
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
                list.Add(trackEntity);
            }
            return list.ToList();
        }

        public async Task<Playlist> AddAsync(Playlist newPlaylist, CancellationToken ct = default(CancellationToken))
        {
            var playlist = new DataModels.Playlist {Name = newPlaylist.Name};

            _context.Playlist.Add(playlist);
            await _context.SaveChangesAsync(ct);
            newPlaylist.PlaylistId = playlist.PlaylistId;
            return newPlaylist;
        }

        public async Task<bool> UpdateAsync(Playlist playlist, CancellationToken ct = default(CancellationToken))
        {
            if (!await PlaylistExists(playlist.PlaylistId, ct))
                return false;
            var changing = await _context.Playlist.FindAsync(playlist.PlaylistId);
            _context.Playlist.Update(changing);
            changing.PlaylistId = playlist.PlaylistId;
            changing.Name = playlist.Name;

            await _context.SaveChangesAsync(ct);
            return true;
        }

        public async Task<bool> DeleteAsync(int id, CancellationToken ct = default(CancellationToken))
        {
            if (!await PlaylistExists(id, ct))
                return false;
            var toRemove = _context.Playlist.Find(id);
            _context.Playlist.Remove(toRemove);
            await _context.SaveChangesAsync(ct);
            return true;
        }
    }
}
