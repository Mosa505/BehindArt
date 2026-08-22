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
    public class PaintingService : IPaintingService
    {
        private readonly IPaintingRepository _paintingRepository;
        private readonly IArtistRepository _artistRepository;
        private readonly IEraRepository _eraRepository;

        public PaintingService(IPaintingRepository paintingRepository, IArtistRepository artistRepository, IEraRepository eraRepository)
        {
            _paintingRepository = paintingRepository;
            _artistRepository = artistRepository;
            _eraRepository = eraRepository;
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
        public async Task<PaintingDto> CreateAsync(CreatePaintingDto dto)
        {
            await EnsureArtistAndEraExistAsync(dto.ArtistId, dto.EraId);
            var painting = new Painting
            {
                Title = dto.Title,
                Description = dto.Description,
                Story = dto.Story,
                Year = dto.Year,
                ImageUrl = dto.ImageUrl,
                ArtistId = dto.ArtistId,
                EraId = dto.EraId
            };
            await _paintingRepository.AddAsync(painting);
            await _paintingRepository.SaveChangesAsync();
            var created = await _paintingRepository.GetByIdAsync(painting.Id);
            return MapToDto(created!);

        }

        public async Task<bool> UpdateAsync(int id, UpdatePaintingDto dto)
        {
            var painting = await _paintingRepository.GetByIdAsync(id);
            if (painting is null) return false;

            await EnsureArtistAndEraExistAsync(dto.ArtistId, dto.EraId);

            painting.Title = dto.Title;
            painting.Description = dto.Description;
            painting.Story = dto.Story;
            painting.Year = dto.Year;
            painting.ImageUrl = dto.ImageUrl;
            painting.ArtistId = dto.ArtistId;
            painting.EraId = dto.EraId;

            _paintingRepository.Update(painting);
            return await _paintingRepository.SaveChangesAsync();
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var painting = await _paintingRepository.GetByIdAsync(id);
            if (painting is null) return false;

            _paintingRepository.Delete(painting);
            return await _paintingRepository.SaveChangesAsync();
        }

        private async Task EnsureArtistAndEraExistAsync(int artistId, int eraId)
        {
            var artist = await _artistRepository.GetByIdAsync(artistId);
            if (artist is null)
                throw new KeyNotFoundException($"Artist with id {artistId} does not exist.");

            var era = await _eraRepository.GetByIdAsync(eraId);
            if (era is null)
                throw new KeyNotFoundException($"Era with id {eraId} does not exist.");
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
