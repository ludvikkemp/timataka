﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Timataka.Core.Models.Entities;
using Timataka.Core.Models.ViewModels.CompetitionViewModels;

namespace Timataka.Core.Data.Repositories
{
    public class CompetitionRepository : ICompetitionRepository
    {
        private bool _disposed = false;
        private readonly ApplicationDbContext _context;

        public CompetitionRepository(ApplicationDbContext context)
        {
            _context = context;
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

        //CompetitionInstance

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

        //ManagesCompetition

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

        public IEnumerable<ManagesCompetition> GetRolesForUser(string Id)
        {
            var m = from x in _context.ManagesCompetitions
                    where x.UserId.Equals(Id)
                    select x;
            return m;
        }
    }
}
