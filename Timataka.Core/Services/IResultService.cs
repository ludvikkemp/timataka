using System.Collections.Generic;
using Timataka.Core.Models.ViewModels.HomeViewModels;
using System.Threading.Tasks;
using Timataka.Core.Models.Entities;
using Timataka.Core.Models.ViewModels;
using System;

namespace Timataka.Core.Services
{
    public interface IResultService
    {
        IEnumerable<ResultViewModel> GetResultViewModelsForEvent(int eventId);
        Task AddAsync(CreateResultViewModel model);
        Result GetResult(string userId, int heatId);
        Task RemoveAsync(Result r);
        Boolean Edit(Result r);
        Task<Boolean> EditAsync(Result r);
        int NumberOfTimes();

    }
}
