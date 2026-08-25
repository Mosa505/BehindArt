using BehindArt.Application.DTOs;
using BehindArt.Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BehindArt.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArtistsController : ControllerBase
    {
        private readonly IArtistService _artistService;

        public ArtistsController(IArtistService artistService)
        {
            _artistService = artistService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var artists = await _artistService.GetAllAsync();
            return Ok(artists);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            if (id == 0)
            {
                throw new Exception("Artist ID cannot be zero");
            }

            var artist = await _artistService.GetByIdAsync(id);
            
            return artist is null ? NotFound() : Ok(artist);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateArtistDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var created = await _artistService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById),new {id = created.Id },created);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateArtistDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var updated = await _artistService.UpdateAsync(id, dto);
            return updated ? NoContent() : NotFound();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _artistService.DeleteAsync(id);
            return deleted ? NoContent() : NotFound();
        }

    }
}
