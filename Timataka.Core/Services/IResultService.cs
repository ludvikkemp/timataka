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
        void Add(CreateResultViewModel model);
        Result GetResult(string userId, int heatId);
    }
}
