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
            var old = await _context.Track.ToListAsync(cancellationToken: ct);
            foreach (var i in old)
            {
                var album = await _context.Album.FindAsync(i.AlbumId);
                var mediaType = await _context.MediaType.FindAsync(i.MediaTypeId);
                string genreName;
                if (i.GenreId != null)
                {
                    var genre = await _context.Genre.FindAsync(i.GenreId);
                    genreName = genre.Name;
                }
                else
                {
                    genreName = "";
                }
                var track = new Track
                {
                    TrackId = i.TrackId,
                    Name = i.Name,
                    AlbumId = i.AlbumId,
                    AlbumName = album.Title,
                    MediaTypeId = i.MediaTypeId,
                    MediaTypeName = mediaType.Name,
                    GenreId = i.GenreId,
                    GenreName = genreName,
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
            string genreName;
            var old = await _context.Track.FindAsync(id);
            var album = await _context.Album.FindAsync(old.AlbumId);
            var mediaType = await _context.MediaType.FindAsync(old.MediaTypeId);
            if (old.GenreId != null)
            {
                var genre = await _context.Genre.FindAsync(old.GenreId);
                genreName = genre.Name;
            }
            else
            {
                genreName = "";
            }
            var track = new Track
            {
                TrackId = old.TrackId,
                Name = old.Name,
                AlbumId = old.AlbumId,
                AlbumName = album.Title,
                MediaTypeId = old.MediaTypeId,
                MediaTypeName = mediaType.Name,
                GenreId = old.GenreId,
                GenreName = genreName,
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
            var current = await _context.Track.Where(a => a.AlbumId == id).ToListAsync(cancellationToken: ct);
            foreach (DataModels.Track i in current)
            {
                var album = await _context.Album.FindAsync(i.AlbumId);
                var mediaType = await _context.MediaType.FindAsync(i.MediaTypeId);
                var genre = await _context.Genre.FindAsync(i.GenreId);
                Track newisd = new Track
                {
                    TrackId = i.TrackId,
                    Name = i.Name,
                    AlbumId = i.AlbumId,
                    AlbumName = album.Title,
                    MediaTypeId = i.MediaTypeId,
                    MediaTypeName = mediaType.Name,
                    GenreId = i.GenreId,
                    GenreName = genre.Name,
                    Composer = i.Composer,
                    Milliseconds = i.Milliseconds,
                    Bytes = i.Bytes,
                    UnitPrice = i.UnitPrice
                };
                list.Add(newisd);
            }
            return list.ToList();
        }

        public async Task<List<Track>> GetByGenreIdAsync(int id, CancellationToken ct = default(CancellationToken))
        {
            IList<Track> list = new List<Track>();
            var current = await _context.Track.Where(a => a.GenreId == id).ToListAsync(cancellationToken: ct);
            foreach (var i in current)
            {
                var album = await _context.Album.FindAsync(i.AlbumId);
                var mediaType = await _context.MediaType.FindAsync(i.MediaTypeId);
                var genre = await _context.Genre.FindAsync(i.GenreId);
                var newisd = new Track
                {
                    TrackId = i.TrackId,
                    Name = i.Name,
                    AlbumId = i.AlbumId,
                    AlbumName = album.Title,
                    MediaTypeId = i.MediaTypeId,
                    MediaTypeName = mediaType.Name,
                    GenreId = i.GenreId,
                    GenreName = genre.Name,
                    Composer = i.Composer,
                    Milliseconds = i.Milliseconds,
                    Bytes = i.Bytes,
                    UnitPrice = i.UnitPrice
                };
                list.Add(newisd);
            }
            return list.ToList();
        }

        public async Task<List<Track>> GetByMediaTypeIdAsync(int id, CancellationToken ct = default(CancellationToken))
        {
            IList<Track> list = new List<Track>();
            var current = await _context.Track.Where(a => a.MediaTypeId == id).ToListAsync(cancellationToken: ct);
            foreach (var i in current)
            {
                var album = await _context.Album.FindAsync(i.AlbumId);
                var mediaType = await _context.MediaType.FindAsync(i.MediaTypeId);
                var genre = await _context.Genre.FindAsync(i.GenreId);
                var newisd = new Track
                {
                    TrackId = i.TrackId,
                    Name = i.Name,
                    AlbumId = i.AlbumId,
                    AlbumName = album.Title,
                    MediaTypeId = i.MediaTypeId,
                    MediaTypeName = mediaType.Name,
                    GenreId = i.GenreId,
                    GenreName = genre.Name,
                    Composer = i.Composer,
                    Milliseconds = i.Milliseconds,
                    Bytes = i.Bytes,
                    UnitPrice = i.UnitPrice
                };
                list.Add(newisd);
            }
            return list.ToList();
        }
    }
}
