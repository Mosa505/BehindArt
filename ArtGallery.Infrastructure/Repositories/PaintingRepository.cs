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
    public class PaintingRepository : IPaintingRepository
    {
        private readonly AppDbContext _context;

        public PaintingRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Painting?> GetByIdAsync(int id) =>
            await _context.Paintings
                .Include(p => p.Artist)
                .Include(p => p.Era)
                .FirstOrDefaultAsync(p => p.Id == id);

        public async Task<IEnumerable<Painting>> GetAllAsync() =>
            await _context.Paintings
                .Include(p => p.Artist)
                .Include(p => p.Era)
                .ToListAsync();

        public async Task<IEnumerable<Painting>> GetByEraAsync(int eraId) =>
            await _context.Paintings
                .Where(p => p.EraId == eraId)
                .Include(p => p.Artist)
                .ToListAsync();

        public async Task AddAsync(Painting painting) =>
            await _context.Paintings.AddAsync(painting);

        public void Update(Painting painting) =>
            _context.Paintings.Update(painting);

        public void Delete(Painting painting) =>
            _context.Paintings.Remove(painting);

        public async Task<bool> SaveChangesAsync() =>
            await _context.SaveChangesAsync() > 0;
    }
}
