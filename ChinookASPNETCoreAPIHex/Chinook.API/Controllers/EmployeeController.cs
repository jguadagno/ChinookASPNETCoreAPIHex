using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Chinook.API.ViewModels;
using Chinook.Domain.Repositories;
using AutoMapper;
using System.Threading;
using Newtonsoft.Json;
using System.Diagnostics;

namespace Chinook.API.Controllers
{
    [Route("api/[controller]")]
    public class EmployeeController : Controller
    {
        private readonly IEmployeeRepository _employeeRepository;

        public EmployeeController(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        [HttpGet]
        [Produces(typeof(List<EmployeeViewModel>))]
        public async Task<IActionResult> Get(CancellationToken ct = default(CancellationToken))
        {
            try
            {
                return new ObjectResult(await _employeeRepository.GetAllAsync(ct));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        [HttpGet("{id}")]
        [Produces(typeof(EmployeeViewModel))]
        public async Task<IActionResult> Get(int id, CancellationToken ct = default(CancellationToken))
        {
            try
            {
                if (await _employeeRepository.GetByIdAsync(id, ct) == null)
                {
                    return NotFound();
                }
                return Ok(await _employeeRepository.GetByIdAsync(id, ct));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        [HttpGet("reportsto/{id}")]
        [Produces(typeof(List<EmployeeViewModel>))]
        public async Task<IActionResult> GetReportsTo(int id, CancellationToken ct = default(CancellationToken))
        {
            try
            {
                if (await _employeeRepository.GetByIdAsync(id, ct) == null)
                {
                    return NotFound();
                }
                return Ok(await _employeeRepository.GetReportsToAsync(id, ct));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        [HttpPost]
        [Produces(typeof(EmployeeViewModel))]
        public async Task<IActionResult> Post([FromBody]EmployeeViewModel input, CancellationToken ct = default(CancellationToken))
        {
            try
            {
                if (input == null)
                    return BadRequest();
                var employee = new Domain.Entities.Employee
                {
                    LastName = input.LastName,
                    FirstName = input.FirstName,
                    Title = input.Title,
                    ReportsTo = input.ReportsTo,
                    BirthDate = input.BirthDate,
                    HireDate = input.HireDate,
                    Address = input.Address,
                    City = input.City,
                    State = input.State,
                    Country = input.Country,
                    PostalCode = input.PostalCode,
                    Phone = input.Phone,
                    Fax = input.Fax,
                    Email = input.Email
                };

                return Ok(await _employeeRepository.AddAsync(employee, ct));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        [HttpPut("{id}")]
        [Produces(typeof(void))]
        public async Task<IActionResult> Put(int id, [FromBody]EmployeeViewModel input, CancellationToken ct = default(CancellationToken))
        {
            try
            {
                if (input == null)
                    return BadRequest();
                if (await _employeeRepository.GetByIdAsync(id, ct) == null)
                {
                    return NotFound();
                }
                var errors = JsonConvert.SerializeObject(ModelState.Values
                .SelectMany(state => state.Errors)
                .Select(error => error.ErrorMessage));
                Debug.WriteLine(errors);

                var currentValues = await _employeeRepository.GetByIdAsync(id, ct);

                currentValues.EmployeeId = input.EmployeeId;
                currentValues.LastName = input.LastName;
                currentValues.FirstName = input.FirstName;
                currentValues.Title = input.Title;
                currentValues.ReportsTo = input.ReportsTo;
                currentValues.BirthDate = input.BirthDate;
                currentValues.HireDate = input.HireDate;
                currentValues.Address = input.Address;
                currentValues.City = input.City;
                currentValues.State = input.State;
                currentValues.Country = input.Country;
                currentValues.PostalCode = input.PostalCode;
                currentValues.Phone = input.Phone;
                currentValues.Fax = input.Fax;
                currentValues.Email = input.Email;

                return Ok(await _employeeRepository.UpdateAsync(currentValues, ct));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id, CancellationToken ct = default(CancellationToken))
        {
            try
            {
                if (await _employeeRepository.GetByIdAsync(id, ct) == null)
                {
                    return NotFound();
                }
                return Ok(await _employeeRepository.DeleteAsync(id, ct));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }
    }
}
