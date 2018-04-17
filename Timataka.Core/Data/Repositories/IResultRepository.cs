using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Timataka.Core.Models.Entities;
using Timataka.Core.Models.ViewModels.HomeViewModels;

namespace Timataka.Core.Data.Repositories
{
    public interface IResultRepository : IDisposable
    {
        Result Add(Result r);
        Task<Result> AddAsync(Result r);

        IEnumerable<Result> Get();

        Result GetById(int id);
        Task<Result> GetByIdAsync(int id);

        Boolean Edit(Result r);
        Task<Boolean> EditAsync(Result r);

        Boolean Remove(Result r);
        Task<Boolean> RemoveAsync(Result r);

        IEnumerable<ResultViewModel> GetResultViewModelsForEvent(int eventId);
    }
}
