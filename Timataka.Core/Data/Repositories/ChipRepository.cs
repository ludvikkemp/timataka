﻿using Microsoft.EntityFrameworkCore;
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
            var result = false;
            if (_db.ChipsInHeats.Add(c) != null)
            {
                result = true;
            }
            _db.SaveChanges();
            return result;
        }

        public async Task<bool> AssignChipToUserInHeatAsync(ChipInHeat c)
        {
            var result = false;
            if (await _db.ChipsInHeats.AddAsync(c) != null)
            {
                result = true;
            }
            await _db.SaveChangesAsync();
            return result;
        }

        public bool EditChip(Chip c)
        {
            var result = false;
            if (_db.Chips.Update(c) != null)
            {
                result = true;
            }
            _db.SaveChanges();
            return result;
        }

        public async Task<bool> EditChipAsync(Chip c)
        {
            var result = false;
            if (_db.Chips.Update(c) != null)
            {
                result = true;
            }
            await _db.SaveChangesAsync();
            return result;
        }

        public bool EditChipInHeat(ChipInHeat c)
        {
            var result = false;
            if (_db.ChipsInHeats.Update(c) != null)
            {
                result = true;
            }
            _db.SaveChanges();
            return result;
        }

        public async Task<bool> EditChipInHeatAsync(ChipInHeat c)
        {
            var result = false;
            if (_db.ChipsInHeats.Update(c) != null)
            {
                result = true;
            }
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
                         join u in _db.Users on c.LastUserId equals u.Id
                         select new ChipViewModel
                         {
                            LastUserId = u.Id,
                            LastCompetitionInstanceId = i.Id,
                            Active = c.Active,
                            Code = c.Code,
                            LastCompetitionInstanceName = i.Name,
                            LastSeen = c.LastSeen,
                            LastUserName = u.FirstName + " " + u.MiddleName + " " + u.LastName,
                            LastUserSsn = u.Ssn,
                            Number = c.Number
                         }).ToList();
            return chips;
        }

        public IEnumerable<ChipInHeat> GetChipsInHeats()
        {
            return _db.ChipsInHeats.ToList();
        }

        public bool RemoveChip(Chip c)
        {
            var result = false;
            if (_db.Chips.Remove(c) != null)
            {
                result = true;
            }
            _db.SaveChanges();
            return result;
        }

        public async Task<bool> RemoveChipAsync(Chip c)
        {
            var result = false;
            if (_db.Chips.Remove(c) != null)
            {
                result = true;
            }
            await _db.SaveChangesAsync();
            return result;
        }

        public bool RemoveChipInHeat(ChipInHeat c)
        {
            var result = false;
            if (_db.ChipsInHeats.Remove(c) != null)
            {
                result = true;
            }
            _db.SaveChanges();
            return result;
        }

        public async Task<bool> RemoveChipInHeatAsync(ChipInHeat c)
        {
            var result = false;
            if (_db.ChipsInHeats.Remove(c) != null)
            {
                result = true;
            }
            await _db.SaveChangesAsync();
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


    }
}
