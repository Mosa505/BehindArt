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
    public class SaveService : ISaveService
    {
        private readonly ISaveRepository _saveRepository;
        private readonly IPaintingRepository _paintingRepository;

        public SaveService(ISaveRepository saveRepository, IPaintingRepository paintingRepository)
        {
            _saveRepository = saveRepository;
            _paintingRepository = paintingRepository;
        }

        public async Task SavePaintingAsync(int userId, int paintingId)
        {
            var painting = await _paintingRepository.GetByIdAsync(paintingId);
            if (painting is null)
                throw new KeyNotFoundException($"Painting with id {paintingId} does not exist.");

            var alreadySaved = await _saveRepository.ExistsAsync(userId, paintingId);
            if (alreadySaved)
                throw new InvalidOperationException("You already saved this painting.");

            var save = new Save
            {
                UserId= userId,
                PaintingId = paintingId
            };

            await _saveRepository.AddAsync(save);
            await _saveRepository.SaveChangesAsync();
        }

        public async Task UnsavePaintingAsync(int userId, int paintingId)
        {
            var save = await _saveRepository.GetAsync(userId, paintingId);
            if (save is null)
                throw new KeyNotFoundException("You have not saved this painting.");

            _saveRepository.Delete(save);
            await _saveRepository.SaveChangesAsync();
        }

        public async Task<IEnumerable<SavedPaintingDto>> GetUserSavedPaintingsAsync(int userId)
        {
            var saves = await _saveRepository.GetUserSavedPaintingsAsync(userId);

            return saves.Select(s => new SavedPaintingDto
            {
                PaintingId = s.PaintingId,
                Title = s.Painting.Title,
                ImageUrl = s.Painting.ImageUrl,
                ArtistName = s.Painting.Artist.Name,
                EraName = s.Painting.Era.Name,
                SavedAt = s.CreatedAt
            });
        }
    }
}
