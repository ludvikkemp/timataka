using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Timataka.Core.Models.Entities;

namespace Timataka.Core.Data.Repositories
{
    public interface IMarkerRepository : IDisposable
    {
        Marker Add(Marker m);
        Task<Marker> AddAsync(Marker m);

        IEnumerable<Marker> Get();

        Marker GetById(int id);
        Task<Marker> GetByIdAsync(int id);

        Boolean Edit(Marker m);
        Task<Boolean> EditAsync(Marker m);

        Boolean Remove(Marker m);
        Task<Boolean> RemoveAsync(Marker m);

        MarkerInHeat AddMarkerInHeat(MarkerInHeat m);
        Task<MarkerInHeat> AddMarkerInHeatAsync(MarkerInHeat m);

        IEnumerable<MarkerInHeat> GetMarkersInHeats();

        MarkerInHeat GetMarkerInHeat(int markerId, int heatId);

        Boolean RemoveMarkerInHeat(MarkerInHeat m);
        Task<Boolean> RemoveMarkerInHeatAsync(MarkerInHeat m);

        IEnumerable<Marker> GetMarkersFromTimingDb();
    }
}
