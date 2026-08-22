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
    public class EraService : IEraService
    {
        private readonly IEraRepository _repository;
        public EraService(IEraRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<EraDto>> GetAllAsync()
        {
           var eras = await _repository.GetAllAsync();
            return eras.Select(MapToDto);
        }

        public async Task<EraDto?> GetByIdAsync(int id)
        {
           var era = await _repository.GetByIdAsync(id);
            return era is null ? null : MapToDto(era);
        }


        private static EraDto MapToDto(Era era) => new()
        {
            Id = era.Id,
            Name = era.Name,
            Description = era.Description,
            StartYear = era.StartYear,
            EndYear = era.EndYear
        };
    }
}
