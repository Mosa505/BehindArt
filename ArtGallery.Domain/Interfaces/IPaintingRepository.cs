using BehindArt.Domain.Entitiyes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BehindArt.Domain.Interfaces
{
    public interface IPaintingRepository
    {
        Task<Painting?> GetByIdAsync(int id);
        Task<IEnumerable<Painting>> GetAllAsync();
        Task<IEnumerable<Painting>> GetByEraAsync(int eraId);
        Task AddAsync(Painting painting);
        void Update(Painting painting);
        void Delete(Painting painting);
        Task<bool> SaveChangesAsync();
    }
}
