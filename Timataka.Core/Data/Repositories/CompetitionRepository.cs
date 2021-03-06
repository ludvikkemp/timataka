﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Timataka.Core.Models.Dto.CompetitionInstanceDTO;
using Timataka.Core.Models.Entities;
using Timataka.Core.Models.ViewModels.CompetitionViewModels;
using Timataka.Core.Models.ViewModels.ContestantViewModels;
using Timataka.Core.Models.ViewModels.EventViewModels;
using Timataka.Core.Models.ViewModels.UserViewModels;

namespace Timataka.Core.Data.Repositories
{
    public class CompetitionRepository : ICompetitionRepository
    {
        private bool _disposed;
        private readonly ApplicationDbContext _context;

        public CompetitionRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        #region COMPETITION

        // *** INSERT *** //
        public void Insert(Competition c)
        {
            _context.Competitions.Add(c);
            _context.SaveChanges();
        }

        public async Task InsertAsync(Competition c)
        {
            await _context.Competitions.AddAsync(c);
            await _context.SaveChangesAsync();
        }

        // *** GET *** //
        public IEnumerable<Competition> Get()
        {
            return _context.Competitions.ToList();
        }

        public Competition GetById(int id)
        {
            return _context.Competitions.SingleOrDefault(x => x.Id == id);
        }

        public Task<Competition> GetByIdAsync(int id)
        {
            return _context.Competitions.SingleOrDefaultAsync(x => x.Id == id);
        }

        public Task<Competition> GetCompetitionByNameAsync(string cName)
        {
            return _context.Competitions.SingleOrDefaultAsync(x => x.Name == cName);
        }

        public CompetitionsViewModel GetCompetitionById(int id)
        {
            var model = (from c in _context.Competitions
                where c.Id == id
                select new CompetitionsViewModel
                {
                    Id = c.Id,
                    Name = c.Name,
                    Email = c.Email,
                    WebPage = c.WebPage,
                    Description = c.Description,
                    PhoneNumber = c.Phone,
                    Sponsor = c.Sponsor
                }).SingleOrDefault();
            return model;
        }

        // *** EDIT *** //
        public void Edit(Competition c)
        {
            _context.Competitions.Update(c);
            _context.SaveChanges();
        }

        public async Task EditAsync(Competition c)
        {
            _context.Competitions.Update(c);
            await _context.SaveChangesAsync();
        }

        // *** REMOVE *** //
        public void Remove(Competition c)
        {
            //TODO:Mark as removed, not delete compleatly
            c.Deleted = true;
            _context.Competitions.Update(c);
            _context.SaveChanges();
        }

        public async Task RemoveAsync(Competition c)
        {
            //TODO:Mark as removed, not delete compleatly
            c.Deleted = true;
            _context.Competitions.Update(c);
            await _context.SaveChangesAsync();
        }

        #endregion

        #region COMPETITION INSTANCE

        // *** INSERT *** //
        public void InsertInstance(CompetitionInstance c)
        {
            _context.CompetitionInstances.Add(c);
            _context.SaveChanges();
        }

        public async Task InsertInstanceAsync(CompetitionInstance c)
        {
            await _context.CompetitionInstances.AddAsync(c);
            await _context.SaveChangesAsync();
        }


        // *** EDIT *** //
        public void EditInstance(CompetitionInstance c)
        {
            _context.CompetitionInstances.Update(c);
            _context.SaveChanges();
        }

        public async Task EditInstanceAsync(CompetitionInstance c)
        {
            _context.CompetitionInstances.Update(c);
            await _context.SaveChangesAsync();
        }


        


        // *** REMOVE ***//
        public void RemoveInstance(CompetitionInstance c)
        {
            c.Deleted = true;
            _context.CompetitionInstances.Update(c);
            _context.SaveChanges();
        }

        public async Task RemoveInstanceAsync(CompetitionInstance c)
        {
            c.Deleted = true;
            _context.CompetitionInstances.Update(c);
            await _context.SaveChangesAsync();
        }


        // *** GET *** //
        public IEnumerable<CompetitionInstance> GetInstances()
        {
            return _context.CompetitionInstances.ToList();
        }

        public IEnumerable<CompetitionInstance> GetInstancesForCompetition(int id)
        {
            return _context.CompetitionInstances.Where(x => x.CompetitionId == id).ToList();
        }

        public CompetitionInstance GetInstanceById(int id)
        {
            return _context.CompetitionInstances.SingleOrDefault(x => x.Id == id);
        }

        public Task<CompetitionInstance> GetInstanceByIdAsync(int id)
        {
            return _context.CompetitionInstances.SingleOrDefaultAsync(x => x.Id == id);
        }

        public CompetitionsInstanceViewModel GetCompetitionInstanceById(int id)
        {
            var model = (from ci in _context.CompetitionInstances
                where ci.Id == id
                select new CompetitionsInstanceViewModel
                {
                    Id = ci.Id,
                    Name = ci.Name,
                    CompetitionId = ci.CompetitionId,
                    DateFrom = ci.DateFrom,
                    DateTo = ci.DateTo,
                    Location = ci.Location,
                    Status = ci.Status,
                    CountryId = ci.CountryId,
                    CountryName = (from n in _context.Countries
                        where n.Id == ci.CountryId
                        select n.Name).FirstOrDefault()
                }).SingleOrDefault();

            return model;
        }

        public IEnumerable<ContestantsInCompetitionViewModel> GetAllContestantsInCompetitionInstance(int id)
        {
            var r = (from e in _context.Events
                     where e.CompetitionInstanceId == id
                     join h in _context.Heats on e.Id equals h.EventId
                     join c in _context.ContestantsInHeats on h.Id equals c.HeatId
                     join u in _context.Users on c.UserId equals u.Id
                     select new ContestantsInCompetitionViewModel
                     {
                         Id = u.Id,
                         Name = u.FirstName + " " + u.LastName,
                         Gender = u.Gender,
                         HasAllChips = false,
                         EventList = null
                     }).Distinct().ToList();
            foreach (var item in r)
            {
                item.HasAllChips = HasAllChips(item.Id, id);
                item.EventList = GetEventListForContestant(item.Id, id);
            }
            return r;
        }

        public bool HasAllChips(string userId, int competitionInstanceId)
        {
            var countChips = (from h in _context.Heats
                              join e in _context.Events on h.EventId equals e.Id
                              where e.CompetitionInstanceId == competitionInstanceId
                              join cih in _context.ChipsInHeats on h.Id equals cih.HeatId
                              where cih.UserId == userId && cih.Valid == true
                              select cih.HeatId).Distinct().Count();
            var countEvents = (from h in _context.Heats
                               join e in _context.Events on h.EventId equals e.Id
                               where e.CompetitionInstanceId == competitionInstanceId
                               join cih in _context.ContestantsInHeats on h.Id equals cih.HeatId
                               where cih.UserId == userId
                               select cih.HeatId).Distinct().Count();
            if (countChips == countEvents)
            {
                return true;
            }
            return false;
        }

        public bool HasChip(string userId, int eventId)
        {
            var countChips = (from h in _context.Heats
                              where h.EventId == eventId
                              join cih in _context.ChipsInHeats on h.Id equals cih.HeatId
                              where cih.UserId == userId && cih.Valid == true
                              select cih).Count();
            if (countChips > 0)
            {
                return true;
            }
            return false;
        }

        public List<AddContestantViewModel> GetAddContestantViewModelByCompetitionInstanceId(int competitionInstanceId, string userId)
        {
            var results = (from e in _context.Events
                           where e.CompetitionInstanceId == competitionInstanceId
                           select new AddContestantViewModel
                           {
                               EventId = e.Id,
                               EventName = e.Name,
                               Heats = (from h in _context.Heats
                                        where h.EventId == e.Id && h.Deleted == false
                                        select new SelectListItem { Value = h.Id.ToString(), Text = h.HeatNumber.ToString() }).ToList(),
                               UserId = userId
                           }).ToList();


            var finalResults = new List<AddContestantViewModel>();

            foreach (var item in results)
            {
                var heatsThatContestantIsIn = (from h in _context.Heats
                                     join c in _context.ContestantsInHeats on h.Id equals c.HeatId
                                     where h.EventId == item.EventId && c.UserId == userId
                                     select h).ToList();

                if(heatsThatContestantIsIn.Count == 0)
                {
                    finalResults.Add(item);
                }
                
            }

            return finalResults;
        }

        public IEnumerable<MyCompetitionsViewModel> GetAllCompetitionInstancesForUser(string userId)
        {
            var r = (from c in _context.ContestantsInHeats
                     join h in _context.Heats on c.HeatId equals h.Id
                     join e in _context.Events on h.EventId equals e.Id
                     join i in _context.CompetitionInstances on e.CompetitionInstanceId equals i.Id
                     where c.UserId == userId
                     select i).Distinct().ToList();

            var model = new List<MyCompetitionsViewModel>();

            foreach (var item in r)
            {
                var events = GetEventsForUserInCompetitionInstance(userId, item.Id);
                var m = new MyCompetitionsViewModel
                {
                    Events = events,
                    Instance = item
                };
                model.Add(m);
            }
            return model;
        }

        private IEnumerable<Event> GetEventsForUserInCompetitionInstance(string userId, int competitionInstanceId)
        {
            var r = (from e in _context.Events
                     where e.CompetitionInstanceId == competitionInstanceId
                     join h in _context.Heats on e.Id equals h.EventId
                     join c in _context.ContestantsInHeats on h.Id equals c.HeatId
                     join u in _context.Users on c.UserId equals u.Id
                     where u.Id == userId
                     select e).ToList();
            return r;
        }

        public IEnumerable<EventForContestantDropDownListViewModel> GetEventListForContestant(string userId, int competitionInstanceId)
        {
            var r = (from e in _context.Events
                     where e.CompetitionInstanceId == competitionInstanceId
                     join h in _context.Heats on e.Id equals h.EventId
                     join c in _context.ContestantsInHeats on h.Id equals c.HeatId
                     join u in _context.Users on c.UserId equals u.Id
                     where u.Id == userId
                     select new EventForContestantDropDownListViewModel
                     {
                         Id = e.Id,
                         Name = e.Name,
                         HasChip = false
                     }).ToList();

            foreach(var item in r)
            {
                item.HasChip = HasChip(userId, item.Id);
            }
            return r;
        }

        public IEnumerable<Heat> GetHeatsForContestantInCompetitioninstance(string userId, int competitionInstanceId)
        {
            var r = (from e in _context.Events
                     where e.CompetitionInstanceId == competitionInstanceId
                     join h in _context.Heats on e.Id equals h.EventId
                     join c in _context.ContestantsInHeats on h.Id equals c.HeatId
                     join u in _context.Users on c.UserId equals u.Id
                     where u.Id == userId
                     select h).ToList();
            return r;
        }

        public EditContestantChipHeatResultDto GetEditContestantChipHeatResultDtoFor(string userId, int eventId, int competitionInstanceId)
        {
            var chipCode = "";
            var chipNumber = 0;
            var chip = (from c in _context.Chips
                            join cih in _context.ChipsInHeats on c.Code equals cih.ChipCode
                            join h in _context.Heats on cih.HeatId equals h.Id
                            where h.EventId == eventId && cih.UserId == userId
                            select c).SingleOrDefault();


            if(chip != null)
            {
                chipCode = chip.Code;
                chipNumber = chip.Number;
            }

            var results = (from contInHeat in _context.ContestantsInHeats
                           join h in _context.Heats on contInHeat.HeatId equals h.Id
                           join e in _context.Events on h.EventId equals e.Id
                           join r in _context.Results on new { contInHeat.UserId, contInHeat.HeatId } equals new { r.UserId, r.HeatId }
                           where contInHeat.UserId == userId && e.Id == eventId

                           select new EditContestantChipHeatResultDto
                           {
                               HeatId = h.Id,
                               ChipCode = chipCode,
                               ChipNumber = chipNumber,
                               Bib = contInHeat.Bib,
                               HeatNumber = h.HeatNumber,
                               ResultModified = r.Modified,
                               ContestantInHeatModified = contInHeat.Modified,
                               Notes = r.Notes,
                               Status = r.Status,
                               Team = contInHeat.Team
                           }).SingleOrDefault();

            return results;
        }

        public IEnumerable<EventViewModel> GetEventsForInstance(int id)
        {
            var result = (from e in _context.Events
                          join d in _context.Disciplines on e.DisciplineId equals d.Id
                          join s in _context.Sports on d.SportId equals s.Id
                          where e.CompetitionInstanceId == id
                          select new EventViewModel
                          {
                              Name = e.Name,
                              DateFrom = e.DateFrom,
                              CompetitionInstanceId = e.CompetitionInstanceId,
                              SportId = s.Id
                          }).ToList();
            return result;
        }

        public IEnumerable<Heat> GetHeatsInCompetitionInstance(int id)
        {
            var result = (from i in _context.CompetitionInstances
                          where i.Id == id
                          join e in _context.Events on i.Id equals e.CompetitionInstanceId
                          join h in _context.Heats on e.Id equals h.EventId
                          select h).ToList();
            return result;
        }

        public int GetNumberOfContestantsInInstance(int id)
        {
            var r = (from e in _context.Events
                     where e.CompetitionInstanceId == id
                     join h in _context.Heats on e.Id equals h.EventId
                     join c in _context.ContestantsInHeats on h.Id equals c.HeatId
                     join u in _context.Users on c.UserId equals u.Id
                     select u).Count();
            return r;
        }

        #endregion

        #region MANAGES COMPETITION

        public void AddRole(ManagesCompetition m)
        {
            _context.ManagesCompetitions.Add(m);
            _context.SaveChanges();
        }

        public async Task AddRoleAsync(ManagesCompetition m)
        {
            await _context.ManagesCompetitions.AddAsync(m);
            await _context.SaveChangesAsync();
        }

        public void EditRole(ManagesCompetition m)
        {
            _context.ManagesCompetitions.Update(m);
            _context.SaveChanges();
        }

        public async Task EditRoleAsync(ManagesCompetition m)
        {
            _context.ManagesCompetitions.Update(m);
            await _context.SaveChangesAsync();
        }

        public void RemoveRole(ManagesCompetition m)
        {
            _context.ManagesCompetitions.Remove(m);
            _context.SaveChanges();
        }

        public async Task RemoveRoleAsync(ManagesCompetition m)
        {
            _context.ManagesCompetitions.Remove(m);
            await _context.SaveChangesAsync();
        }

        public IEnumerable<ManagesCompetition> GetAllRoles()
        {
            var m = _context.ManagesCompetitions.ToList();
            return m;
        }

        public IEnumerable<ManagesCompetitionViewModel> GetRolesForCompetition(int id)
        {
            var results = from m in _context.ManagesCompetitions
                    join c in _context.Competitions on m.CompetitionId equals c.Id
                    join u in _context.Users on m.UserId equals u.Id
                    where m.CompetitionId.Equals(id)
                    select new ManagesCompetitionViewModel
                    {
                        UserId = u.Id,
                        CompetitionId = c.Id,
                        DateOfBirth = u.DateOfBirth,
                        FirstName = u.FirstName,
                        LastName = u.LastName,
                        MiddleName = u.MiddleName,
                        Role = m.Role,
                        Ssn = u.Ssn,
                        UserDeleted = u.Deleted
                    };
            return results;
        }

        public IEnumerable<ManagesCompetition> GetRolesForUser(string id)
        {
            var m = from x in _context.ManagesCompetitions
                    where x.UserId.Equals(id)
                    select x;
            return m;
        }

        
        protected virtual void Dispose(bool disposing)
        {
            if (!this._disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }

            this._disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion

    }
}

