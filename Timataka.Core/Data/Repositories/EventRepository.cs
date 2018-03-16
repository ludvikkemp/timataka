using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Timataka.Core.Models.Entities;

namespace Timataka.Core.Data.Repositories
{
    public class EventRepository : IEventRepository
    {
        private readonly ApplicationDbContext _context;
        private bool _disposed = false;

        public EventRepository(ApplicationDbContext context)
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

        public async Task InsertAsync(Event entity)
        {
            await _context.Event.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<Event> GetEventByNameAsync(string eventName)
        {
            return await _context.Event.SingleOrDefaultAsync(x => x.Name == eventName);
        }

        public async Task<Event> GetByIdAsync(int id)
        {
            return await _context.Event.SingleOrDefaultAsync(x => x.Id == id);
        }

        public void Edit(Event entity)
        {
            _context.Event.Update(entity);
            _context.SaveChanges();
        }

        public async Task EditAsync(Event entity)
        {
            _context.Event.Update(entity);
            await _context.SaveChangesAsync();
        }

        public void Remove(Event entity)
        {
            //TODO:Mark as removed, not delete compleatly
            throw new NotImplementedException();
        }

        public Task RemoveAsync(Event entity)
        {
            //TODO:Mark as removed, not delete compleatly
            throw new NotImplementedException();
        }
        public IEnumerable<Event> Get()
        {
            return _context.Events.ToList();
        }

        public IEnumerable<Event> GetEventsForInstance(int id)
        {
            return _context.Events.Where(x => x.CompetitionInstanceId == id).ToList();
        }
    }
}
