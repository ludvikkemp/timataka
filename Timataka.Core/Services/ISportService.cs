using System.Collections.Generic;
using System.Threading.Tasks;
using Timataka.Core.Models.Entities;
using Timataka.Core.Models.ViewModels.AdminViewModels;

namespace Timataka.Core.Services
{
    public interface ISportService
    {
        Task<Sport> Add(SportsViewModel m);
        Task<Sport> Edit(Sport s);
        Task<int> Remove(int sportId);
        IEnumerable<SportsViewModel> GetAllSports();
        Task<Sport> GetSportByIdAsync(int sportId);

    }
}