using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Timataka.Core.Data.Repositories;
using Timataka.Core.Models.Entities;

namespace Timataka.Core.Services
{
    public class CompetitionService : ICompetitionService
    {
        private readonly ICompetitionRepository _repo;

        public CompetitionService(ICompetitionRepository repo)
        {
            _repo = _repo;
        }

        public CompetitionService()
        {
            //For unit tests
        }

        public Task<Competition> Add(Competition c)
        {
            throw new NotImplementedException();
        }

        public Task<Competition> Edit(Competition c)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Competition> GetAllCompetitions()
        {
            throw new NotImplementedException();
        }

        public Task<Competition> GetCompetitionById(int CompetitionId)
        {
            throw new NotImplementedException();
        }

        public Task<int> Remove(int CompetitionId)
        {
            throw new NotImplementedException();
        }
    }
}
