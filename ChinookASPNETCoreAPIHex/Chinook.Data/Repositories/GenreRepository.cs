using System.Linq;
using System.Threading.Tasks;
using System.Threading;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Chinook.Domain.Repositories;
using Chinook.Domain.Entities;

namespace Chinook.Data.Repositories
{
    public class GenreRepository : IGenreRepository
    {
        private readonly ChinookContext _context;

        public GenreRepository(ChinookContext context)
        {
            _context = context;
        }

        private async Task<bool> GenreExists(int id, CancellationToken ct = default(CancellationToken))
        {
            return await GetByIdAsync(id, ct) != null;
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public async Task<List<Genre>> GetAllAsync(CancellationToken ct = default(CancellationToken))
        {
            IList<Genre> list = new List<Genre>();
            var genres = await _context.Genre.ToListAsync(cancellationToken: ct);
            foreach (var g in genres)
            {
                var genre = new Genre
                {
                    GenreId = g.GenreId,
                    Name = g.Name
                };
                list.Add(genre);
            }
            return list.ToList();
        }

        public async Task<Genre> GetByIdAsync(int id, CancellationToken ct = default(CancellationToken))
        {
            var old = await _context.Genre.FindAsync(id);
            var genre = new Genre
            {
                GenreId = old.GenreId,
                Name = old.Name
            };
            return genre;
        }

        public async Task<Genre> AddAsync(Genre newGenre, CancellationToken ct = default(CancellationToken))
        {
            var genre = new DataModels.Genre {Name = newGenre.Name};

            _context.Genre.Add(genre);
            await _context.SaveChangesAsync(ct);
            newGenre.GenreId = genre.GenreId;
            return newGenre;
        }

        public async Task<bool> UpdateAsync(Genre genre, CancellationToken ct = default(CancellationToken))
        {
            if (!await GenreExists(genre.GenreId, ct))
                return false;
            var changing = await _context.Genre.FindAsync(genre.GenreId);
            _context.Genre.Update(changing);
            changing.GenreId = genre.GenreId;
            changing.Name = genre.Name;

            await _context.SaveChangesAsync(ct);
            return true;
        }

        public async Task<bool> DeleteAsync(int id, CancellationToken ct = default(CancellationToken))
        {
            if (!await GenreExists(id, ct))
                return false;
            var toRemove = _context.Genre.Find(id);
            _context.Genre.Remove(toRemove);
            await _context.SaveChangesAsync(ct);
            return true;
        }
    }
}
