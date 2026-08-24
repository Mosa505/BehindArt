using BehindArt.Application.DTOs;
using BehindArt.Application.Interfaces;
using BehindArt.Application.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BehindArt.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ErasController : ControllerBase
    {
        private readonly IEraService _eraService;
        public ErasController(IEraService eraService)
        {
            _eraService = eraService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var Eras = await _eraService.GetAllAsync();
            return Ok(Eras);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var era = await _eraService.GetByIdAsync(id);
            return era is null ? NotFound() : Ok(era);
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateEraDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var newera = await _eraService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = newera.Id }, newera);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, UpdateEraDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var updated = await _eraService.UpdateAsync(id, dto);
            return updated ? NoContent() : NotFound();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _eraService.DeleteAsync(id);
            return deleted ? NoContent() : NotFound();
        }


    }
}
