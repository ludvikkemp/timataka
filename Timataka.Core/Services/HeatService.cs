using System;
using System.Collections.Generic;
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
            Heat heat = new Heat
            {

            }
        }

        public async Task<Heat> EditAsync(Heat h)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Heat> GetAllHeats()
        {
            throw new NotImplementedException();
        }

        public async Task<Heat> GetHeatByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Heat> GetHeatsForEvent(int eventId)
        {
            throw new NotImplementedException();
        }

        public async Task<int> RemoveAsync(int heatId)
        {
            throw new NotImplementedException();
        }
    }
}
