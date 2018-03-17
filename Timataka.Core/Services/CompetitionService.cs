using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Timataka.Core.Data.Repositories;
using Timataka.Core.Models.Entities;
using Timataka.Core.Models.ViewModels.CompetitionViewModels;

namespace Timataka.Core.Services
{
    public class CompetitionService : ICompetitionService
    {
        private readonly ICompetitionRepository _repo;

        public CompetitionService(ICompetitionRepository repo)
        {
            _repo = repo;
        }

        public CompetitionService()
        {
            //For unit tests
        }

        public async Task<Competition> Add(CompetitionsViewModel c)
        {
            var newComp = new Competition
            {
                Name = c.Name,
                WebPage = c.WebPage,
                Email = c.Email,
                Phone = c.PhoneNumber,
                Description = c.Description,
                Sponsor = c.Sponsor,
                Deleted = false
            };
            await _repo.InsertAsync(newComp);
            return newComp;
        }

        public async Task<Competition> Edit(Competition c, CompetitionsViewModel m)
        {
            c.Description = m.Description;
            c.Email = m.Description;
            c.Name = m.Name;
            c.Phone = m.PhoneNumber;
            c.Sponsor = m.Sponsor;
            c.WebPage = m.WebPage;
            await _repo.EditAsync(c);
            return c;
        }

        public IEnumerable<Competition> GetAllCompetitions()
        {
            var competitions = _repo.Get();
            return competitions;
        }

        public async Task<Competition> GetCompetitionById(int competitionId)
        {
            var c = await _repo.GetByIdAsync(competitionId);
            return c;
        }

        public CompetitionsViewModel GetCompetitionViewModelById(int id)
        {
            return _repo.GetCompetitionById(id);
        }

        public async Task<bool> CompetitionExists(string modelName)
        {
            var result = await _repo.GetCompetitionByNameAsync(modelName);
            if (result != null) return false;
            return true;
        }

        public async Task<int> Remove(int competitionId)
        {
            var c = await GetCompetitionById(competitionId);
            await _repo.RemoveAsync(c);
            return competitionId;
        }

        public async Task<CompetitionInstance> AddInstance(CompetitionsInstanceViewModel model)
        {
            // TODO: Villu checka, athuga hvort það sé til núþegar

            var newInstance = new CompetitionInstance
            {
                CompetitionId = model.CompetitionId,
                DateFrom = model.DateFrom,
                DateTo = model.DateTo,
                Location = model.Location,
                CountryId = model.CountryId,
                Name = model.Name,
                Status = model.Status,
                Deleted = false
            };

            await _repo.InsertInstanceAsync(newInstance);
            return newInstance;
        }

        public async Task<CompetitionInstance> EditInstance(CompetitionInstance compInstance, CompetitionsInstanceViewModel model)
        {
            compInstance.CountryId = model.CountryId;
            compInstance.DateFrom = model.DateFrom;
            compInstance.DateTo = model.DateTo;
            compInstance.Location = model.Location;
            compInstance.Name = model.Name;
            compInstance.Status = model.Status;
            await _repo.EditInstanceAsync(compInstance);
            return compInstance;
        }

        public async Task<int> RemoveInstance(int competitionInstanceId)
        {
            var c = await GetCompetitionInstanceById(competitionInstanceId);
            await _repo.RemoveInstanceAsync(c);
            return competitionInstanceId;
        }

        public IEnumerable<CompetitionInstance> GetAllCompetitionInstances()
        {
            var instances = _repo.GetInstances();
            return instances;
        }

        public IEnumerable<CompetitionInstance> GetAllInstancesOfCompetition(int id)
        {
            var instances = _repo.GetInstancesForCompetition(id);
            return instances;
        }

        public async Task<CompetitionInstance> GetCompetitionInstanceById(int id)
        {
            return await _repo.GetInstanceByIdAsync(id);
        }

        public CompetitionsInstanceViewModel GetCompetitionInstanceViewModelById(int id)
        {
            return _repo.GetCompetitionInstanceById(id);
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
            var m = _repo.GetAllRoles();
            return m;
        }

        public IEnumerable<ManagesCompetition> GetAllRolesForCompetition(int id)
        {
            var m = _repo.GetRolesForCompetition(id);
            return m;
        }

        public IEnumerable<ManagesCompetition> GetAllRolesForUser(string id)
        {
            var m = _repo.GetRolesForUser(id);
            return m;
        }

        public Role GetRole(string userId, int competitionId)
        {
            IEnumerable<ManagesCompetition> m = _repo.GetRolesForUser(userId);
            ManagesCompetition r = (from x in m
                                    where x.CompetitionId.Equals(competitionId)
                                    select x).SingleOrDefault();
            return r.Role;
        }
    }
}
