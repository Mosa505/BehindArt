using BehindArt.Application.DTOs;
using BehindArt.Application.Interfaces;
using BehindArt.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BehindArt.Application.Services
{
    public class PaintingService : IPaintingService
    {
        private readonly IPaintingRepository _paintingRepository;

        public PaintingService(IPaintingRepository paintingRepository)
        {
            _paintingRepository = paintingRepository;
        }

        public async Task<PaintingDto?> GetByIdAsync(int id)
        {
            var painting = await _paintingRepository.GetByIdAsync(id);
            return painting is null ? null : MapToDto(painting);
        }

        public async Task<IEnumerable<PaintingDto>> GetAllAsync()
        {
            var paintings = await _paintingRepository.GetAllAsync();
            return paintings.Select(MapToDto);
        }

        public async Task<IEnumerable<PaintingDto>> GetByEraAsync(int eraId)
        {
            var paintings = await _paintingRepository.GetByEraAsync(eraId);
            return paintings.Select(MapToDto);
        }

        private static PaintingDto MapToDto(Domain.Entitiyes.Painting p) => new()
        {
            Id = p.Id,
            Title = p.Title,
            Description = p.Description,
            ImageUrl = p.ImageUrl,
            Year = p.Year,
            ArtistName = p.Artist.Name,
            EraName = p.Era.Name
        };
    }
}
