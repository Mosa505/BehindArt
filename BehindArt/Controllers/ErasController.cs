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




    }
}
