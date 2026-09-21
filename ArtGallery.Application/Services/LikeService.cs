using BehindArt.Application.DTOs;
using BehindArt.Application.Interfaces;
using BehindArt.Domain.Entitiyes;
using BehindArt.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BehindArt.Application.Services
{
    public class LikeService : ILikeService
    {
        private readonly ILikeRepository _likeRepository;
        private readonly IPaintingRepository _paintingRepository;

        public LikeService(ILikeRepository likeRepository, IPaintingRepository paintingRepository)
        {
            _likeRepository = likeRepository;
            _paintingRepository = paintingRepository;
        }

        public async Task<LikeStatusDto> LikePaintingAsync(int userId, int paintingId)
        {
            var painting = await _paintingRepository.GetByIdAsync(paintingId);
            if (painting is null)
                throw new KeyNotFoundException($"Painting with id {paintingId} does not exist.");

            var alreadyLiked = await _likeRepository.ExistsAsync(userId, paintingId);
            if (alreadyLiked)
                throw new InvalidOperationException("You already liked this painting.");

            var like = new Like
            {
                UserId = userId,
                PaintingId = paintingId
            };

            await _likeRepository.AddAsync(like);
            await _likeRepository.SaveChangesAsync();

            var count = await _likeRepository.GetLikeCountAsync(paintingId);
            return new LikeStatusDto { IsLiked = true, LikeCount = count };
        }

        public async Task<LikeStatusDto> UnlikePaintingAsync(int userId, int paintingId)
        {
            var like = await _likeRepository.GetAsync(userId, paintingId);
            if (like is null)
                throw new KeyNotFoundException("You have not liked this painting.");

            _likeRepository.Delete(like);
            await _likeRepository.SaveChangesAsync();

            var count = await _likeRepository.GetLikeCountAsync(paintingId);
            return new LikeStatusDto { IsLiked = false, LikeCount = count };
        }
    }
}
