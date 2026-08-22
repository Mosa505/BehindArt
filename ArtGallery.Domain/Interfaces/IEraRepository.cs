using BehindArt.Domain.Entitiyes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BehindArt.Domain.Interfaces
{
    public interface IEraRepository
    {
        Task<Era?> GetByIdAsync(int id);
        Task<IEnumerable<Era>> GetAllAsync();
        Task AddAsync(Era era);
        void Update(Era era);
        void Delete(Era era);
        Task<bool> SaveChangesAsync();


    }
}
