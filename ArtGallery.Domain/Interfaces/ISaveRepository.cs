using BehindArt.Domain.Entitiyes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BehindArt.Domain.Interfaces
{
    public interface ISaveRepository
    {
        Task<bool> ExistsAsync(int userId, int paintingId);
        Task<Save?> GetAsync(int userId, int paintingId);
        Task AddAsync(Save save);
        void Delete(Save save);
        Task<IEnumerable<Save>> GetUserSavedPaintingsAsync(int userId);
        Task<bool> SaveChangesAsync();
    }
}
