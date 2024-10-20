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
    public class UsersRepository : GenericRepository<User>
    {
        public UsersRepository() { }
        public UsersRepository(FA24_SE1702_PRN221_G6_KoiShowManagementContext context) =>
            _context = context;
    }
}
