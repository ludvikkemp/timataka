﻿using System;
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

        #region Competition
        /// <summary>
        /// Function to add new competition.
        /// </summary>
        /// <param name="c">CompetitionViewModel</param>
        /// <returns>Returns the newly created competition</returns>
        public async Task<Competition> Add(CompetitionsViewModel c)
        {
            //TODO: Check if competition exists
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

        /// <summary>
        /// Function to edit a competition
        /// </summary>
        /// <param name="c"></param>
        /// <param name="m"></param>
        /// <returns>Edited competition</returns>
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

        /// <summary>
        /// Function to get list of all compeitions
        /// </summary>
        /// <returns>List of all competitions</returns>
        public IEnumerable<Competition> GetAllCompetitions()
        {
            var competitions = _repo.Get();
            return competitions;
        }

        /// <summary>
        /// Function to get a competition by its ID
        /// </summary>
        /// <param name="competitionId"></param>
        /// <returns>Competition with the given ID if it exists</returns>
        public async Task<Competition> GetCompetitionById(int competitionId)
        {
            //TODO: Check if competition exists
            var c = await _repo.GetByIdAsync(competitionId);
            return c;
        }

        /// <summary>
        /// Function to get competition by ID.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>CompetitionViewModel for the given competition</returns>
        public CompetitionsViewModel GetCompetitionViewModelById(int id)
        {
            //TODO: Check if competition exists
            return _repo.GetCompetitionById(id);
        }

        /// <summary>
        /// Function to check if a given cmopetition exists.
        /// </summary>
        /// <param name="modelName"></param>
        /// <returns>True if the competition exists, false otherwise.</returns>
        public async Task<bool> CompetitionExists(string modelName)
        {
            var result = await _repo.GetCompetitionByNameAsync(modelName);
            if (result == null) return false;
            return true;
        }

        /// <summary>
        /// Function to remove a given competition.
        /// </summary>
        /// <param name="competitionId"></param>
        /// <returns>The ID of the competition removed.</returns>
        public async Task<int> Remove(int competitionId)
        {
            //TODO: return true/false?
            var c = await GetCompetitionById(competitionId);
            await _repo.RemoveAsync(c);
            return competitionId;
        }

#endregion

        #region Competition Instance
        /// <summary>
        /// Function to add an instance of a competition.
        /// </summary>
        /// <param name="model"></param>
        /// <returns>The created instance</returns>
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

        /// <summary>
        /// Function to edit an instance of a competition.
        /// </summary>
        /// <param name="compInstance"></param>
        /// <param name="model"></param>
        /// <returns>Edited instance</returns>
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

        /// <summary>
        /// Function to remove an instance of a competition.
        /// </summary>
        /// <param name="competitionInstanceId"></param>
        /// <returns>The ID of the instance removed.</returns>
        public async Task<int> RemoveInstance(int competitionInstanceId)
        {
            var c = await GetCompetitionInstanceById(competitionInstanceId);
            await _repo.RemoveInstanceAsync(c);
            return competitionInstanceId;
        }

        /// <summary>
        /// Function to get all competition instances.
        /// </summary>
        /// <returns>All instances.</returns>
        public IEnumerable<CompetitionInstance> GetAllCompetitionInstances()
        {
            var instances = _repo.GetInstances();
            return instances;
        }

        /// <summary>
        /// Function to get all instances of a given competition.
        /// </summary>
        /// <param name="id">ID of the competition</param>
        /// <returns>List of instances for a given competition.</returns>
        public IEnumerable<CompetitionInstance> GetAllInstancesOfCompetition(int id)
        {
            var instances = _repo.GetInstancesForCompetition(id);
            return instances;
        }

        /// <summary>
        /// Function to get an instance of a competition by ID.
        /// </summary>
        /// <param name="id">ID of the instance.</param>
        /// <returns>Instance of a competition.</returns>
        public async Task<CompetitionInstance> GetCompetitionInstanceById(int id)
        {
            return await _repo.GetInstanceByIdAsync(id);
        }

        /// <summary>
        /// Function to get an instance of a competition by ID.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>ViewModel for the competition instance.</returns>
        public CompetitionsInstanceViewModel GetCompetitionInstanceViewModelById(int id)
        {
            return _repo.GetCompetitionInstanceById(id);
        }

        #endregion

        #region User roles for competitions

        /// <summary>
        /// Function to add role to a user so the user can manage competition
        /// </summary>
        /// <param name="m"></param>
        /// <returns>Newly created role</returns>
        public async Task<ManagesCompetition> AddRole(ManagesCompetition m)
        {
            //TODO: Check if role exists
            await _repo.AddRoleAsync(m);
            return m;
        }

        /// <summary>
        /// Function to edit user role in competition
        /// </summary>
        /// <param name="m"></param>
        /// <returns>Edited role</returns>
        public async Task<ManagesCompetition> EditRole(ManagesCompetition m)
        {
            await _repo.EditRoleAsync(m);
            return m;
        }

        /// <summary>
        /// Function to remove user role in competition
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        public async Task RemoveRole(ManagesCompetition m)
        {
            //TODO: Return something to confirm
            await _repo.RemoveRoleAsync(m);
            return;
        }

        /// <summary>
        /// Function to get all roles for all competitions
        /// </summary>
        /// <returns>List of all roles</returns>
        public IEnumerable<ManagesCompetition> GetAllRoles()
        {
            var m = _repo.GetAllRoles();
            return m;
        }

        /// <summary>
        /// Function to get all roles for a given competiton
        /// </summary>
        /// <param name="id"></param>
        /// <returns>All roles for a given competitions</returns>
        public IEnumerable<ManagesCompetition> GetAllRolesForCompetition(int id)
        {
            var m = _repo.GetRolesForCompetition(id);
            return m;
        }

        /// <summary>
        /// Function to get all roles for a given user
        /// </summary>
        /// <param name="id"></param>
        /// <returns>List of roles in competitions for a given user</returns>
        public IEnumerable<ManagesCompetition> GetAllRolesForUser(string id)
        {
            var m = _repo.GetRolesForUser(id);
            return m;
        }

        /// <summary>
        /// Function to get the role af a user in competition
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="competitionId"></param>
        /// <returns>Role of the user in given competition</returns>
        public Role GetRole(string userId, int competitionId)
        {
            IEnumerable<ManagesCompetition> m = _repo.GetRolesForUser(userId);
            ManagesCompetition r = (from x in m
                                    where x.CompetitionId.Equals(competitionId)
                                    select x).SingleOrDefault();
            return r.Role;
        }

        #endregion

    }
}
