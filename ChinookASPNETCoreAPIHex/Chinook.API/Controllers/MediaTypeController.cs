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
    public class MediaTypeController : Controller
    {
        public readonly IMediaTypeRepository MediaTypeRepository;
        public IMapper Mapper1 { get; }

        public MediaTypeController(IMediaTypeRepository mediaTypeRepository, IMapper mapper)
        {
            MediaTypeRepository = mediaTypeRepository;
            Mapper1 = mapper;
        }

        [HttpGet]
        [Produces(typeof(List<MediaTypeViewModel>))]
        public async Task<IActionResult> Get(CancellationToken ct = default(CancellationToken))
        {
            try
            {
                return new ObjectResult(await MediaTypeRepository.GetAllAsync(ct));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        [HttpGet("{id}")]
        [Produces(typeof(MediaTypeViewModel))]
        public async Task<IActionResult> Get(int id, CancellationToken ct = default(CancellationToken))
        {
            try
            {
                if (await MediaTypeRepository.GetByIdAsync(id, ct) == null)
                {
                    return NotFound();
                }
                return Ok(await MediaTypeRepository.GetByIdAsync(id, ct));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        [HttpPost]
        [Produces(typeof(MediaTypeViewModel))]
        public async Task<IActionResult> Post([FromBody]MediaTypeViewModel input, CancellationToken ct = default(CancellationToken))
        {
            try
            {
                if (input == null)
                    return BadRequest();
                var mediaType = new Domain.Entities.MediaType
                {
                    Name = input.Name

                };

                return Ok(await MediaTypeRepository.AddAsync(mediaType, ct));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        [HttpPut("{id}")]
        [Produces(typeof(void))]
        public async Task<IActionResult> Put(int id, [FromBody]MediaTypeViewModel input, CancellationToken ct = default(CancellationToken))
        {
            try
            {
                if (input == null)
                    return BadRequest();
                if (await MediaTypeRepository.GetByIdAsync(id, ct) == null)
                {
                    return NotFound();
                }
                string errors = JsonConvert.SerializeObject(ModelState.Values
                .SelectMany(state => state.Errors)
                .Select(error => error.ErrorMessage));
                Debug.WriteLine(errors);

                var currentValues = await MediaTypeRepository.GetByIdAsync(id, ct);

                currentValues.MediaTypeId = input.MediaTypeId;
                currentValues.Name = input.Name;

                return Ok(await MediaTypeRepository.UpdateAsync(currentValues, ct));
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
                if (await MediaTypeRepository.GetByIdAsync(id, ct) == null)
                {
                    return NotFound();
                }
                return Ok(await MediaTypeRepository.DeleteAsync(id, ct));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }
    }
}
