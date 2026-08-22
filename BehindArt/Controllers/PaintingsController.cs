using BehindArt.Application.DTOs;
using BehindArt.Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BehindArt.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaintingsController : ControllerBase
    {
        private readonly IPaintingService _paintingService;

        public PaintingsController(IPaintingService paintingService)
        {
            _paintingService = paintingService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var paintings = await _paintingService.GetAllAsync();
            return Ok(paintings);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var painting = await _paintingService.GetByIdAsync(id);
            return painting is null ? NotFound() : Ok(painting);
        }

        [HttpGet("era/{eraId:int}")]
        public async Task<IActionResult> GetByEra(int eraId)
        {
            var paintings = await _paintingService.GetByEraAsync(eraId);
            return Ok(paintings);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreatePaintingDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            try
            {
                var created = await _paintingService.CreateAsync(dto);
                return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
            }
            catch (KeyNotFoundException ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdatePaintingDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            try
            {
                var updated = await _paintingService.UpdateAsync(id, dto);
                return updated ? NoContent() : NotFound();
            }
            catch (KeyNotFoundException ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _paintingService.DeleteAsync(id);
            return deleted ? NoContent() : NotFound();
        }
    }
}
