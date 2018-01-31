using System.Collections.Generic;
using Chinook.Domain.Entities;
using Chinook.Domain.ViewModels;

namespace Chinook.Domain.Converters
{
    public class EmployeeCoverter
    {
        public static EmployeeViewModel Convert(Employee employee)
        {
            var employeeViewModel = new EmployeeViewModel()
            {
                EmployeeId = employee.EmployeeId,
                LastName = employee.LastName,
                FirstName = employee.FirstName,
                Title = employee.Title,
                ReportsTo = employee.ReportsTo,
                BirthDate = employee.BirthDate,
                HireDate = employee.HireDate,
                Address = employee.Address,
                City = employee.City,
                State = employee.State,
                Country = employee.Country,
                PostalCode = employee.PostalCode,
                Phone = employee.Phone,
                Fax = employee.Fax,
                Email = employee.Email
            };

            return employeeViewModel;
        }
        
        public static List<EmployeeViewModel> ConvertList(List<Employee> employees)
        {
            List<EmployeeViewModel> employeeViewModels = new List<EmployeeViewModel>();
            foreach(var e in employees)
            {
                var employeeViewModel = new EmployeeViewModel
                {
                    EmployeeId = e.EmployeeId,
                    LastName = e.LastName,
                    FirstName = e.FirstName,
                    Title = e.Title,
                    ReportsTo = e.ReportsTo,
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
                employeeViewModels.Add(employeeViewModel);
            }

            return employeeViewModels;
        }
    }
}