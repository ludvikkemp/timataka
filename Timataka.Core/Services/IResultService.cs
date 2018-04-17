using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Timataka.Core.Models.Entities;
using Timataka.Core.Models.ViewModels;

namespace Timataka.Core.Services
{
    public interface IResultService
    {
        Task AddAsync(CreateResultViewModel model);
        Result GetResult(string userId, int heatId);
        Task RemoveAsync(Result r);
    }
}
