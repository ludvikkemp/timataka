using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Timataka.Core.Models.Entities;

namespace Timataka.Core.Services
{
    public interface IHeatService
    {
        Task<Heat> Add(Heat h);
        Task<Heat> Edit(Heat h);
        Task<int> Remove(int heatId);
        IEnumerable<Heat> GetAllHeats();
        IEnumerable<Heat> GetHeatsForEvent(int eventId);
        Task<Heat> GetHeatById(int id);

    }
}
