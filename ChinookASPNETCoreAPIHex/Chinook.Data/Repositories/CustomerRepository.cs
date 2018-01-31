using System.Linq;
using System.Threading.Tasks;
using System.Threading;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Chinook.Domain.Repositories;
using Chinook.Domain.Entities;

namespace Chinook.Data.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly ChinookContext _context;

        public CustomerRepository(ChinookContext context)
        {
            _context = context;
        }

        private async Task<bool> CustomerExists(int id, CancellationToken ct = default(CancellationToken))
        {
            return await GetByIdAsync(id, ct) != null;
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public async Task<List<Customer>> GetAllAsync(CancellationToken ct = default(CancellationToken))
        {
            IList<Customer> list = new List<Customer>();
            var customers = await _context.Customer.ToListAsync(cancellationToken: ct);
            foreach (var i in customers)
            {
                var customer = new Customer
                {
                    CustomerId = i.CustomerId,
                    FirstName = i.FirstName,
                    LastName = i.LastName,
                    Company = i.Company,
                    Address = i.Address,
                    City = i.City,
                    State = i.State,
                    Country = i.Country,
                    PostalCode = i.PostalCode,
                    Phone = i.Phone,
                    Fax = i.Fax,
                    Email = i.Email,
                    SupportRepId = i.SupportRepId
                };
                list.Add(customer);
            }
            return list.ToList();
        }

        public async Task<Customer> GetByIdAsync(int id, CancellationToken ct = default(CancellationToken))
        {
            var old = await _context.Customer.FindAsync(id);
            var customer = new Customer
            {
                CustomerId = old.CustomerId,
                FirstName = old.FirstName,
                LastName = old.LastName,
                Company = old.Company,
                Address = old.Address,
                City = old.City,
                State = old.State,
                Country = old.Country,
                PostalCode = old.PostalCode,
                Phone = old.Phone,
                Fax = old.Fax,
                Email = old.Email,
                SupportRepId = old.SupportRepId
            };
            return customer;
        }

        public async Task<Customer> AddAsync(Customer newCustomer, CancellationToken ct = default(CancellationToken))
        {
            var customer = new DataModels.Customer
            {
                FirstName = newCustomer.FirstName,
                LastName = newCustomer.LastName,
                Company = newCustomer.Company,
                Address = newCustomer.Address,
                City = newCustomer.City,
                State = newCustomer.State,
                Country = newCustomer.Country,
                PostalCode = newCustomer.PostalCode,
                Phone = newCustomer.Phone,
                Fax = newCustomer.Fax,
                Email = newCustomer.Email,
                SupportRepId = newCustomer.SupportRepId
            };

            _context.Customer.Add(customer);
            await _context.SaveChangesAsync(ct);
            newCustomer.CustomerId = customer.CustomerId;
            return newCustomer;
        }

        public async Task<bool> UpdateAsync(Customer customer, CancellationToken ct = default(CancellationToken))
        {
            if (!await CustomerExists(customer.CustomerId, ct))
                return false;
            var changing = await _context.Customer.FindAsync(customer.CustomerId);
            _context.Customer.Update(changing);
            changing.CustomerId = customer.CustomerId;
            changing.FirstName = customer.FirstName;
            changing.LastName = customer.LastName;
            changing.Company = customer.Company;
            changing.Address = customer.Address;
            changing.City = customer.City;
            changing.State = customer.State;
            changing.Country = customer.Country;
            changing.PostalCode = customer.PostalCode;
            changing.Phone = customer.Phone;
            changing.Fax = customer.Fax;
            changing.Email = customer.Email;
            changing.SupportRepId = customer.SupportRepId;

            await _context.SaveChangesAsync(ct);
            return true;
        }

        public async Task<bool> DeleteAsync(int id, CancellationToken ct = default(CancellationToken))
        {
            if (!await CustomerExists(id, ct))
                return false;
            var toRemove = _context.Customer.Find(id);
            _context.Customer.Remove(toRemove);
            await _context.SaveChangesAsync(ct);
            return true;
        }

        public async Task<List<Customer>> GetBySupportRepIdAsync(int id, CancellationToken ct = default(CancellationToken))
        {
            IList<Customer> list = new List<Customer>();
            var current = await _context.Customer.Where(a => a.SupportRepId == id).ToListAsync(cancellationToken: ct);
            foreach (var i in current)
            {
                var customer = new Customer
                {
                    CustomerId = i.CustomerId,
                    FirstName = i.FirstName,
                    LastName = i.LastName,
                    Company = i.Company,
                    Address = i.Address,
                    City = i.City,
                    State = i.State,
                    Country = i.Country,
                    PostalCode = i.PostalCode,
                    Phone = i.Phone,
                    Fax = i.Fax,
                    Email = i.Email,
                    SupportRepId = i.SupportRepId
                };
                list.Add(customer);
            }
            return list.ToList();
        }
    }
}
