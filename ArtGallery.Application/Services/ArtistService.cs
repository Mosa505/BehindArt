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
