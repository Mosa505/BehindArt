using BehindArt.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BehindArt.Controllers
{
    [ApiController]
    [Route("api/paintings/{paintingId:int}/like")]
    [Authorize]
    public class LikesController : ControllerBase
    {
        private readonly ILikeService _likeService;

        public LikesController(ILikeService likeService)
        {
            _likeService = likeService;
        }

        [HttpPost]
        public async Task<IActionResult> Like(int paintingId)
        {
            var userId = GetCurrentUserId();
            var result = await _likeService.LikePaintingAsync(userId, paintingId);
            return Ok(result);
        }

        [HttpDelete]
        public async Task<IActionResult> Unlike(int paintingId)
        {
            var userId = GetCurrentUserId();
            var result = await _likeService.UnlikePaintingAsync(userId, paintingId);
            return Ok(result);
        }

        private int GetCurrentUserId()
        {
            var idClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
            return int.Parse(idClaim!);
        }
    }
}
