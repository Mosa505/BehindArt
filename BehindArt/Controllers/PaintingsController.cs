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
    }
}
