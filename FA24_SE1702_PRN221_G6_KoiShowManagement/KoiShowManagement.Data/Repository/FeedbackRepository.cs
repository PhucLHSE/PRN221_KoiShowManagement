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
    public class FeedbackRepository : GenericRepository<Feedback>
    {
        public FeedbackRepository() { }
        public FeedbackRepository(FA24_SE1702_PRN221_G6_KoiShowManagementContext context) =>
            _context = context;
        public async Task<List<Feedback>> GetAllWithDetailsAsync()
        {
            return await _context.Feedbacks
                .Include(r => r.Competition)
                .Include(r => r.User)
                .ToListAsync();
        }

        public async Task<Feedback> GetByIdWithDetailsAsync(int id)
        {
            return await _context.Feedbacks
                .Include(r => r.Competition)
                .Include(r => r.User)
                .FirstOrDefaultAsync(r => r.FeedbackId == id);
        }

    }
}
