using System.Collections.Generic;
using System.Threading.Tasks;
using Timataka.Core.Models.Entities;

namespace Timataka.Core.Services
{
    public interface ISportService
    {
        Task<Sport> Add(Sport s);
        Task<Sport> Edit(Sport s);
        int Remove(int sportId);
        IEnumerable<Sport> GetAllSports();
        Sport GetSportById(int sportId);
    }
}