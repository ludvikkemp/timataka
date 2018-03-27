using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Timataka.Core.Data.Repositories;
using Timataka.Core.Models.Entities;
using Timataka.Core.Models.ViewModels.Marker;

namespace Timataka.Core.Services
{
    public class MarkerService : IMarkerService
    {
        private readonly IMarkerRepository _repo;

        public MarkerService(IMarkerRepository repo)
        {
            _repo = repo;
        }

        public async Task<Marker> AddAsync(MarkerCreateViewModel m)
        {
            return await _repo.AddAsync(new Marker
            {
                CompetitionInstanceId = m.CompetitionInstanceId,
                HeatId = m.HeatId,
                Location = m.Location,
                Time = m.Time,
                Type = m.Type
            });
        }

        public void AssignMarkerToHeat(Marker m, int heatId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> EditAsync(Marker m)
        {
            throw new NotImplementedException();
        }

        public Task<Marker> GetDeviceByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Marker> GetMarkers()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Marker> GetMarkersForCompetitionInstance(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Marker> GetmarkersForHeat(int id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> RemoveAsync(Marker m)
        {
            throw new NotImplementedException();
        }
    }
}
