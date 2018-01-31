using System.Linq;
using System.Threading.Tasks;
using System.Threading;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Chinook.Domain.Repositories;
using Chinook.Domain.Entities;

namespace Chinook.Data.Repositories
{
    public class AlbumRepository : IAlbumRepository
    {
        private readonly ChinookContext _context;

        public AlbumRepository(ChinookContext context)
        {
            _context = context;
        }

        private async Task<bool> AlbumExists(int id, CancellationToken ct = default(CancellationToken))
        {
            return await GetByIdAsync(id, ct) != null;
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public async Task<List<Album>> GetAllAsync(CancellationToken ct = default(CancellationToken))
        {
            IList<Album> list = new List<Album>();
            var albums = await _context.Album.ToListAsync(ct);

            foreach (var i in albums)
            {
                var album = new Album
                {
                    AlbumId = i.AlbumId,
                    ArtistId = i.ArtistId,
                    Title = i.Title
                };
                list.Add(album);
            }
            return list.ToList();
        }

        public async Task<Album> GetByIdAsync(int id, CancellationToken ct = default(CancellationToken))
        {
            var albums = await _context.Album.FindAsync(id);
            var album = new Album
            {
                AlbumId = albums.AlbumId,
                ArtistId = albums.ArtistId,
                Title = albums.Title
            };
            return album;
        }

        public async Task<Album> AddAsync(Album newAlbum, CancellationToken ct = default(CancellationToken))
        {
            var album = new DataModels.Album
            {
                Title = newAlbum.Title,
                ArtistId = newAlbum.ArtistId
            };

            _context.Album.Add(album);
            await _context.SaveChangesAsync(ct);
            newAlbum.AlbumId = album.AlbumId;
            return newAlbum;
        }

        public async Task<bool> UpdateAsync(Album album, CancellationToken ct = default(CancellationToken))
        {
            if (!await AlbumExists(album.AlbumId, ct))
                return false;
            var changing = await _context.Album.FindAsync(album.AlbumId);
            

            changing.AlbumId = album.AlbumId;
            changing.Title = album.Title;
            changing.ArtistId = album.ArtistId;
            _context.Album.Update(changing);

            _context.Update(changing);
            await _context.SaveChangesAsync(ct);
            return true;
        }

        public async Task<bool> DeleteAsync(int id, CancellationToken ct = default(CancellationToken))
        {
            if (!await AlbumExists(id, ct))
                return false;
            var toRemove = _context.Album.Find(id);
            _context.Album.Remove(toRemove);
            await _context.SaveChangesAsync(ct);
            return true;
        }

        public async Task<List<Album>> GetByArtistIdAsync(int id, CancellationToken ct = default(CancellationToken))
        {
            IList<Album> list = new List<Album>();
            var current = await _context.Album.Where(a => a.ArtistId == id).ToListAsync(ct);
            foreach (var i in current)
            {
                var newisd = new Album
                {
                    Title = i.Title,
                    ArtistId = i.ArtistId,
                    AlbumId = i.AlbumId
                };
                list.Add(newisd);
            }
            return list.ToList();
        }
    }
}
