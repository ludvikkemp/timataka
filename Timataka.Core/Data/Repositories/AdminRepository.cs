using System;
using System.Collections.Generic;
using System.Text;

namespace Timataka.Core.Data.Repositories
{
    public class AdminRepository : IAdminRepository
    {
        private readonly ApplicationDbContext _db;

        public AdminRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        
    }
}
