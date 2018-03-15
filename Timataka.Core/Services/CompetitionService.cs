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
            /*if (_repo.GetCompetitionByNameAsync(c.Name) != null)
            {
                throw new Exception("Competition already exists");
            }
            */
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

        public async Task<Competition> Edit(Competition c)
        {
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
                CountryId = model.Country,
                Name = model.Name,
                Status = model.Status,
                Deleted = false
            };

            await _repo.InsertInstanceAsync(newInstance);
            return newInstance;
        }

        public async Task<CompetitionInstance> EditInstance(CompetitionInstance c)
        {
            await _repo.EditInstanceAsync(c);
            return c;
        }

        public async Task<int> RemoveInstance(int competitionInstanceId)
        {
            var c = await GetCompetitionInstanceById(competitionInstanceId);
            await _repo.RemoveInstanceAsync(c);
            return competitionInstanceId;
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
