using BehindArt.Domain.Entitiyes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BehindArt.Domain.Interfaces
{
    public interface ILikeRepository
    {
        Task<bool> ExistsAsync(int userId, int paintingId);
        Task<Like?> GetAsync(int userId, int paintingId);
        Task AddAsync(Like like);
        void Delete(Like like);
        Task<int> GetLikeCountAsync(int paintingId);
        Task<bool> SaveChangesAsync();
    }
}
