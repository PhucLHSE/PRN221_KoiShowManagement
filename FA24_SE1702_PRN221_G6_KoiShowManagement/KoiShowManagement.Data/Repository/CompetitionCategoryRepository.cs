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
    public class CompetitionCategoryRepository : GenericRepository<CompetitionCategory>
    {
        public CompetitionCategoryRepository() { }
        public CompetitionCategoryRepository(FA24_SE1702_PRN221_G6_KoiShowManagementContext context) => _context = context;

        public async Task<List<CompetitionCategory>> GetAllWithDetailsAsync()
        {
            return await _context.CompetitionCategories
                .Include(r => r.Competition)
                .Include(r => r.Variety)
                .ToListAsync();
        }

        public async Task<CompetitionCategory> GetByIdWithDetailsAsync(int id)
        {
            return await _context.CompetitionCategories
                .Include(r => r.Competition)
                .Include(r => r.Variety)
                .FirstOrDefaultAsync(r => r.CategoryId == id);
        }
    }
}
