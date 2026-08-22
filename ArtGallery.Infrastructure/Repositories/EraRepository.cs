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
    public class EraRepository : IEraRepository
    {
        private readonly AppDbContext _context;
        public EraRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task AddAsync(Era era)=>
            await _context.Eras.AddAsync(era);
        
        public void Delete(Era era)
        => _context.Eras.Remove(era);

        public async Task<IEnumerable<Era>> GetAllAsync()=>
            await _context.Eras.ToListAsync();

        public async Task<Era?> GetByIdAsync(int id)
        {
            return await _context.Eras
                .Include(p=> p.Paintings)
                .FirstOrDefaultAsync(e => e.Id == id);

        }

        public  async Task<bool> SaveChangesAsync()
        =>  await _context.SaveChangesAsync() > 0;

        public void Update(Era era)
        => _context.Eras.Update(era);
    }
}
