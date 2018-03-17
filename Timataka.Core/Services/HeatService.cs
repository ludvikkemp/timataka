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

        public async Task<Heat> AddAsync(CreateHeatViewModel h)
        {
            IEnumerable<Heat> heats = GetHeatsForEvent(h.EventId);
            int nextHeatNumber;
            if(heats == null)
            {
                //For first heat
                nextHeatNumber = 1;
            }
            else
            {
                nextHeatNumber = heats.Last().HeatNumber + 1;
            }
            
            Heat heat = new Heat
            {
                HeatNumber = nextHeatNumber,
                EventId = h.EventId,
                Deleted = false
            };
            await _repo.InsertAsync(heat);
            return heat;
        }

        public async Task<Heat> EditAsync(Heat h)
        {
            await _repo.EditAsync(h);
            return h;
        }

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
                               where x.EventId.Equals(eventId)
                               select x;
            return heatsInEvent;  
        }

        public async Task<int> RemoveAsync(int heatId)
        {
            var heat = await GetHeatByIdAsync(heatId);
            await EditAsync(heat);
            return heatId;
        }

        public async void ReorderHeats(int eventId)
        {
            IEnumerable<Heat> heats = GetHeatsForEvent(eventId);
            int heatNumber = 1;
            foreach (var item in heats)
            {
                item.HeatNumber = heatNumber;
                await EditAsync(item);
                heatNumber++;
            }

        }
    }
}
