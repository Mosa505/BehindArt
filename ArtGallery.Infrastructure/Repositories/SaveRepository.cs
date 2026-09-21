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
    public class SaveRepository : ISaveRepository
    {
        private readonly AppDbContext _context;

        public SaveRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<bool> ExistsAsync(int userId, int paintingId) =>
            await _context.Saves.AnyAsync(s => s.UserId == userId && s.PaintingId == paintingId);

        public async Task<Save?> GetAsync(int userId, int paintingId) =>
            await _context.Saves.FirstOrDefaultAsync(s => s.UserId == userId && s.PaintingId == paintingId);

        public async Task AddAsync(Save save) =>
            await _context.Saves.AddAsync(save);

        public void Delete(Save save) =>
            _context.Saves.Remove(save);

        public async Task<IEnumerable<Save>> GetUserSavedPaintingsAsync(int userId) =>
            await _context.Saves
                .Where(s => s.UserId == userId)
                .Include(s => s.Painting)
                    .ThenInclude(p => p.Artist)
                .Include(s => s.Painting)
                    .ThenInclude(p => p.Era)
                .ToListAsync();

        public async Task<bool> SaveChangesAsync() =>
            await _context.SaveChangesAsync() > 0;



    }
}
