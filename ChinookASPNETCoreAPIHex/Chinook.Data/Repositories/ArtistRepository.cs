using System.Linq;
using System.Threading.Tasks;
using System.Threading;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Chinook.Domain.Repositories;
using Chinook.Domain.Entities;

namespace Chinook.Data.Repositories
{
    public class ArtistRepository : IArtistRepository
    {
        private readonly ChinookContext _context;

        public ArtistRepository(ChinookContext context)
        {
            _context = context;
        }

        private async Task<bool> ArtistExists(int id, CancellationToken ct = default(CancellationToken))
        {
            return await GetByIdAsync(id, ct) != null;
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public async Task<List<Artist>> GetAllAsync(CancellationToken ct = default(CancellationToken))
        {
            IList<Artist> list = new List<Artist>();
            var artists = await _context.Artist.ToListAsync(cancellationToken: ct);

            foreach (var i in artists)
            {
                var artist = new Artist
                {
                    ArtistId = i.ArtistId,
                    Name = i.Name
                };
                list.Add(artist);
            }
            return list.ToList();
        }

        public async Task<Artist> GetByIdAsync(int id, CancellationToken ct = default(CancellationToken))
        {
            var old = await _context.Artist.FindAsync(id);
            var artist = new Artist
            {
                ArtistId = old.ArtistId,
                Name = old.Name
            };
            return artist;
        }

        public async Task<Artist> AddAsync(Artist newArtist, CancellationToken ct = default(CancellationToken))
        {
            var artist = new DataModels.Artist {Name = newArtist.Name};

            _context.Artist.Add(artist);
            await _context.SaveChangesAsync(ct);
            newArtist.ArtistId = artist.ArtistId;
            return newArtist;
        }

        public async Task<bool> UpdateAsync(Artist artist, CancellationToken ct = default(CancellationToken))
        {
            if (!await ArtistExists(artist.ArtistId, ct))
                return false;
            var changing = await _context.Artist.FindAsync(artist.ArtistId);
            _context.Artist.Update(changing);
            changing.ArtistId = artist.ArtistId;
            changing.Name = artist.Name;

            await _context.SaveChangesAsync(ct);
            return true;
        }

        public async Task<bool> DeleteAsync(int id, CancellationToken ct = default(CancellationToken))
        {
            if (!await ArtistExists(id, ct))
                return false;
            var toRemove = _context.Artist.Find(id);
            _context.Artist.Remove(toRemove);
            await _context.SaveChangesAsync(ct);
            return true;
        }
    }
}
