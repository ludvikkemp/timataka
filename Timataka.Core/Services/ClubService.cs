using System;
using System.Collections.Generic;
using System.Text;
using Timataka.Core.Data.Repositories;

namespace Timataka.Core.Services
{
    public class ClubService : IClubService
    {
        private readonly IClubRepository _repo;

        public ClubService(IClubRepository repo)
        {
            _repo = repo;
        }
    }
}
