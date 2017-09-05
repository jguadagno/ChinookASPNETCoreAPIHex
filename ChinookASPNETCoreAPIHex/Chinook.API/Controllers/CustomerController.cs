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
    public class CustomerController : Controller
    {
        public readonly ICustomerRepository CustomerRepository;
        public readonly IEmployeeRepository EmployeeRepository;
        public IMapper Mapper1 { get; }

        public CustomerController(ICustomerRepository customerRepository, IMapper mapper, IEmployeeRepository employeeRepository)
        {
            CustomerRepository = customerRepository;
            EmployeeRepository = employeeRepository;
            Mapper1 = mapper;
        }

        [HttpGet]
        [Produces(typeof(List<CustomerViewModel>))]
        public async Task<IActionResult> Get(CancellationToken ct = default(CancellationToken))
        {
            try
            {
                return new ObjectResult(await CustomerRepository.GetAllAsync(ct));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        [HttpGet("{id}")]
        [Produces(typeof(CustomerViewModel))]
        public async Task<IActionResult> Get(int id, CancellationToken ct = default(CancellationToken))
        {
            try
            {
                if (await CustomerRepository.GetByIdAsync(id, ct) == null)
                {
                    return NotFound();
                }
                return Ok(await CustomerRepository.GetByIdAsync(id, ct));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        [HttpGet("supportrep/{id}")]
        [Produces(typeof(List<CustomerViewModel>))]
        public async Task<IActionResult> GetBySupportRepId(int id, CancellationToken ct = default(CancellationToken))
        {
            try
            {
                if (await EmployeeRepository.GetByIdAsync(id, ct) == null)
                {
                    return NotFound();
                }
                return Ok(await CustomerRepository.GetBySupportRepIdAsync(id, ct));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        [HttpPost]
        [Produces(typeof(CustomerViewModel))]
        public async Task<IActionResult> Post([FromBody]CustomerViewModel input, CancellationToken ct = default(CancellationToken))
        {
            try
            {
                if (input == null)
                    return BadRequest();
                var customer = new Domain.Entities.Customer
                {
                    FirstName = input.FirstName,
                    LastName = input.LastName,
                    Company = input.Company,
                    Address = input.Address,
                    City = input.City,
                    State = input.State,
                    Country = input.Country,
                    PostalCode = input.PostalCode,
                    Phone = input.Phone,
                    Fax = input.Fax,
                    Email = input.Email,
                    SupportRepId = input.SupportRepId
                };

                return Ok(await CustomerRepository.AddAsync(customer, ct));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        [HttpPut("{id}")]
        [Produces(typeof(void))]
        public async Task<IActionResult> Put(int id, [FromBody]CustomerViewModel input, CancellationToken ct = default(CancellationToken))
        {
            try
            {
                if (input == null)
                    return BadRequest();
                if (await CustomerRepository.GetByIdAsync(id, ct) == null)
                {
                    return NotFound();
                }
                string errors = JsonConvert.SerializeObject(ModelState.Values
                .SelectMany(state => state.Errors)
                .Select(error => error.ErrorMessage));
                Debug.WriteLine(errors);

                var currentValues = await CustomerRepository.GetByIdAsync(id, ct);

                currentValues.CustomerId = input.CustomerId;
                currentValues.FirstName = input.FirstName;
                currentValues.LastName = input.LastName;
                currentValues.Company = input.Company;
                currentValues.Address = input.Address;
                currentValues.City = input.City;
                currentValues.State = input.State;
                currentValues.Country = input.Country;
                currentValues.PostalCode = input.PostalCode;
                currentValues.Phone = input.Phone;
                currentValues.Fax = input.Fax;
                currentValues.Email = input.Email;
                currentValues.SupportRepId = input.SupportRepId;

                return Ok(await CustomerRepository.UpdateAsync(currentValues, ct));
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
                if (await CustomerRepository.GetByIdAsync(id, ct) == null)
                {
                    return NotFound();
                }
                return Ok(await CustomerRepository.DeleteAsync(id, ct));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }
    }
}
