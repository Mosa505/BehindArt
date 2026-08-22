using BehindArt.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BehindArt.Application.Interfaces
{
    public interface IEraService
    {
        Task<EraDto?> GetByIdAsync(int id);
        Task<IEnumerable<EraDto>> GetAllAsync();

    }
}
