using System.Linq;
using System.Threading.Tasks;
using System.Threading;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Chinook.Domain.Repositories;
using Chinook.Domain.Entities;

namespace Chinook.Data.Repositories
{
    public class MediaTypeRepository : IMediaTypeRepository
    {
        private readonly ChinookContext _context;
        private readonly ITrackRepository _trackRepo;

        public MediaTypeRepository(ChinookContext context, ITrackRepository trackRepo)
        {
            _context = context;
            _trackRepo = trackRepo;
        }

        private async Task<bool> MediaTypeExists(int id, CancellationToken ct = default(CancellationToken))
        {
            return await GetByIdAsync(id, ct) != null;
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public async Task<List<MediaType>> GetAllAsync(CancellationToken ct = default(CancellationToken))
        {
            IList<MediaType> list = new List<MediaType>();
            var mediaTypes = await _context.MediaType.ToListAsync(cancellationToken: ct);
            foreach (var i in mediaTypes)
            {
                var tracks = await _trackRepo.GetByMediaTypeIdAsync(i.MediaTypeId);
                var mediaType = new MediaType
                {
                    MediaTypeId = i.MediaTypeId,
                    Name = i.Name,
                    Tracks = tracks
                };
                list.Add(mediaType);
            }
            return list.ToList();
        }

        public async Task<MediaType> GetByIdAsync(int id, CancellationToken ct = default(CancellationToken))
        {
            var old = await _context.MediaType.FindAsync(id);
            var tracks = await _trackRepo.GetByMediaTypeIdAsync(old.MediaTypeId);
            var mediaType = new MediaType
            {
                MediaTypeId = old.MediaTypeId,
                Name = old.Name,
                Tracks = tracks
            };
            return mediaType;
        }

        public async Task<MediaType> AddAsync(MediaType newMediaType, CancellationToken ct = default(CancellationToken))
        {
            var mediaType = new DataModels.MediaType {Name = newMediaType.Name};

            _context.MediaType.Add(mediaType);
            await _context.SaveChangesAsync(ct);
            newMediaType.MediaTypeId = mediaType.MediaTypeId;
            return newMediaType;
        }

        public async Task<bool> UpdateAsync(MediaType mediaType, CancellationToken ct = default(CancellationToken))
        {
            if (!await MediaTypeExists(mediaType.MediaTypeId, ct))
                return false;
            var changing = await _context.MediaType.FindAsync(mediaType.MediaTypeId);
            _context.MediaType.Update(changing);
            changing.MediaTypeId = mediaType.MediaTypeId;
            changing.Name = mediaType.Name;

            await _context.SaveChangesAsync(ct);
            return true;
        }

        public async Task<bool> DeleteAsync(int id, CancellationToken ct = default(CancellationToken))
        {
            if (!await MediaTypeExists(id, ct))
                return false;
            var toRemove = _context.MediaType.Find(id);
            _context.MediaType.Remove(toRemove);
            await _context.SaveChangesAsync(ct);
            return true;
        }
    }
}
