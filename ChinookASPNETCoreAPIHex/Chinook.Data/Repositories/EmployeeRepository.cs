using System.Linq;
using System.Threading.Tasks;
using System.Threading;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Chinook.Domain.Repositories;
using Chinook.Domain.Entities;

namespace Chinook.Data.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly ChinookContext _context;
        private readonly ICustomerRepository _customerRepo;

        public EmployeeRepository(ChinookContext context, ICustomerRepository customerRepo)
        {
            _context = context;
            _customerRepo = customerRepo;
        }

        private async Task<bool> EmployeeExists(int id, CancellationToken ct = default(CancellationToken))
        {
            return await GetByIdAsync(id, ct) != null;
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public async Task<List<Employee>> GetAllAsync(CancellationToken ct = default(CancellationToken))
        {
            IList<Employee> list = new List<Employee>();
            var employees = await _context.Employee.ToListAsync(cancellationToken: ct);
            foreach (var i in employees)
            {
                string reportsToName;
                var reportsTo = await this.GetReportsToAsync(i.EmployeeId, ct);
                if (reportsTo != null)
                {
                    reportsToName = reportsTo.LastName + ", " + reportsTo.FirstName;
                }
                else
                {
                    reportsToName = "";
                }
                var customers = await _customerRepo.GetBySupportRepIdAsync(i.EmployeeId, ct);
                var directReports = await this.GetDirectReportsAsync(i.EmployeeId, ct);
                var employee = new Employee
                {
                    EmployeeId = i.EmployeeId,
                    LastName = i.LastName,
                    FirstName = i.FirstName,
                    Title = i.Title,
                    ReportsTo = i.ReportsTo,
                    ReportsToName = reportsToName,
                    BirthDate = i.BirthDate,
                    HireDate = i.HireDate,
                    Address = i.Address,
                    City = i.City,
                    State = i.State,
                    Country = i.Country,
                    PostalCode = i.PostalCode,
                    Phone = i.Phone,
                    Fax = i.Fax,
                    Email = i.Email,
                    Manager = reportsTo,
                    Customers = customers,
                    DirectReports = directReports
                };
                list.Add(employee);
            }
            return list.ToList();
        }

        public async Task<Employee> GetByIdAsync(int id, CancellationToken ct = default(CancellationToken))
        {
            string reportsToName;
            var old = await _context.Employee.FindAsync(id);
            if (old.ReportsTo != null)
            {
                var reportsTo = await _context.Employee.FindAsync(old.ReportsTo);
                reportsToName = reportsTo.LastName + ", " + reportsTo.FirstName;
            }
            else
            {
                reportsToName = "";
            }
            var employee = new Employee
            {
                EmployeeId = old.EmployeeId,
                LastName = old.LastName,
                FirstName = old.FirstName,
                Title = old.Title,
                ReportsTo = old.ReportsTo,
                ReportsToName = reportsToName,
                BirthDate = old.BirthDate,
                HireDate = old.HireDate,
                Address = old.Address,
                City = old.City,
                State = old.State,
                Country = old.Country,
                PostalCode = old.PostalCode,
                Phone = old.Phone,
                Fax = old.Fax,
                Email = old.Email
            };
            return employee;
        }

        public async Task<Employee> AddAsync(Employee newEmployee, CancellationToken ct = default(CancellationToken))
        {
            var employee = new DataModels.Employee
            {
                Title = newEmployee.Title,
                LastName = newEmployee.LastName,
                FirstName = newEmployee.FirstName
            };

            employee.Title = newEmployee.Title;
            employee.ReportsTo = newEmployee.ReportsTo;
            employee.BirthDate = newEmployee.BirthDate;
            employee.HireDate = newEmployee.HireDate;
            employee.Address = newEmployee.Address;
            employee.City = newEmployee.City;
            employee.State = newEmployee.State;
            employee.Country = newEmployee.Country;
            employee.PostalCode = newEmployee.PostalCode;
            employee.Phone = newEmployee.Phone;
            employee.Fax = newEmployee.Fax;
            employee.Email = newEmployee.Email;

            _context.Employee.Add(employee);
            await _context.SaveChangesAsync(ct);
            newEmployee.EmployeeId = employee.EmployeeId;
            return newEmployee;
        }

        public async Task<bool> UpdateAsync(Employee employee, CancellationToken ct = default(CancellationToken))
        {
            if (!await EmployeeExists(employee.EmployeeId, ct))
                return false;
            var changing = await _context.Employee.FindAsync(employee.EmployeeId);
            _context.Employee.Update(changing);
            changing.EmployeeId = employee.EmployeeId;
            changing.LastName = employee.LastName;
            changing.FirstName = employee.FirstName;
            changing.Title = employee.Title;
            changing.ReportsTo = employee.ReportsTo;
            changing.BirthDate = employee.BirthDate;
            changing.HireDate = employee.HireDate;
            changing.Address = employee.Address;
            changing.City = employee.City;
            changing.State = employee.State;
            changing.Country = employee.Country;
            changing.PostalCode = employee.PostalCode;
            changing.Phone = employee.Phone;
            changing.Fax = employee.Fax;
            changing.Email = employee.Email;

            await _context.SaveChangesAsync(ct);
            return true;
        }

        public async Task<bool> DeleteAsync(int id, CancellationToken ct = default(CancellationToken))
        {
            if (!await EmployeeExists(id, ct))
                return false;
            var toRemove = _context.Employee.Find(id);
            _context.Employee.Remove(toRemove);
            await _context.SaveChangesAsync(ct);
            return true;
        }

        public async Task<Employee> GetReportsToAsync(int id, CancellationToken ct = default(CancellationToken))
        {
            var old = await _context.Employee.FindAsync(id);
            var reportsTo = await _context.Employee.FindAsync(old.ReportsTo);
            var employee = new Employee
            {
                EmployeeId = reportsTo.EmployeeId,
                LastName = reportsTo.LastName,
                FirstName = reportsTo.FirstName,
                Title = reportsTo.Title,
                BirthDate = reportsTo.BirthDate,
                HireDate = reportsTo.HireDate,
                Address = reportsTo.Address,
                City = reportsTo.City,
                State = reportsTo.State,
                Country = reportsTo.Country,
                PostalCode = reportsTo.PostalCode,
                Phone = reportsTo.Phone,
                Fax = reportsTo.Fax,
                Email = reportsTo.Email
            };
            return employee;
        }
        
        public async Task<List<Employee>> GetDirectReportsAsync(int id, CancellationToken ct = default(CancellationToken))
        {
            IList<Employee> list = new List<Employee>();
            var old = await _context.Employee.FindAsync(id);
            var directReports = _context.Employee.Where(e => e.ReportsTo == id);

            foreach (var e in directReports)
            {
                var employee = new Employee
                {
                    EmployeeId = e.EmployeeId,
                    LastName = e.LastName,
                    FirstName = e.FirstName,
                    Title = e.Title,
                    ReportsTo = old.EmployeeId,
                    ReportsToName = old.LastName + ", " + old.FirstName,
                    BirthDate = e.BirthDate,
                    HireDate = e.HireDate,
                    Address = e.Address,
                    City = e.City,
                    State = e.State,
                    Country = e.Country,
                    PostalCode = e.PostalCode,
                    Phone = e.Phone,
                    Fax = e.Fax,
                    Email = e.Email
                };
                list.Add(employee);
            }
            return list.ToList();
        }
    }
}
