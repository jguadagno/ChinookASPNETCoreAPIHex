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
    public class TrackController : Controller
    {
        private readonly ITrackRepository _trackRepository;
        private readonly IAlbumRepository _albumRepository;
        private readonly IGenreRepository _genreRepository;
        private readonly IMediaTypeRepository _mediaTypeRepository;

        public TrackController(ITrackRepository trackRepository,
            IAlbumRepository albumRepository, IGenreRepository genreRepository,
            IMediaTypeRepository mediaTypeRepository)
        {
            _trackRepository = trackRepository;
            _albumRepository = albumRepository;
            _genreRepository = genreRepository;
            _mediaTypeRepository = mediaTypeRepository;
        }

        [HttpGet]
        [Produces(typeof(List<TrackViewModel>))]
        public async Task<IActionResult> Get(CancellationToken ct = default(CancellationToken))
        {
            try
            {
                return new ObjectResult(await _trackRepository.GetAllAsync(ct));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        [HttpGet("{id}")]
        [Produces(typeof(TrackViewModel))]
        public async Task<IActionResult> Get(int id, CancellationToken ct = default(CancellationToken))
        {
            try
            {
                if (await _trackRepository.GetByIdAsync(id, ct) == null)
                {
                    return NotFound();
                }
                return Ok(await _trackRepository.GetByIdAsync(id, ct));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        [HttpGet("album/{id}")]
        [Produces(typeof(List<TrackViewModel>))]
        public async Task<IActionResult> GetByAlbumId(int id, CancellationToken ct = default(CancellationToken))
        {
            try
            {
                if (await _albumRepository.GetByIdAsync(id, ct) == null)
                {
                    return NotFound();
                }
                return Ok(await _trackRepository.GetByAlbumIdAsync(id, ct));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        [HttpGet("mediatype/{id}")]
        [Produces(typeof(List<TrackViewModel>))]
        public async Task<IActionResult> GetByMediaTypeId(int id, CancellationToken ct = default(CancellationToken))
        {
            try
            {
                if (await _mediaTypeRepository.GetByIdAsync(id, ct) == null)
                {
                    return NotFound();
                }
                return Ok(await _trackRepository.GetByMediaTypeIdAsync(id, ct));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        [HttpGet("genre/{id}")]
        [Produces(typeof(List<TrackViewModel>))]
        public async Task<IActionResult> GetByGenreId(int id, CancellationToken ct = default(CancellationToken))
        {
            try
            {
                if (await _genreRepository.GetByIdAsync(id, ct) == null)
                {
                    return NotFound();
                }
                return Ok(await _trackRepository.GetByGenreIdAsync(id, ct));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        [HttpPost]
        [Produces(typeof(TrackViewModel))]
        public async Task<IActionResult> Post([FromBody]TrackViewModel input, CancellationToken ct = default(CancellationToken))
        {
            try
            {
                if (input == null)
                    return BadRequest();
                var track = new Domain.Entities.Track
                {
                    Name = input.Name,
                    AlbumId = input.AlbumId,
                    MediaTypeId = input.MediaTypeId,
                    GenreId = input.GenreId,
                    Composer = input.Composer,
                    Milliseconds = input.Milliseconds,
                    Bytes = input.Bytes,
                    UnitPrice = input.UnitPrice

                };

                return Ok(await _trackRepository.AddAsync(track, ct));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        [HttpPut("{id}")]
        [Produces(typeof(void))]
        public async Task<IActionResult> Put(int id, [FromBody]TrackViewModel input, CancellationToken ct = default(CancellationToken))
        {
            try
            {
                if (input == null)
                    return BadRequest();
                if (await _trackRepository.GetByIdAsync(id, ct) == null)
                {
                    return NotFound();
                }
                string errors = JsonConvert.SerializeObject(ModelState.Values
                .SelectMany(state => state.Errors)
                .Select(error => error.ErrorMessage));
                Debug.WriteLine(errors);

                var currentValues = await _trackRepository.GetByIdAsync(id, ct);

                currentValues.TrackId = input.TrackId;
                currentValues.Name = input.Name;
                currentValues.AlbumId = input.AlbumId;
                currentValues.MediaTypeId = input.MediaTypeId;
                currentValues.GenreId = input.GenreId;
                currentValues.Composer = input.Composer;
                currentValues.Milliseconds = input.Milliseconds;
                currentValues.Bytes = input.Bytes;
                currentValues.UnitPrice = input.UnitPrice;

                return Ok(await _trackRepository.UpdateAsync(currentValues, ct));
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
                if (await _trackRepository.GetByIdAsync(id, ct) == null)
                {
                    return NotFound();
                }
                return Ok(await _trackRepository.DeleteAsync(id, ct));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }
    }
}
