using System;
using System.Collections.Generic;
using System.Text;
using Timataka.Core.Data.Repositories;

namespace Timataka.Core.Services
{
    public class AdminService : IAdminService
    {
        private readonly IAdminRepository _repo;

        public AdminService(IAdminRepository repo)
        {
            _repo = repo;
        }


    }
}
