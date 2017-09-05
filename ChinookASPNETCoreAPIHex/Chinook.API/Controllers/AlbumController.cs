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
    public class AlbumController : Controller
    {
        public readonly IAlbumRepository AlbumRepository;
        public readonly IArtistRepository ArtistRepository;
        public IMapper Mapper1 { get; }

        public AlbumController(IAlbumRepository albumRepository, IMapper mapper, IArtistRepository artistRepository)
        {
            AlbumRepository = albumRepository;
            ArtistRepository = artistRepository;
            Mapper1 = mapper;
        }

        [HttpGet]
        [Produces(typeof(List<AlbumViewModel>))]
        public async Task<IActionResult> Get(string sortOrder = "", string searchString = "", int page = 0, int pageSize = 0, CancellationToken ct = default(CancellationToken))
        {
            try
            {
                return new ObjectResult(await AlbumRepository.GetAllAsync(sortOrder, searchString, page, pageSize, ct));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        [HttpGet("{id}")]
        [Produces(typeof(AlbumViewModel))]
        public async Task<IActionResult> Get(int id, CancellationToken ct = default(CancellationToken))
        {
            try
            {
                if (await AlbumRepository.GetByIdAsync(id, ct) == null)
                {
                    return NotFound();
                }
                return Ok(await AlbumRepository.GetByIdAsync(id, ct));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        [HttpGet("artist/{id}")]
        [Produces(typeof(List<AlbumViewModel>))]
        public async Task<IActionResult> GetByArtistId(int id, CancellationToken ct = default(CancellationToken))
        {
            try
            {
                if (await ArtistRepository.GetByIdAsync(id, ct) == null)
                {
                    return NotFound();
                }
                return Ok(await AlbumRepository.GetByArtistIdAsync(id, ct));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        [HttpPost]
        [Produces(typeof(AlbumViewModel))]
        public async Task<IActionResult> Post([FromBody]AlbumViewModel input, CancellationToken ct = default(CancellationToken))
        {
            try
            {
                if (input == null)
                    return BadRequest();
                var album = new Domain.Entities.Album
                {
                    Title = input.Title,
                    ArtistId = input.ArtistId

                };

                return Ok(await AlbumRepository.AddAsync(album, ct));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        [HttpPut("{id}")]
        [Produces(typeof(void))]
        public async Task<IActionResult> Put(int id, [FromBody]AlbumViewModel input, CancellationToken ct = default(CancellationToken))
        {
            try
            {
                if (input == null)
                    return BadRequest();
                if (await AlbumRepository.GetByIdAsync(id, ct) == null)
                {
                    return NotFound();
                }
                string errors = JsonConvert.SerializeObject(ModelState.Values
                .SelectMany(state => state.Errors)
                .Select(error => error.ErrorMessage));
                Debug.WriteLine(errors);

                var currentValues = await AlbumRepository.GetByIdAsync(id, ct);

                currentValues.Title = input.Title;
                currentValues.ArtistId = input.ArtistId;

                return Ok(await AlbumRepository.UpdateAsync(currentValues, ct));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteAsync(int id, CancellationToken ct = default(CancellationToken))
        {
            try
            {
                if (await AlbumRepository.GetByIdAsync(id, ct) == null)
                {
                    return NotFound();
                }
                return Ok(await AlbumRepository.DeleteAsync(id, ct));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }
    }
}
