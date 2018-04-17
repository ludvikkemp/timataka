using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Timataka.Core.Data.Repositories;
using Timataka.Core.Models.Entities;
using Timataka.Core.Models.ViewModels.ChipViewModels;

namespace Timataka.Core.Services
{
    public class ChipService : IChipService
    {
        private readonly IChipRepository _repo;
        private readonly IHeatService _heatService;
        private readonly IEventService _eventService;

        public ChipService(IChipRepository repo,
                            IHeatService heatService,
                            IEventService eventService)
        {
            _repo = repo;
            _heatService = heatService;
            _eventService = eventService;
        }

        public async Task<Chip> AddChipAsync(Chip c)
        {
            return await _repo.AddAsync(c);
        }

        public bool AssignChipToUserInHeat(ChipInHeat c)
        {
            return _repo.AssignChipToUserInHeat(c);
        }

        public async Task<ChipInHeatViewModel> AssignChipToUserInHeatAsync(ChipInHeat c)
        {
            if (await _repo.AssignChipToUserInHeatAsync(c))
            {
                var result = GetChipInHeatByCodeAndUserId(c.ChipCode, c.UserId, c.HeatId);
                return result;
            }
            else
            {
                throw new Exception("Failed");
            }
        }

        public async Task<bool> EditChipAsync(Chip c)
        {
            return await _repo.EditChipAsync(c);
        }

        public async Task<Chip> GetChipByCodeAsync(string code)
        {
            var result = await _repo.GetChipByCodeAsync(code);
            return result;
        }

        public ChipInHeatViewModel GetChipInHeatByCodeAndUserId(string code, string userId, int heatId)
        {
            var result = (from h in GetChipsInHeat(heatId)
                          where h.ChipCode == code && h.UserId == userId && h.HeatId == heatId
                          select h).SingleOrDefault();
            return result;
        }

        public IEnumerable<ChipViewModel> GetChips()
        {
            return _repo.GetChips();

        }

        public Boolean EditChipInHeat(ChipInHeat c)
        {
            return _repo.EditChipInHeat(c);
        }

        public IEnumerable<ChipInHeat> GetChipsInHeatsForUser(string userId)
        {
            return (from c in _repo.GetChipsInHeats()
                    where c.UserId == userId
                    select c).ToList();
        }

        public IEnumerable<ChipInHeat> GetChipsInHeats()
        {
            return _repo.GetChipsInHeats();
        }

        public IEnumerable<ChipInHeat> GetChipsInHeatsForUserInHeat(string userId, int heatId)
        {
            return (from c in GetChipsInHeatsForUser(userId)
                    where c.HeatId == heatId
                    select c).ToList();
        }

        public IEnumerable<ChipInHeat> GetChipsInHeatsForEvent(int eventId)
        {
            var heats = _heatService.GetHeatsForEvent(eventId);
            IEnumerable<ChipInHeat> result = Enumerable.Empty<ChipInHeat>();
            foreach (var item in heats)
            {
                var r = GetChipsInHeatForHeat(item.Id);
                result.Concat(r);
            }
            return result;
        }

        public IEnumerable<Chip> Get()
        {
            return _repo.Get();
        }

        public IEnumerable<ChipInHeat> GetChipsInHeatForHeat(int heatId)
        {
            return (from c in GetChipsInHeats()
                    where c.HeatId == heatId
                    select c).ToList();
        }
        
        public IEnumerable<ChipInHeatViewModel> GetChipsInHeat(int heatId)
        {
            return _repo.GetChipsInHeat(heatId);
        }

        public IEnumerable<ChipInHeat> GetChipsInCompetitionInstanceForUser(int competitionInstanceId, string userId)
        {
            var events = _eventService.GetEventsByCompetitionInstanceId(competitionInstanceId);
            IEnumerable<ChipInHeat> result = Enumerable.Empty<ChipInHeat>();
            foreach(var i in events)
            {
                var heats = _heatService.GetHeatsForEvent(i.Id);
                foreach(var j in heats)
                {
                    result.Concat(GetChipsInHeatsForUserInHeat(userId, j.Id));
                }
            }
            return result;
        }

        public Boolean RemoveChipInHeat(ChipInHeat c)
        {
            return _repo.RemoveChipInHeat(c);
        }

        public async Task<Chip> GetChipByNumberAsync(int modelNumber)
        {
            return await _repo.GetChipByNumberAsync(modelNumber);
        }

        public async Task<bool> MarkInvalid(ChipInHeat c)
        {
            c.Valid = false;
            return await _repo.EditChipInHeatAsync(c);
        }

        public async Task<bool> RemoveChipAsync(Chip c)
        {
            return await _repo.RemoveChipAsync(c);
        }

        public async Task<bool> UpdateChipStory(ChipInHeat c)
        {
            Chip chip = await GetChipByCodeAsync(c.ChipCode);
            chip.LastCompetitionInstanceId = c.Heat.Event.CompetitionInstanceId;
            chip.LastSeen = DateTime.Now;
            chip.LastUserId = c.UserId;
            return await _repo.EditChipAsync(chip);
        }
    }
}
