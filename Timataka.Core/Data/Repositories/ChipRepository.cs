using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Timataka.Core.Models.Entities;
using Timataka.Core.Models.ViewModels.ChipViewModels;

namespace Timataka.Core.Data.Repositories
{
    public class ChipRepository : IChipRepository
    {
        private bool _disposed = false;
        private readonly ApplicationDbContext _db;

        public ChipRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public Chip Add(Chip c)
        {
            _db.Chips.Add(c);
            _db.SaveChanges();
            return c;
        }

        public async Task<Chip> AddAsync(Chip c)
        {
            await _db.Chips.AddAsync(c);
            await _db.SaveChangesAsync();
            return c;
        }

        public bool AssignChipToUserInHeat(ChipInHeat c)
        {
            bool result = _db.ChipsInHeats.Add(c) != null;
            _db.SaveChanges();
            return result;
        }

        public async Task<bool> AssignChipToUserInHeatAsync(ChipInHeat c)
        {
            var exists = (from cih in _db.ChipsInHeats
                          where cih.ChipCode == c.ChipCode && cih.HeatId == c.HeatId
                          select cih).SingleOrDefault();
            if(exists != null)
            {
                throw new Exception("Chip already assigned to a contestant in this heat.");
            }
            await _db.ChipsInHeats.AddAsync(c);
            await _db.SaveChangesAsync();
            return true;
        }

        public bool EditChip(Chip c)
        {
            bool result = _db.Chips.Update(c) != null;
            _db.SaveChanges();
            return result;
        }

        public async Task<bool> EditChipAsync(Chip c)
        {
            bool result = _db.Chips.Update(c) != null;
            await _db.SaveChangesAsync();
            return result;
        }

        public bool EditChipInHeat(ChipInHeat c)
        {
            bool result = _db.ChipsInHeats.Update(c) != null;
            _db.SaveChanges();
            return result;
        }

        public async Task<bool> EditChipInHeatAsync(ChipInHeat c)
        {
            bool result = _db.ChipsInHeats.Update(c) != null;
            await _db.SaveChangesAsync();
            return result;
        }

        public Chip GetChipByCode(string code)
        {
            return (from c in _db.Chips
                    where c.Code == code
                    select c).SingleOrDefault();
        }

        public async Task<Chip> GetChipByCodeAsync(string code)
        {
            var result = await (from c in _db.Chips
                                where c.Code == code
                                select c).SingleOrDefaultAsync();
            return result;
        }


        public IEnumerable<Chip> Get()
        {
            return _db.Chips;
        }

        public Chip GetChipByNumber(int number)
        {
            return (from c in _db.Chips
                    where c.Number == number
                    select c).SingleOrDefault();
        }

        public async Task<Chip> GetChipByNumberAsync(int number)
        {
            var result = await(from c in _db.Chips
                               where c.Number == number
                               select c).SingleOrDefaultAsync();
            return result;
        }

        public IEnumerable<ChipViewModel> GetChips()
        {
            var chips = (from c in _db.Chips
                         join i in _db.CompetitionInstances on c.LastCompetitionInstanceId equals i.Id
                         into ix from i in ix.DefaultIfEmpty()
                         join u in _db.Users on c.LastUserId equals u.Id
                         into ux from u in ux.DefaultIfEmpty()
                         select new ChipViewModel
                         {
                            LastUserId = u == null ? "" : u.Id,
                            LastCompetitionInstanceId = i == null ? 0 : i.Id,
                            Active = c.Active,
                            Code = c.Code,
                            LastCompetitionInstanceName = i == null ? "" : i.Name,
                            LastSeen = c.LastSeen,
                            LastUserName = u == null ? "" : u.FirstName + " " + u.LastName,
                            LastUserSsn = u == null ? "" : u.Ssn,
                            Number = c.Number
                         }).ToList();
            return chips;
        }

        public IEnumerable<ChipInHeat> GetChipsInHeats()
        {
            return _db.ChipsInHeats.ToList();
        }


        public IEnumerable<ChipInHeatViewModel> GetChipsInHeat(int heatId)
        {
            var chipsInHeat = (from c in _db.ChipsInHeats
                         join h in _db.Heats on c.HeatId equals h.Id
                         join chips in _db.Chips on c.ChipCode equals chips.Code
                         join i in _db.CompetitionInstances on chips.LastCompetitionInstanceId equals i.Id
                         join u in _db.Users on chips.LastUserId equals u.Id
                         select new ChipInHeatViewModel
                         {
                             LastUserId = u.Id,
                             LastCompetitionInstanceId = i.Id,
                             Active = chips.Active,
                             ChipCode = chips.Code,
                             LastCompetitionInstanceName = i.Name,
                             LastSeen = chips.LastSeen,
                             LastUserName = u.FirstName + " " + u.MiddleName + " " + u.LastName,
                             LastUserSsn = u.Ssn,
                             Number = chips.Number,
                             HeatId = h.Id,
                             HeatNumber = h.HeatNumber,
                             Valid = c.Valid,
                             UserId = c.UserId
                         }).ToList();

            return chipsInHeat;
        }

        public async Task<ChipInHeat> GetChipInHeatByCodeAndUserId(string code, string userId)
        {
            var result = await (from h in _db.ChipsInHeats
                                where h.ChipCode == code && h.UserId == userId
                                select h).SingleOrDefaultAsync();
            return result;
        }


        public bool RemoveChip(Chip c)
        {
            bool result = _db.Chips.Remove(c) != null;
            _db.SaveChanges();
            return result;
        }

        public async Task<bool> RemoveChipAsync(Chip c)
        {
            bool result = _db.Chips.Remove(c) != null;
            await _db.SaveChangesAsync();
            return result;
        }

        public bool RemoveChipInHeat(ChipInHeat c)
        {
            bool result = _db.ChipsInHeats.Remove(c) != null;
            _db.SaveChanges();
            return result;
        }

        public async Task<bool> RemoveChipInHeatAsync(ChipInHeat c)
        {
            bool result = _db.ChipsInHeats.Remove(c) != null;
            await _db.SaveChangesAsync();
            return result;
        }

        public ChipInHeat GetChipInHeatByCodeUserIdHeatId(string modelOldChipCode, string userId, int modelOldHeatId)
        {
            var result = (from cih in _db.ChipsInHeats
                where cih.ChipCode == modelOldChipCode &&
                      cih.UserId == userId && cih.HeatId ==
                      modelOldHeatId
                select cih).FirstOrDefault();

            return result;
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

        public Task<int> GetInstanceIdForHeat(int heatId)
        {
            return (from h in _db.Heats
                    where h.Id == heatId
                    join e in _db.Events on h.EventId equals e.Id
                    join i in _db.CompetitionInstances on e.CompetitionInstanceId equals i.Id
                    select i.Id).SingleOrDefaultAsync();
        }
    }
}
