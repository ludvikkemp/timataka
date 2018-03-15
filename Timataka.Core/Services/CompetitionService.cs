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

        public async Task<Competition> Add(Competition c)
        {
            if(GetCompetitionByName(c.Name) != null)
            {
                throw new Exception("Competition already exists");
            }
            await _repo.InsertAsync(c);
            return c;
        }

        public async Task<Competition> GetCompetitionByName(string Name)
        {
            var c = await _repo.GetCompetitionByNameAsync(Name);
            return c;
        }

        public async Task<Competition> Edit(Competition c)
        {
            await _repo.EditAsync(c);
            return c;
        }

        public IEnumerable<Competition> GetAllCompetitions()
        {
            var Competitions = _repo.Get();
            return Competitions;
        }

        public async Task<Competition> GetCompetitionById(int CompetitionId)
        {
            var c = await _repo.GetByIdAsync(CompetitionId);
            return c;
        }

        public async Task<int> Remove(int CompetitionId)
        {
            var c = await GetCompetitionById(CompetitionId);
            await _repo.RemoveAsync(c);
            return CompetitionId;
        }

        public async Task<CompetitionInstance> AddInstance(CompetitionInstance c)
        {
            await _repo.InsertInstanceAsync(c);
            return c;
        }

        public async Task<CompetitionInstance> EditInstance(CompetitionInstance c)
        {
            await _repo.EditInstanceAsync(c);
            return c;
        }

        public async Task<int> RemoveInstance(int CompetitionInstanceId)
        {
            var c = await GetCompetitionInstanceById(CompetitionInstanceId);
            await _repo.RemoveInstanceAsync(c);
            return CompetitionInstanceId;
        }

        public IEnumerable<CompetitionInstance> GetAllCompetitionInstances()
        {
            var Instances = _repo.GetInstances();
            return Instances;
        }

        public IEnumerable<CompetitionInstance> GetAllInstancesOfCompetition(int Id)
        {
            var Instances = _repo.GetInstancesForCompetition(Id);
            return Instances;
        }

        public async Task<CompetitionInstance> GetCompetitionInstanceById(int Id)
        {
            return await _repo.GetInstanceByIdAsync(Id);
        }
    }
}
