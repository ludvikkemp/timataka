using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Timataka.Core.Models.Entities;
using Timataka.Core.Models.ViewModels.DisciplineViewModels;

namespace Timataka.Core.Data.Repositories
{
    
    public class DisciplineRepository : IDisciplineRepository
    {
        private bool _disposed = false;
        private readonly ApplicationDbContext _db;

        public DisciplineRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public IEnumerable<Discipline> Get()
        {
            return _db.Disciplines.ToList();
        }

        public Discipline GetById(int id)
        {
            return _db.Disciplines.SingleOrDefault(x => x.Id == id);
        }

        public Task<Discipline> GetByIdAsync(int id)
        {
            return _db.Disciplines.SingleOrDefaultAsync(x => x.Id == id);
        }

        public void Insert(Discipline discipline)
        {
            _db.Disciplines.Add(discipline);
            _db.SaveChanges();
        }

        public async Task InsertAsync(Discipline d)
        {
            _db.Disciplines.Add(d);
            await _db.SaveChangesAsync();
        }

        public void Edit(Discipline d)
        {
            _db.Disciplines.Update(d);
            _db.SaveChanges();
        }

        public async Task EditAsync(Discipline d)
        {
            _db.Disciplines.Update(d);
            await _db.SaveChangesAsync();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this._disposed)
            {
                if (disposing)
                {
                    _db.Dispose();
                }
            }

            this._disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public void Remove(Discipline d)
        {
            d.Deleted = true;
            Edit(d);
        }

        public async Task RemoveAsync(Discipline d)
        {
            d.Deleted = true;
            await EditAsync(d);
        }

        public List<Sport> GetSports()
        {
            return _db.Sports.OrderBy(x => x.Name).ToList();
        }

        public IEnumerable<DisciplineViewModel> GetDisciplinesBySportId(int id)
        {
            var disciplines = (from d in _db.Disciplines
                where d.SportId == id
                select new DisciplineViewModel
                {
                    Id = d.Id,
                    Name = d.Name,
                    SportId = d.SportId
                }).ToList();
            return disciplines;
        }
    }
}
