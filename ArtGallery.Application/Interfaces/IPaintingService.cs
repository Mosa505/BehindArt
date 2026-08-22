using BehindArt.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BehindArt.Application.Interfaces
{
    public interface IPaintingService
    {
        Task<PaintingDto?> GetByIdAsync(int id);
        Task<IEnumerable<PaintingDto>> GetAllAsync();
        Task<IEnumerable<PaintingDto>> GetByEraAsync(int eraId);
        Task<PaintingDto> CreateAsync(CreatePaintingDto dto);
        Task<bool> UpdateAsync(int id, UpdatePaintingDto dto);
        Task<bool> DeleteAsync(int id);
    }
}
