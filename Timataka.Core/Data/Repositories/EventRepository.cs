﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Timataka.Core.Models.Entities;
using Timataka.Core.Models.ViewModels.EventViewModels;

namespace Timataka.Core.Data.Repositories
{
    public class EventRepository : IEventRepository
    {
        private readonly ApplicationDbContext _context;
        private bool _disposed;

        public EventRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        
        public async Task<Event> InsertAsync(Event entity)
        {
            var results = await _context.Events.AddAsync(entity);
            await _context.SaveChangesAsync();
            return results.Entity;
        }

        public async Task<Event> GetEventByNameAsync(string eventName)
        {
            return await _context.Events.SingleOrDefaultAsync(x => x.Name == eventName);
        }

        public async Task<Event> GetByIdAsync(int id)
        {
            return await _context.Events.SingleOrDefaultAsync(x => x.Id == id);
        }

        public void Edit(Event entity)
        {
            _context.Events.Update(entity);
            _context.SaveChanges();
        }

        public async Task EditAsync(Event entity)
        {
            _context.Events.Update(entity);
            await _context.SaveChangesAsync();
        }

        public void Remove(Event entity)
        {
            entity.Deleted = true;
            _context.Events.Update(entity);
            _context.SaveChanges();
            
        }

        public async Task RemoveAsync(Event entity)
        {
            entity.Deleted = true;
            _context.Events.Update(entity);
            await _context.SaveChangesAsync();
        }
        public IEnumerable<Event> Get()
        {
            return _context.Events.ToList();
        }

        public IEnumerable<EventViewModel> GetEventsForInstance(int id)
        {
            var events = (from e in _context.Events
                          join d in _context.Disciplines on e.DisciplineId equals d.Id
                          where e.CompetitionInstanceId == id
                          select new EventViewModel
                          {
                              ActiveChip = e.ActiveChip,
                              CompetitionInstanceId = e.CompetitionInstanceId,
                              CourseId = e.CourseId,
                              DateFrom = e.DateFrom,
                              DateTo = e.DateTo,
                              DisciplineId = e.DisciplineId,
                              DisciplineName = d.Name,
                              DistanceOffset = e.DistanceOffset,
                              Gender = e.Gender,
                              Id = e.Id,
                              Laps = e.Laps,
                              Name = e.Name,
                              Splits = e.Splits,
                              StartInterval = e.StartInterval,
                              Deleted = e.Deleted,
                              Categories = (from c in _context.Categories
                                            where c.EventId == e.Id
                                            select c).ToList()
                          }).ToList();
            return events;
        }
        
        public Task<EventViewModel> GetEventByIdAsync(int id)
        {
            var model = (from e in _context.Events
                where e.Id == id
                select new EventViewModel
                {
                    Name = e.Name,
                    Id = e.Id,
                    DateFrom = e.DateFrom,
                    DateTo = e.DateTo,
                    CompetitionInstanceId = e.CompetitionInstanceId,
                    DisciplineId = e.DisciplineId,
                    DisciplineName = (from d in _context.Disciplines
                                      where d.Id == e.DisciplineId
                                      select d.Name).FirstOrDefault(),
                    ActiveChip = e.ActiveChip,
                    CourseId = e.CourseId,
                    CourseName = (from c in _context.Courses
                                  where c.Id == e.CourseId
                                  select c.Name).FirstOrDefault(),
                    DistanceOffset = e.DistanceOffset,
                    Gender = e.Gender,
                    Laps = e.Laps,
                    Splits = e.Splits,
                    StartInterval = e.StartInterval
                }).SingleOrDefaultAsync();
            return model;
        }

        public IEnumerable<EventDropDownListViewModel> GetDropDownList()
        {
            var model = (from e in _context.Events
                select new EventDropDownListViewModel
                {
                    Id = e.Id,
                    Name = e.Name
                }).ToList();
            return model;
        }

        public IEnumerable<Event> GetEventByInstanceAndContestantId(int competitionInstanceId, string userId)
        {
            var events = (from e in _context.Events
                            join h in _context.Heats on e.Id equals h.EventId
                            join c in _context.ContestantsInHeats on h.Id equals c.HeatId
                            join u in _context.Users on c.UserId equals u.Id
                            where e.CompetitionInstanceId == competitionInstanceId && u.Id == userId
                            select e).ToList();
            
            return events;
        }
        
        public Event GetById(int id)
        {
            return _context.Events.SingleOrDefault(x => x.Id == id);
        }

        public IEnumerable<EventViewModel> GetEventsOpenForRegistrationForUserInCompetitionInstance(int competitionInstanceId, string userId)
        {
            var events = (from e in _context.Events
                          join d in _context.Disciplines on e.DisciplineId equals d.Id
                          where e.CompetitionInstanceId == competitionInstanceId
                          where ((from cih in _context.ContestantsInHeats
                                  where cih.UserId == userId
                                  join h in _context.Heats on cih.HeatId equals h.Id
                                  join ev in _context.Events on h.EventId equals ev.Id
                                  where ev.Id == e.Id
                                  select cih).Count()) <= 0
                          select new EventViewModel
                          {
                              ActiveChip = e.ActiveChip,
                              CompetitionInstanceId = e.CompetitionInstanceId,
                              CourseId = e.CourseId,
                              DateFrom = e.DateFrom,
                              DateTo = e.DateTo,
                              DisciplineId = e.DisciplineId,
                              DisciplineName = d.Name,
                              DistanceOffset = e.DistanceOffset,
                              Gender = e.Gender,
                              Id = e.Id,
                              Laps = e.Laps,
                              Name = e.Name,
                              Splits = e.Splits,
                              StartInterval = e.StartInterval,
                              Deleted = e.Deleted,
                              Categories = (from c in _context.Categories
                                            where c.EventId == e.Id
                                            select c).ToList()
                          }).ToList();
            return events;

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
    }
}
