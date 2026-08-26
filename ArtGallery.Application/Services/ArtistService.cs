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
    public class ArtistService : IArtistService
    {
        private readonly IArtistRepository _artistRepository;

        public ArtistService(IArtistRepository artistRepository)
        {
            _artistRepository = artistRepository;
        }

        public async Task<ArtistDto?> GetByIdAsync(int id)
        {
            var artist = await _artistRepository.GetByIdAsync(id);
            return artist is null ? null : MapToDto(artist);
        }

        public async Task<IEnumerable<ArtistDto>> GetAllAsync()
        {
            var artists = await _artistRepository.GetAllAsync();
            return artists.Select(MapToDto);
        }
        public async Task<ArtistDto> CreateAsync(CreateArtistDto dto)
        {
            var artist = new Artist
            {
                Name = dto.Name,
                Biography = dto.Biography,
                BirthYear = dto.BirthYear,
                DeathYear = dto.DeathYear
            };

            await _artistRepository.AddAsync(artist);
            await _artistRepository.SaveChangesAsync();

            return MapToDto(artist);
        }
        public async Task<bool> UpdateAsync(int id, UpdateArtistDto dto)
        {
            var Artsist = await _artistRepository.GetByIdAsync(id);
            if (Artsist is null) return false;
             Artsist.Biography = dto.Biography;
             Artsist.BirthYear = dto.BirthYear;
             Artsist.DeathYear = dto.DeathYear;
             Artsist.Name = dto.Name;
            _artistRepository.Update(Artsist);
            return await _artistRepository.SaveChangesAsync();
           
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var artist = await _artistRepository.GetByIdAsync(id);
            if (artist is null) return false;
            if (artist.Paintings.Any()) 
                throw new InvalidOperationException("Cannot delete an artist with associated paintings.");

            _artistRepository.Delete(artist);
            return await _artistRepository.SaveChangesAsync();
        }

        private static ArtistDto MapToDto(Domain.Entitiyes.Artist a) => new()
        {
            Id = a.Id,
            Name = a.Name,
            Biography = a.Biography,
            BirthYear = a.BirthYear,
            DeathYear = a.DeathYear
        };

        
    }
}
