using System.Linq;
using System.Threading.Tasks;
using System.Threading;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Chinook.Domain.Repositories;
using Chinook.Domain.Entities;

namespace Chinook.Data.Repositories
{
    public class InvoiceLineRepository : IInvoiceLineRepository
    {
        private readonly ChinookContext _context;

        public InvoiceLineRepository(ChinookContext context)
        {
            _context = context;
        }

        private async Task<bool> InvoiceLineExists(int id, CancellationToken ct = default(CancellationToken))
        {
            return await GetByIdAsync(id, ct) != null;
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public async Task<List<InvoiceLine>> GetAllAsync(CancellationToken ct = default(CancellationToken))
        {
            IList<InvoiceLine> list = new List<InvoiceLine>();
            var invoiceLines = await _context.InvoiceLine.ToListAsync(cancellationToken: ct);
            foreach (var i in invoiceLines)
            {
                var invoiceLine = new InvoiceLine
                {
                    InvoiceLineId = i.InvoiceLineId,
                    InvoiceId = i.InvoiceId,
                    TrackId = i.TrackId,
                    UnitPrice = i.UnitPrice,
                    Quantity = i.Quantity
                };
                list.Add(invoiceLine);
            }
            return list.ToList();
        }

        public async Task<InvoiceLine> GetByIdAsync(int id, CancellationToken ct = default(CancellationToken))
        {
            var old = await _context.InvoiceLine.FindAsync(id);
            var invoiceLine = new InvoiceLine
            {
                InvoiceLineId = old.InvoiceLineId,
                InvoiceId = old.InvoiceId,
                TrackId = old.TrackId,
                UnitPrice = old.UnitPrice,
                Quantity = old.Quantity
            };
            return invoiceLine;
        }

        public async Task<InvoiceLine> AddAsync(InvoiceLine newInvoiceLine, CancellationToken ct = default(CancellationToken))
        {
            var invoiceLine = new DataModels.InvoiceLine
            {
                InvoiceId = newInvoiceLine.InvoiceId,
                TrackId = newInvoiceLine.TrackId,
                UnitPrice = newInvoiceLine.UnitPrice,
                Quantity = newInvoiceLine.Quantity
            };


            _context.InvoiceLine.Add(invoiceLine);
            await _context.SaveChangesAsync(ct);
            newInvoiceLine.InvoiceLineId = invoiceLine.InvoiceLineId;
            return newInvoiceLine;
        }

        public async Task<bool> UpdateAsync(InvoiceLine invoiceLine, CancellationToken ct = default(CancellationToken))
        {
            if (!await InvoiceLineExists(invoiceLine.InvoiceLineId, ct))
                return false;
            var changing = await _context.InvoiceLine.FindAsync(invoiceLine.InvoiceLineId);
            _context.InvoiceLine.Update(changing);

            changing.InvoiceLineId = invoiceLine.InvoiceLineId;
            changing.InvoiceId = invoiceLine.InvoiceId;
            changing.TrackId = invoiceLine.TrackId;
            changing.UnitPrice = invoiceLine.UnitPrice;
            changing.Quantity = invoiceLine.Quantity;

            await _context.SaveChangesAsync(ct);
            return true;
        }

        public async Task<bool> DeleteAsync(int id, CancellationToken ct = default(CancellationToken))
        {
            if (!await InvoiceLineExists(id, ct))
                return false;
            var toRemove = _context.InvoiceLine.Find(id);
            _context.InvoiceLine.Remove(toRemove);
            await _context.SaveChangesAsync(ct);
            return true;
        }

        public async Task<List<InvoiceLine>> GetByInvoiceIdAsync(int id, CancellationToken ct = default(CancellationToken))
        {
            IList<InvoiceLine> list = new List<InvoiceLine>();
            var current = await _context.InvoiceLine.Where(a => a.InvoiceId == id).ToListAsync(cancellationToken: ct);
            foreach (var i in current)
            {
                var newisd = new InvoiceLine
                {
                    InvoiceLineId = i.InvoiceLineId,
                    InvoiceId = i.InvoiceId,
                    TrackId = i.TrackId,
                    UnitPrice = i.UnitPrice,
                    Quantity = i.Quantity
                };
                list.Add(newisd);
            }
            return list.ToList();
        }

        public async Task<List<InvoiceLine>> GetByTrackIdAsync(int id, CancellationToken ct = default(CancellationToken))
        {
            IList<InvoiceLine> list = new List<InvoiceLine>();
            var current = await _context.InvoiceLine.Where(a => a.TrackId == id).ToListAsync(cancellationToken: ct);
            foreach (var i in current)
            {
                var newisd = new InvoiceLine
                {
                    InvoiceLineId = i.InvoiceLineId,
                    InvoiceId = i.InvoiceId,
                    TrackId = i.TrackId,
                    UnitPrice = i.UnitPrice,
                    Quantity = i.Quantity
                };
                list.Add(newisd);
            }
            return list.ToList();
        }
    }
}
