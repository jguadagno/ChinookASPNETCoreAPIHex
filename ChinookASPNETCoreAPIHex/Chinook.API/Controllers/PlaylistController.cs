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
    public class PlaylistController : Controller
    {
        public readonly IPlaylistRepository PlaylistRepository;
        public IMapper Mapper1 { get; }

        public PlaylistController(IPlaylistRepository playlistRepository, IMapper mapper)
        {
            PlaylistRepository = playlistRepository;
            Mapper1 = mapper;
        }

        [HttpGet]
        [Produces(typeof(List<PlaylistViewModel>))]
        public async Task<IActionResult> Get(CancellationToken ct = default(CancellationToken))
        {
            try
            {
                return new ObjectResult(await PlaylistRepository.GetAllAsync(ct));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        [HttpGet("{id}")]
        [Produces(typeof(PlaylistViewModel))]
        public async Task<IActionResult> Get(int id, CancellationToken ct = default(CancellationToken))
        {
            try
            {
                if (await PlaylistRepository.GetByIdAsync(id, ct) == null)
                {
                    return NotFound();
                }
                return Ok(await PlaylistRepository.GetByIdAsync(id, ct));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        [HttpPost]
        [Produces(typeof(PlaylistViewModel))]
        public async Task<IActionResult> Post([FromBody]PlaylistViewModel input, CancellationToken ct = default(CancellationToken))
        {
            try
            {
                if (input == null)
                    return BadRequest();
                var playlist = new Domain.Entities.Playlist
                {
                    Name = input.Name
                };

                return Ok(await PlaylistRepository.AddAsync(playlist, ct));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        [HttpPut("{id}")]
        [Produces(typeof(void))]
        public async Task<IActionResult> Put(int id, [FromBody]PlaylistViewModel input, CancellationToken ct = default(CancellationToken))
        {
            try
            {
                if (input == null)
                    return BadRequest();
                if (await PlaylistRepository.GetByIdAsync(id, ct) == null)
                {
                    return NotFound();
                }
                string errors = JsonConvert.SerializeObject(ModelState.Values
                .SelectMany(state => state.Errors)
                .Select(error => error.ErrorMessage));
                Debug.WriteLine(errors);

                var currentValues = await PlaylistRepository.GetByIdAsync(id, ct);

                currentValues.PlaylistId = input.PlaylistId;
                currentValues.Name = input.Name;

                return Ok(await PlaylistRepository.UpdateAsync(currentValues, ct));
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
                if (await PlaylistRepository.GetByIdAsync(id, ct) == null)
                {
                    return NotFound();
                }
                return Ok(await PlaylistRepository.DeleteAsync(id, ct));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }
    }
}
