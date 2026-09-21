using BehindArt.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BehindArt.Controllers
{
    [ApiController]
    [Authorize]
    public class SavesController : ControllerBase
    {
        private readonly ISaveService _saveService;

        public SavesController(ISaveService saveService)
        {
            _saveService = saveService;
        }

        [HttpPost("api/paintings/{paintingId:int}/save")]
        public async Task<IActionResult> Save(int paintingId)
        {
            var userId = GetCurrentUserId();
            await _saveService.SavePaintingAsync(userId, paintingId);
            return Ok(new { message = "Painting saved successfully." });
        }

        [HttpDelete("api/paintings/{paintingId:int}/save")]
        public async Task<IActionResult> Unsave(int paintingId)
        {
            var userId = GetCurrentUserId();
            await _saveService.UnsavePaintingAsync(userId, paintingId);
            return Ok(new { message = "Painting unsaved successfully." });
        }

        [HttpGet("api/users/me/saved")]
        public async Task<IActionResult> GetMySavedPaintings()
        {
            var userId = GetCurrentUserId();
            var saved = await _saveService.GetUserSavedPaintingsAsync(userId);
            return Ok(saved);
        }

        private int GetCurrentUserId()
        {
            var idClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
            return int.Parse(idClaim!);
        }
    }
}
