using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Timataka.Core.Models.Entities;
using Timataka.Core.Models.ViewModels.MarkerViewModels;

namespace Timataka.Core.Services
{
    public interface IMarkerService
    {
        Task<Marker> AddAsync(Marker m);
        Task<Boolean> EditAsync(Marker m);
        Task<Boolean> RemoveAsync(Marker m);

        Task<Marker> GetMarkerByIdAsync(int id);
        IEnumerable<Marker> GetMarkers();
        IEnumerable<Marker> GetMarkersForCompetitionInstance(int id);
        IEnumerable<Marker> GetMarkersForHeat(int id);
        IEnumerable<MarkersInEventViewModel> GetMarkersForEvent(int id);

        void AssignMarkerToHeat(int markerId, int heatId);

    }
}
