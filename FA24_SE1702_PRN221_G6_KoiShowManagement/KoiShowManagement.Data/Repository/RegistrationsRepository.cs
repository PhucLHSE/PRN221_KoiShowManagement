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
    public class RegistrationsRepository : GenericRepository<Registration>
    {
        public RegistrationsRepository() { }
        public RegistrationsRepository(FA24_SE1702_PRN221_G6_KoiShowManagementContext context) =>
            _context = context;
        //public async Task<Registration> GetByIdAsync(int code)
        //{
        //    return await _context.Registrations.Include(re => re.User).FirstOrDefaultAsync(re => re.UserId == code);

        //}

        public async Task<List<Registration>> GetAllWithDetailsAsync()
        {
            return await _context.Registrations
                .Include(r => r.Animal)
                .Include(r => r.Competition)
                .Include(r => r.User)
                .ToListAsync();
        }

        public async Task<Registration> GetByIdWithDetailsAsync(int id)
        {
            return await _context.Registrations
                .Include(r => r.Animal)
                .Include(r => r.Competition)
                .Include(r => r.User)
                .FirstOrDefaultAsync(r => r.RegistrationId == id);
        }



    }
}
