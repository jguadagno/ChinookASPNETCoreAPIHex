using System.Linq;
using System.Threading.Tasks;
using System.Threading;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Chinook.Domain.Repositories;
using Chinook.Domain.Entities;

namespace Chinook.Data.Repositories
{
    public class TrackRepository : ITrackRepository
    {
        private readonly ChinookContext _context;

        public TrackRepository(ChinookContext context)
        {
            _context = context;
        }

        private async Task<bool> TrackExists(int id, CancellationToken ct = default(CancellationToken))
        {
            return await GetByIdAsync(id, ct) != null;
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public async Task<List<Track>> GetAllAsync(CancellationToken ct = default(CancellationToken))
        {
            IList<Track> list = new List<Track>();
            var tracks = await _context.Track.ToListAsync(ct);
            foreach (var i in tracks)
            {
                var track = new Track
                {
                    TrackId = i.TrackId,
                    Name = i.Name,
                    AlbumId = i.AlbumId,
                    MediaTypeId = i.MediaTypeId,
                    GenreId = i.GenreId,
                    Composer = i.Composer,
                    Milliseconds = i.Milliseconds,
                    Bytes = i.Bytes,
                    UnitPrice = i.UnitPrice
                };
                list.Add(track);
            }
            return list.ToList();
        }

        public async Task<Track> GetByIdAsync(int id, CancellationToken ct = default(CancellationToken))
        {
            var old = await _context.Track.FindAsync(id);
            var track = new Track
            {
                TrackId = old.TrackId,
                Name = old.Name,
                AlbumId = old.AlbumId,
                MediaTypeId = old.MediaTypeId,
                GenreId = old.GenreId,
                Composer = old.Composer,
                Milliseconds = old.Milliseconds,
                Bytes = old.Bytes,
                UnitPrice = old.UnitPrice
            };
            return track;
        }

        public async Task<Track> AddAsync(Track newTrack, CancellationToken ct = default(CancellationToken))
        {
            var track = new DataModels.Track
            {
                Name = newTrack.Name,
                AlbumId = newTrack.AlbumId,
                MediaTypeId = newTrack.MediaTypeId,
                GenreId = newTrack.GenreId,
                Composer = newTrack.Composer,
                Milliseconds = newTrack.Milliseconds,
                Bytes = newTrack.Bytes,
                UnitPrice = newTrack.UnitPrice
            };

            _context.Track.Add(track);
            await _context.SaveChangesAsync(ct);
            newTrack.TrackId = track.TrackId;
            return newTrack;
        }

        public async Task<bool> UpdateAsync(Track track, CancellationToken ct = default(CancellationToken))
        {
            if (!await TrackExists(track.TrackId, ct))
                return false;
            var changing = await _context.Track.FindAsync(track.TrackId);
            _context.Track.Update(changing);

            changing.TrackId = track.TrackId;
            changing.Name = track.Name;
            changing.AlbumId = track.AlbumId;
            changing.MediaTypeId = track.MediaTypeId;
            changing.GenreId = track.GenreId;
            changing.Composer = track.Composer;
            changing.Milliseconds = track.Milliseconds;
            changing.Bytes = track.Bytes;
            changing.UnitPrice = track.UnitPrice;

            await _context.SaveChangesAsync(ct);
            return true;
        }

        public async Task<bool> DeleteAsync(int id, CancellationToken ct = default(CancellationToken))
        {
            if (!await TrackExists(id, ct))
                return false;
            var toRemove = _context.Track.Find(id);
            _context.Track.Remove(toRemove);
            await _context.SaveChangesAsync(ct);
            return true;
        }

        public async Task<List<Track>> GetByAlbumIdAsync(int id, CancellationToken ct = default(CancellationToken))
        {
            IList<Track> list = new List<Track>();
            var current = await _context.Track.Where(a => a.AlbumId == id).ToListAsync(ct);
            foreach (var i in current)
            {
                var track = new Track
                {
                    TrackId = i.TrackId,
                    Name = i.Name,
                    AlbumId = i.AlbumId,
                    MediaTypeId = i.MediaTypeId,
                    GenreId = i.GenreId,
                    Composer = i.Composer,
                    Milliseconds = i.Milliseconds,
                    Bytes = i.Bytes,
                    UnitPrice = i.UnitPrice
                };
                list.Add(track);
            }
            return list.ToList();
        }

        public async Task<List<Track>> GetByGenreIdAsync(int id, CancellationToken ct = default(CancellationToken))
        {
            IList<Track> list = new List<Track>();
            var current = await _context.Track.Where(a => a.GenreId == id).ToListAsync(ct);
            foreach (var i in current)
            {
                var track = new Track
                {
                    TrackId = i.TrackId,
                    Name = i.Name,
                    AlbumId = i.AlbumId,
                    MediaTypeId = i.MediaTypeId,
                    GenreId = i.GenreId,
                    Composer = i.Composer,
                    Milliseconds = i.Milliseconds,
                    Bytes = i.Bytes,
                    UnitPrice = i.UnitPrice
                };
                list.Add(track);
            }
            return list.ToList();
        }

        public async Task<List<Track>> GetByMediaTypeIdAsync(int id, CancellationToken ct = default(CancellationToken))
        {
            IList<Track> list = new List<Track>();
            var current = await _context.Track.Where(a => a.MediaTypeId == id).ToListAsync(ct);
            foreach (var i in current)
            {
                var track = new Track
                {
                    TrackId = i.TrackId,
                    Name = i.Name,
                    AlbumId = i.AlbumId,
                    MediaTypeId = i.MediaTypeId,
                    GenreId = i.GenreId,
                    Composer = i.Composer,
                    Milliseconds = i.Milliseconds,
                    Bytes = i.Bytes,
                    UnitPrice = i.UnitPrice
                };
                list.Add(track);
            }
            return list.ToList();
        }
    }
}
