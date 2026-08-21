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
    public class ArtistRepository : IArtistRepository
    {
        private readonly AppDbContext _context;

        public ArtistRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Artist?> GetByIdAsync(int id) =>
            await _context.Artists
                .Include(a => a.Paintings)
                .FirstOrDefaultAsync(a => a.Id == id);

        public async Task<IEnumerable<Artist>> GetAllAsync() =>
            await _context.Artists.ToListAsync();

        public async Task AddAsync(Artist artist) =>
            await _context.Artists.AddAsync(artist);

        public void Update(Artist artist) =>
            _context.Artists.Update(artist);

        public void Delete(Artist artist) =>
            _context.Artists.Remove(artist);

        public async Task<bool> SaveChangesAsync() =>
            await _context.SaveChangesAsync() > 0;
    }
}
