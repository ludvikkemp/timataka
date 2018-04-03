using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Timataka.Core.Data.Repositories;
using Timataka.Core.Models.Entities;
using Timataka.Core.Models.ViewModels.HeatViewModels;

namespace Timataka.Core.Services
{
    public class HeatService : IHeatService
    {
        private readonly IHeatRepository _repo;

        public HeatService(IHeatRepository repo)
        {
            _repo = repo;
        }

        public HeatService()
        {
            //For unit tests
        }

        /// <summary>
        /// Add heat to a event. If no heat exists for that event
        /// a heat with HeatNumber = 0 is created.
        /// </summary>
        /// <param name="eventId"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Function to edit a heat
        /// </summary>
        /// <param name="h"></param>
        /// <returns>Edited heat</returns>
        public async Task<Heat> EditAsync(Heat h)
        {
            await _repo.EditAsync(h);
            return h;
        }

        /// <summary>
        /// Function to get all heats
        /// </summary>
        /// <returns>List of all heats</returns>
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

        public async Task<int> RemoveAsync(int heatId)
        {
            var heat = await GetHeatByIdAsync(heatId);
            await _repo.RemoveAsync(heat);
            return heatId;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="eventId"></param>
        public async Task ReorderHeatsAsync(int eventId)
        {
            IEnumerable<Heat> heats = GetHeatsForEvent(eventId);
            int heatNumber = 0;
            foreach (var item in heats)
            {
                item.HeatNumber = heatNumber;
                await EditAsync(item);
                heatNumber++;
            }

        }

        public IEnumerable<ContestantsInHeatViewModel> GetContestantsInHeat(int id)
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

        public void EditContestantInHeat(ContestantInHeat h)
        {
            _repo.EditContestantInHeat(h);
        }

        public async Task EditAsyncContestantInHeat(ContestantInHeat h)
        {
            await _repo.EditAsyncContestantInHeat(h);
        }

        public void RemoveContestantInHeat(ContestantInHeat h)
        {
            _repo.RemoveContestantInHeat(h);
        }
        public async Task RemoveAsyncContestantInHeat(ContestantInHeat h)
        {
            await _repo.RemoveAsyncContestantInHeat(h);
        }

        public void AddContestantInHeat(ContestantInHeat h)
        {
            _repo.InsertContestantInHeat(h);
        }
        public async Task AddAsyncContestantInHeat(ContestantInHeat h)
        {
            await _repo.InsertAsyncContestantInHeat(h);
        }

    }
}
