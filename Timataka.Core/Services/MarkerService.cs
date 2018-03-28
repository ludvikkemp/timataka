using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Timataka.Core.Data.Repositories;
using Timataka.Core.Models.Entities;
using Timataka.Core.Models.ViewModels.MarkerViewModels;

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

        public async void AssignMarkerToHeat(Marker m, int heatId)
        {
            if(m.HeatId == null)
            {
                m.HeatId = heatId;
                await EditAsync(m);
            }
            else
            {
                var newMarker = await DuplicateMarker(m);
                newMarker.HeatId = heatId;
                await EditAsync(m);
            }
        }

        public async Task<bool> EditAsync(Marker m)
        {
            return await _repo.EditAsync(m);
        }

        public async Task<Marker> GetMarkerByIdAsync(int id)
        {
            return await _repo.GetByIdAsync(id);
        }

        public IEnumerable<Marker> GetMarkers()
        {
            return _repo.Get();
        }

        public IEnumerable<Marker> GetMarkersForCompetitionInstance(int id)
        {
            var result = (from m in GetMarkers()
                          where m.CompetitionInstanceId == id
                          select m).ToList();
            return result;
        }

        public IEnumerable<Marker> GetMarkersForHeat(int id)
        {
            var result = (from m in GetMarkers()
                          where m.HeatId == id
                          select m).ToList();
            return result;
        }

        public async Task<Boolean> RemoveAsync(Marker m)
        {
            return await _repo.RemoveAsync(m);
        }
        
        public async Task<Marker> DuplicateMarker(Marker m)
        {
            var newMarker = new Marker
            {
                CompetitionInstanceId = m.CompetitionInstanceId,
                Location = m.Location,
                Time = m.Time,
                Type = m.Type
            };
            await _repo.AddAsync(newMarker);
            return newMarker;
        }

        public IEnumerable<Marker> GetUnassignedMarkers(int id)
        {
            var result = (from m in GetMarkers()
                          where m.CompetitionInstanceId == id && m.HeatId == null
                          select m).ToList();
            return result;
        }

    }
}
