using System.Collections.Generic;
using Timataka.Core.Models.ViewModels.HomeViewModels;

namespace Timataka.Core.Services
{
    public interface IResultService
    {
        IEnumerable<ResultViewModel> GetResultViewModelsForEvent(int eventId);
    }
}
