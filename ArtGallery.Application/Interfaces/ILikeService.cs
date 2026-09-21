using BehindArt.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BehindArt.Application.Interfaces
{
    public interface ILikeService
    {
        Task<LikeStatusDto> LikePaintingAsync(int userId, int paintingId);
        Task<LikeStatusDto> UnlikePaintingAsync(int userId, int paintingId);
    }
}
