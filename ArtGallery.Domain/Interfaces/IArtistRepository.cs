using BehindArt.Domain.Entitiyes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BehindArt.Domain.Interfaces
{
    public interface IArtistRepository
    {
        Task<Artist?> GetByIdAsync(int id);
        Task<IEnumerable<Artist>> GetAllAsync();
        Task AddAsync(Artist artist);
        void Update(Artist artist);
        void Delete(Artist artist);
        Task<bool> SaveChangesAsync();
    }
}
