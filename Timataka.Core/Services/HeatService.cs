using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Timataka.Core.Data.Repositories;
using Timataka.Core.Models.Entities;
using Timataka.Core.Models.ViewModels;
using Timataka.Core.Models.ViewModels.EventViewModels;
using Timataka.Core.Models.ViewModels.HeatViewModels;

namespace Timataka.Core.Services
{
    public class HeatService : IHeatService
    {
        private readonly IHeatRepository _repo;
        private readonly IResultService _resultService;

        public HeatService(IHeatRepository repo, 
            IResultService resultService)
        {
            _repo = repo;
            _resultService = resultService;
        }

        public HeatService()
        {
            //For unit tests
        }

        public async Task ReorderHeatsAsync(int eventId)
        {
            var heats = GetHeatsForEvent(eventId);
            var heatNumber = 0;
            foreach (var item in heats)
            {
                item.HeatNumber = heatNumber;
                await EditAsync(item);
                heatNumber++;
            }

        }


        // *** ADD *** //
        public async Task<Heat> AddAsync(int eventId)
        {
            IEnumerable<Heat> heats = GetHeatsForEvent(eventId);
            int nextHeatNumber;
            if(heats.Count() == 0)
            {
                //For first heat
                nextHeatNumber = 0;
            }
            else
            {
                nextHeatNumber = heats.Last().HeatNumber + 1;
            }
            
            Heat heat = new Heat
            {
                HeatNumber = nextHeatNumber,
                EventId = eventId,
                Deleted = false,
                
            };
            await _repo.InsertAsync(heat);
            return heat;
        }

        public void AddContestantInHeat(ContestantInHeat h)
        {
            _repo.InsertContestantInHeat(h);
            var result = new CreateResultViewModel
            {
                UserId = h.UserId,
                HeatId = h.HeatId,
            };
            _resultService.AddAsync(result);
        }

        public async Task AddAsyncContestantInHeat(ContestantInHeat h)
        {
            await _repo.InsertAsyncContestantInHeat(h);
            var result = new CreateResultViewModel
            {
                UserId = h.UserId,
                HeatId = h.HeatId,
            };
            await _resultService.AddAsync(result);
        }


        // *** EDIT *** //
        public async Task<Heat> EditAsync(Heat h)
        {
            await _repo.EditAsync(h);
            return h;
        }

        public void EditContestantInHeat(ContestantInHeat h)
        {
            _repo.EditContestantInHeat(h);
            //TODO: Update result for contestant
        }

        public async Task EditAsyncContestantInHeat(ContestantInHeat h)
        {
            await _repo.EditAsyncContestantInHeat(h);
            //TODO: Update result for contestant
        }


        // *** GET *** //
        public IEnumerable<Heat> GetAllHeats()
        {
            var heats = _repo.Get();
            return heats;
        }

        public async Task<Heat> GetHeatByIdAsync(int id)
        {
            var heat = await _repo.GetByIdAsync(id);
            return heat;
        }

        public IEnumerable<Heat> GetHeatsForEvent(int eventId)
        {
            IEnumerable<Heat> heats = GetAllHeats();
            var heatsInEvent = from x in heats
                               where x.EventId.Equals(eventId) && x.Deleted.Equals(false)
                               select x;
            return heatsInEvent;  
        }

        public IEnumerable<Heat> GetDeletedHeatsForEvent(int eventId)
        {
            IEnumerable<Heat> heats = GetAllHeats();
            var heatsInEvent = from x in heats
                               where x.EventId.Equals(eventId) && x.Deleted.Equals(true)
                               select x;
            return heatsInEvent;
        }

        public IEnumerable<ApplicationUser> GetUsersNotInAnyHeatUnderEvent(int eventId)
        {
            return _repo.GetUsersNotInAnyHeatUnderEvent(eventId);
        }

        public async Task<ContestantInEventViewModel> GetContestantInEventViewModelAsync(string userId, int heatId)
        {
            var x = GetContestantInHeatById(heatId, userId);
            var h = await GetHeatByIdAsync(heatId);
            var r = _resultService.GetResult(userId, heatId);
            //var c = _chipService.GetChipsInHeatsForUserInHeat(userId, heatId).SingleOrDefault();
            //var e = _eventService.GetEventById(h.EventId);

            var result = new ContestantInEventViewModel
            {
                Bib = x.Bib,
                HeatId = heatId,
                HeatNumber = h.HeatNumber,
                Modified = x.Modified,
                Team = x.Team,
                Notes = r.Notes,
                Status = r.Status,
                ChipCode = "",
                EventId = h.EventId,
                EventName = "",
                HeatsInEvent = GetHeatsForEvent(h.EventId)
            };

            return result;

        }

        public IEnumerable<ContestantInHeatViewModel> GetContestantsInHeat(int id)
        {
            return _repo.GetContestantsInHeat(id);
        }

        public IEnumerable<ApplicationUser> GetApplicationUsersInHeat(int id)
        {

            return _repo.GetApplicationUsersInHeat(id);
        }

        public ContestantInHeat GetContestantInHeatById(int heatId, string userId)
        {
            return _repo.GetContestantInHeatById(heatId, userId);
        }

        public ContestantInHeat GetContestantsInHeatByUserIdAndHeatId(string userId, int heatId)
        {
            return _repo.GetContestantInHeatByUserId(userId, heatId);
        }


        // *** REMOVE *** //
        public async Task<int> RemoveAsync(int heatId)
        {
            var heat = await GetHeatByIdAsync(heatId);
            await _repo.RemoveAsync(heat);
            return heatId;
        }

        public void RemoveContestantInHeat(ContestantInHeat h)
        {
            var r = _resultService.GetResult(h.UserId, h.HeatId);
            _resultService.RemoveAsync(r);
            _repo.RemoveContestantInHeat(h);
        }

        public async Task RemoveAsyncContestantInHeat(ContestantInHeat h)
        {
            var r = _resultService.GetResult(h.UserId, h.HeatId);
            await _resultService.RemoveAsync(r);
            await _repo.RemoveAsyncContestantInHeat(h);
        }

    }
}
