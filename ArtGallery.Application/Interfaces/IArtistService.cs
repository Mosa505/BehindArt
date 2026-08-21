using BehindArt.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BehindArt.Application.Interfaces
{
    public interface IArtistService
    {
        Task<ArtistDto?> GetByIdAsync(int id);
        Task<IEnumerable<ArtistDto>> GetAllAsync();

    }
}
