using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Timataka.Core.Data.Repositories;
using Timataka.Core.Models.Entities;
using Timataka.Core.Models.ViewModels.EventViewModels;
using Timataka.Core.Models.ViewModels.MarkerViewModels;

namespace Timataka.Core.Services
{
    public class MarkerService : IMarkerService
    {
        private readonly IMarkerRepository _repo;
        private readonly IHeatService _heatService;
        private readonly IEventService _eventService;

        public MarkerService(IMarkerRepository repo,
            IHeatService heatService,
            IEventService eventService)
        {
            _repo = repo;
            _heatService = heatService;
            _eventService = eventService;
        }

        public Boolean IsAssigned(int markerId, int heatId)
        {
            return (from x in _repo.GetMarkersInHeats()
                where x.MarkerId == markerId && x.HeatId == heatId
                select x).Any();
        }

        // *** ADD *** //
        public async Task<Marker> AddAsync(Marker m)
        {
            return await _repo.AddAsync(m);
        }

        public async Task<Boolean> AssignMarkerToHeatAsync(AssignMarkerToHeatViewModel model)
        {
            var m = new MarkerInHeat { MarkerId = model.MarkerId, HeatId = model.HeatId };
            return await _repo.AddMarkerInHeatAsync(m) != null;
        }

        // *** EDIT *** //
        public async Task<bool> EditAsync(Marker m)
        {
            return await _repo.EditAsync(m);
        }

        // *** GET *** //
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
            var result = (from markersInHeat in _repo.GetMarkersInHeats()
                          join m in GetMarkers() on markersInHeat.MarkerId equals m.Id
                          where markersInHeat.HeatId == id
                          select m).ToList();
            return result;
        }

        public IEnumerable<MarkersInEventViewModel> GetMarkersForEvent(int id)
        {
            var heats = _heatService.GetHeatsForEvent(id);
            var e = _eventService.GetEventById(id);
            var result = (from m in GetMarkersForCompetitionInstance(e.CompetitionInstanceId)
                          join markersInHeat in _repo.GetMarkersInHeats() on m.Id equals markersInHeat.MarkerId
                          join h in heats on markersInHeat.HeatId equals h.Id
                          select new MarkersInEventViewModel
                          {
                              HeatNumber = h.HeatNumber,
                              Marker = m
                          }).ToList();
            return result;
        }

        public IEnumerable<EventHeatViewModel> GetEventHeatListForMarker(int markerId, int competitionInstanceId)
        {
            var heatList = _eventService.GetEventHeatListForCompetitionInstance(competitionInstanceId);
            var r = (from x in heatList
                where IsAssigned(markerId, x.HeatId) == false
                select x).ToList();
            return r;
        }

        public IEnumerable<Marker> GetUnAssignedMarkersForHeat(int heatId, int competitionInstanceId)
        {
            var markerList = (from m in GetMarkersForCompetitionInstance(competitionInstanceId)
                where IsAssigned(m.Id, heatId) == false
                select m).ToList();
            return markerList;
        }

        public async Task GetMarkersFromTimingDb(int competitionInstanceId)
        {
            var results = (from m in _repo.GetMarkersFromTimingDb()
                where m.CompetitionInstanceId == competitionInstanceId
                select m).ToList();
            foreach (var item in results)
            {
                if ((from m in GetMarkersForCompetitionInstance(competitionInstanceId)
                        where m.Time == item.Time
                        select m).SingleOrDefault() == null)
                {
                    await AddAsync(item);
                }
            }
        }


        // *** REMOVE & UNASSIGN *** //
        public async Task<Boolean> RemoveAsync(Marker m)
        {
            return await _repo.RemoveAsync(m);
        }
        
        public async Task<Boolean> UnassignMarkerAsync(AssignMarkerToHeatViewModel model)
        {
            var result = await _repo.RemoveMarkerInHeatAsync(new MarkerInHeat { HeatId = model.HeatId, MarkerId = model.MarkerId });
            return result;
        }

       
    }
}
