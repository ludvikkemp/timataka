using System.Collections.Generic;
using Timataka.Core.Models.ViewModels.HomeViewModels;
using System.Threading.Tasks;
using Timataka.Core.Models.Entities;
using Timataka.Core.Models.ViewModels;
using System;
using Timataka.Core.Models.ViewModels.UserViewModels;

namespace Timataka.Core.Services
{
    public interface IResultService
    {
        IEnumerable<ResultViewModel> GetResultViewModelsForEvent(int eventId);
        IEnumerable<MyResultsViewModel> GetResultsForUser(string userId);
        Task AddAsync(CreateResultViewModel model);
        Result GetResult(string userId, int heatId);
        Task RemoveAsync(Result r);
        Boolean Edit(Result r);
        Task<Boolean> EditAsync(Result r);

        void GetTimes();




    }
}
