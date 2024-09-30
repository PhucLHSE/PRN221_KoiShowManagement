using KoiShowManagement.Data.Base;
using KoiShowManagement.Data.DBContext;
using KoiShowManagement.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoiShowManagement.Data.Repository
{
    public class AnimalRepository : GenericRepository<Animal>
    {
        public AnimalRepository() { }
        public AnimalRepository(FA24_SE1702_PRN221_G6_KoiShowManagementContext context) => _context = context;
        public async Task<List<Animal>> GetAlLAnimalsAsync()
        {
            /* return await _dbSet
                 .Include(u => u.Role.RoleName) // Assuming User entity has a navigation property to Role
                 .ToListAsync();*/
            return await _context.Animals.Include(p => p.Variety).ToListAsync();
        }
        public async Task<Animal> GetByIdAnimalAsync(int id)
        {
            return await _context.Animals.Include(p => p.Variety).FirstOrDefaultAsync(p => p.AnimalId == id);
        }
    }
}
