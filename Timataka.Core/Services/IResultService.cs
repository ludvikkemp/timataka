using System.Collections.Generic;
using Timataka.Core.Models.ViewModels.HomeViewModels;
using System.Threading.Tasks;
using Timataka.Core.Models.Entities;
using Timataka.Core.Models.ViewModels;

namespace Timataka.Core.Services
{
    public interface IResultService
    {
        IEnumerable<ResultViewModel> GetResultViewModelsForEvent(int eventId);
        Task AddAsync(CreateResultViewModel model);
        Result GetResult(string userId, int heatId);
        Task RemoveAsync(Result r);
        int NumberOfTimes();
    }
}
