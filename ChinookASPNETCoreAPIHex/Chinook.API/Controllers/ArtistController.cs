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
    public class ArtistController : Controller
    {
        public readonly IArtistRepository ArtistRepository;
        public IMapper Mapper1 { get; }

        public ArtistController(IArtistRepository artistRepository, IMapper mapper)
        {
            ArtistRepository = artistRepository;
            Mapper1 = mapper;
        }

        [HttpGet]
        [Produces(typeof(List<ArtistViewModel>))]
        public async Task<IActionResult> Get(CancellationToken ct = default(CancellationToken))
        {
            try
            {
                return new ObjectResult(await ArtistRepository.GetAllAsync(ct));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        [HttpGet("{id}")]
        [Produces(typeof(ArtistViewModel))]
        public async Task<IActionResult> Get(int id, CancellationToken ct = default(CancellationToken))
        {
            try
            {
                if (await ArtistRepository.GetByIdAsync(id, ct) == null)
                {
                    return NotFound();
                }
                return Ok(await ArtistRepository.GetByIdAsync(id, ct));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        [HttpPost]
        [Produces(typeof(ArtistViewModel))]
        public async Task<IActionResult> Post([FromBody]ArtistViewModel input, CancellationToken ct = default(CancellationToken))
        {
            try
            {
                if (input == null)
                    return BadRequest();
                var artist = new Domain.Entities.Artist
                {
                    Name = input.Name,
                    ArtistId = input.ArtistId

                };

                return Ok(await ArtistRepository.AddAsync(artist, ct));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        [HttpPut("{id}")]
        [Produces(typeof(void))]
        public async Task<IActionResult> Put(int id, [FromBody]ArtistViewModel input, CancellationToken ct = default(CancellationToken))
        {
            try
            {
                if (input == null)
                    return BadRequest();
                if (await ArtistRepository.GetByIdAsync(id, ct) == null)
                {
                    return NotFound();
                }
                string errors = JsonConvert.SerializeObject(ModelState.Values
                .SelectMany(state => state.Errors)
                .Select(error => error.ErrorMessage));
                Debug.WriteLine(errors);

                var currentValues = await ArtistRepository.GetByIdAsync(id, ct);

                currentValues.ArtistId = input.ArtistId;
                currentValues.Name = input.Name;

                return Ok(await ArtistRepository.UpdateAsync(currentValues, ct));
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
                if (await ArtistRepository.GetByIdAsync(id, ct) == null)
                {
                    return NotFound();
                }
                return Ok(await ArtistRepository.DeleteAsync(id, ct));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }
    }
}
