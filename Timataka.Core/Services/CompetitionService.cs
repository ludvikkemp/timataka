using System;
using System.Collections.Generic;
using System.Linq;
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

        public async Task<ManagesCompetition> AddRole(ManagesCompetition m)
        {
            await _repo.AddRoleAsync(m);
            return m;
        }

        public async Task<ManagesCompetition> EditRole(ManagesCompetition m)
        {
            await _repo.EditRoleAsync(m);
            return m;
        }

        public async Task RemoveRole(ManagesCompetition m)
        {
            await _repo.RemoveRoleAsync(m);
            return;
        }

        public IEnumerable<ManagesCompetition> GetAllRoles()
        {
            var m = GetAllRoles();
            return m;
        }

        public IEnumerable<ManagesCompetition> GetAllRolesForCompetition(int Id)
        {
            var m = GetAllRolesForCompetition(Id);
            return m;
        }

        public IEnumerable<ManagesCompetition> GetAllRolesForUser(int Id)
        {
            var m = GetAllRolesForUser(Id);
            return m;
        }

        public Role GetRole(int UserId, int CompetitionId)
        {
            IEnumerable<ManagesCompetition> m = GetAllRolesForUser(UserId);
            ManagesCompetition r = (from x in m
                                    where x.CompetitionId.Equals(CompetitionId)
                                    select x).SingleOrDefault();
            return r.Role;
        }
    }
}
