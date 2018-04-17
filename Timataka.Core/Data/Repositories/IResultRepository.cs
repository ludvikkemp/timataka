﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Timataka.Core.Models.Entities;
using Timataka.Core.Models.ViewModels.AdminViewModels;

namespace Timataka.Core.Data.Repositories
{
    public interface IResultRepository : IDisposable
    {
        Result Add(Result r);
        Task<Result> AddAsync(Result r);

        IEnumerable<Result> Get();

        Result GetByUserIdAndHeatId(string userId, int heatId);
        Task<Result> GetByUserIdAndHeatIdAsync(string userId, int heatId);

        Boolean Edit(Result r);
        Task<Boolean> EditAsync(Result r);

        Boolean Remove(Result r);
        Task<Boolean> RemoveAsync(Result r);
    }
}