using BehindArt.Domain.Entitiyes;
using BehindArt.Domain.Interfaces;
using BehindArt.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BehindArt.Infrastructure.Repositories
{
    public class LikeRepository : ILikeRepository
    {
        private readonly AppDbContext _context;

        public LikeRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<bool> ExistsAsync(int userId, int paintingId) =>
            await _context.Likes.AnyAsync(l => l.UserId == userId && l.PaintingId == paintingId);

        public async Task AddAsync(Like like) =>
            await _context.Likes.AddAsync(like);

        public void Delete(Like like) =>
            _context.Likes.Remove(like);

        public async Task<bool> SaveChangesAsync() =>
            await _context.SaveChangesAsync() > 0;
    }
}
