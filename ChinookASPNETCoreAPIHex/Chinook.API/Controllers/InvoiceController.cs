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
    public class InvoiceController : Controller
    {
        private readonly IInvoiceRepository _invoiceRepository;
        private readonly ICustomerRepository _customerRepository;

        public InvoiceController(IInvoiceRepository invoiceRepository, ICustomerRepository customerRepository)
        {
            _invoiceRepository = invoiceRepository;
            _customerRepository = customerRepository;
        }

        [HttpGet]
        [Produces(typeof(List<InvoiceViewModel>))]
        public async Task<IActionResult> Get(CancellationToken ct = default(CancellationToken))
        {
            try
            {
                return new ObjectResult(await _invoiceRepository.GetAllAsync(ct));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        [HttpGet("{id}")]
        [Produces(typeof(InvoiceViewModel))]
        public async Task<IActionResult> Get(int id, CancellationToken ct = default(CancellationToken))
        {
            try
            {
                if (await _invoiceRepository.GetByIdAsync(id, ct) == null)
                {
                    return NotFound();
                }
                return Ok(await _invoiceRepository.GetByIdAsync(id, ct));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        [HttpGet("customer/{id}")]
        [Produces(typeof(List<InvoiceViewModel>))]
        public async Task<IActionResult> GetByCustomerId(int id, CancellationToken ct = default(CancellationToken))
        {
            try
            {
                if (await _customerRepository.GetByIdAsync(id, ct) == null)
                {
                    return NotFound();
                }
                return Ok(await _invoiceRepository.GetByCustomerIdAsync(id, ct));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        [HttpPost]
        [Produces(typeof(InvoiceViewModel))]
        public async Task<IActionResult> Post([FromBody]InvoiceViewModel input, CancellationToken ct = default(CancellationToken))
        {
            try
            {
                if (input == null)
                    return BadRequest();
                var invoice = new Domain.Entities.Invoice
                {
                    CustomerId = input.CustomerId,
                    InvoiceDate = input.InvoiceDate,
                    BillingAddress = input.BillingAddress,
                    BillingCity = input.BillingCity,
                    BillingState = input.BillingState,
                    BillingCountry = input.BillingCountry,
                    BillingPostalCode = input.BillingPostalCode,
                    Total = input.Total

                };

                return Ok(await _invoiceRepository.AddAsync(invoice, ct));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        [HttpPut("{id}")]
        [Produces(typeof(void))]
        public async Task<IActionResult> Put(int id, [FromBody]InvoiceViewModel input, CancellationToken ct = default(CancellationToken))
        {
            try
            {
                if (input == null)
                    return BadRequest();
                if (await _invoiceRepository.GetByIdAsync(id, ct) == null)
                {
                    return NotFound();
                }
                var errors = JsonConvert.SerializeObject(ModelState.Values
                .SelectMany(state => state.Errors)
                .Select(error => error.ErrorMessage));
                Debug.WriteLine(errors);

                var currentValues = await _invoiceRepository.GetByIdAsync(id, ct);

                currentValues.InvoiceId = input.InvoiceId;
                currentValues.CustomerId = input.CustomerId;
                currentValues.InvoiceDate = input.InvoiceDate;
                currentValues.BillingAddress = input.BillingAddress;
                currentValues.BillingCity = input.BillingCity;
                currentValues.BillingState = input.BillingState;
                currentValues.BillingCountry = input.BillingCountry;
                currentValues.BillingPostalCode = input.BillingPostalCode;
                currentValues.Total = input.Total;

                return Ok(await _invoiceRepository.UpdateAsync(currentValues, ct));
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
                if (await _invoiceRepository.GetByIdAsync(id, ct) == null)
                {
                    return NotFound();
                }
                return Ok(await _invoiceRepository.DeleteAsync(id, ct));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }
    }
}
