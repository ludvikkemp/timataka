using System.Collections.Generic;
using System.Threading.Tasks;
using Timataka.Core.Models.Entities;

namespace Timataka.Core.Services
{
    public interface ISportsService
    {
        Task<Sport> Add(Sport s);
        Task<int> Edit(Sport s);
        Task<int> Remove(int SportId);
        IEnumerable<Sport> GetAllSports();
        Task<Sport> GetSportById(int SportId);
    }
}