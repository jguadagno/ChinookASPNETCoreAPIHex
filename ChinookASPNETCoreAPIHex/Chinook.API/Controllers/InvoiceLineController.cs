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
    public class InvoiceLineController : Controller
    {
        public readonly IInvoiceLineRepository InvoiceLineRepository;
        public readonly IInvoiceRepository InvoiceRepository;
        public readonly ITrackRepository TrackRepository;
        public IMapper Mapper { get; }

        public InvoiceLineController(IInvoiceLineRepository invoiceLineRepository, IMapper mapper,
            IInvoiceRepository invoiceRepository, ITrackRepository trackRepository)
        {
            InvoiceLineRepository = invoiceLineRepository;
            InvoiceRepository = invoiceRepository;
            TrackRepository = trackRepository;
            Mapper = mapper;
        }

        [HttpGet]
        [Produces(typeof(List<InvoiceLineViewModel>))]
        public async Task<IActionResult> Get(CancellationToken ct = default(CancellationToken))
        {
            try
            {
                return new ObjectResult(await InvoiceLineRepository.GetAllAsync(ct));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        [HttpGet("{id}")]
        [Produces(typeof(InvoiceLineViewModel))]
        public async Task<IActionResult> Get(int id, CancellationToken ct = default(CancellationToken))
        {
            try
            {
                if (await InvoiceLineRepository.GetByIdAsync(id, ct) == null)
                {
                    return NotFound();
                }
                return Ok(await InvoiceLineRepository.GetByIdAsync(id, ct));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        [HttpGet("invoice/{id}")]
        [Produces(typeof(List<InvoiceLineViewModel>))]
        public async Task<IActionResult> GetByInvoiceId(int id, CancellationToken ct = default(CancellationToken))
        {
            try
            {
                if (await InvoiceRepository.GetByIdAsync(id, ct) == null)
                {
                    return NotFound();
                }
                return Ok(await InvoiceLineRepository.GetByInvoiceIdAsync(id, ct));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        [HttpGet("track/{id}")]
        [Produces(typeof(List<InvoiceLineViewModel>))]
        public async Task<IActionResult> GetByArtistId(int id, CancellationToken ct = default(CancellationToken))
        {
            try
            {
                if (await TrackRepository.GetByIdAsync(id, ct) == null)
                {
                    return NotFound();
                }
                return Ok(await InvoiceLineRepository.GetByTrackIdAsync(id, ct));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        [HttpPost]
        [Produces(typeof(InvoiceLineViewModel))]
        public async Task<IActionResult> Post([FromBody]InvoiceLineViewModel input, CancellationToken ct = default(CancellationToken))
        {
            try
            {
                if (input == null)
                    return BadRequest();
                var invoiceLine = new Domain.Entities.InvoiceLine
                {
                    InvoiceId = input.InvoiceId,
                    TrackId = input.TrackId,
                    UnitPrice = input.UnitPrice,
                    Quantity = input.Quantity
                };

                return Ok(await InvoiceLineRepository.AddAsync(invoiceLine, ct));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        [HttpPut("{id}")]
        [Produces(typeof(void))]
        public async Task<IActionResult> Put(int id, [FromBody]InvoiceLineViewModel input, CancellationToken ct = default(CancellationToken))
        {
            try
            {
                if (input == null)
                    return BadRequest();
                if (await InvoiceLineRepository.GetByIdAsync(id, ct) == null)
                {
                    return NotFound();
                }
                string errors = JsonConvert.SerializeObject(ModelState.Values
                .SelectMany(state => state.Errors)
                .Select(error => error.ErrorMessage));
                Debug.WriteLine(errors);

                var currentValues = await InvoiceLineRepository.GetByIdAsync(id, ct);

                currentValues.InvoiceLineId = input.InvoiceLineId;
                currentValues.InvoiceId = input.InvoiceId;
                currentValues.TrackId = input.TrackId;
                currentValues.UnitPrice = input.UnitPrice;
                currentValues.Quantity = input.Quantity;

                return Ok(await InvoiceLineRepository.UpdateAsync(currentValues, ct));
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
                if (await InvoiceLineRepository.GetByIdAsync(id, ct) == null)
                {
                    return NotFound();
                }
                return Ok(await InvoiceLineRepository.DeleteAsync(id, ct));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }
    }
}
