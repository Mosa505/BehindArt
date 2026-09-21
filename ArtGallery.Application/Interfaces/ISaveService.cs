using BehindArt.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BehindArt.Application.Interfaces
{
    public interface ISaveService
    {
        Task SavePaintingAsync(int userId, int paintingId);
        Task UnsavePaintingAsync(int userId, int paintingId);
        Task<IEnumerable<SavedPaintingDto>> GetUserSavedPaintingsAsync(int userId);
    }
}
