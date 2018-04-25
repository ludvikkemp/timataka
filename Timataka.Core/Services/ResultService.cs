using System.Collections.Generic;
using Timataka.Core.Data.Repositories;
using Timataka.Core.Models.ViewModels.HomeViewModels;
﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Timataka.Core.Data.Repositories;
using Timataka.Core.Models.Entities;
using Timataka.Core.Models.ViewModels;
using Timataka.Core.Models.ViewModels.AdminViewModels;
using Timataka.Core.Models.ViewModels.UserViewModels;

namespace Timataka.Core.Services
{
    public class ResultService : IResultService
    {
        private readonly IResultRepository _repo;

        public ResultService(IResultRepository repo)
        {
            _repo = repo;
        }

        /// <summary>
        /// Function to get all results for given event.
        /// </summary>
        /// <param name="eventId"></param>
        /// <returns>List of all results</returns>
        public IEnumerable<ResultViewModel> GetResultViewModelsForEvent(int eventId)
        {
            return _repo.GetResultViewModelsForEvent(eventId);
        }

        public IEnumerable<MyResultsViewModel> GetResultsForUser(string userId)
        {
            return _repo.GetResultsForUser(userId);
        }



        private readonly IAdminService _adminService;

        public ResultService(IResultRepository repo, IAdminService adminService)
        {
            _repo = repo;
            _adminService = adminService;
        }

        public async Task AddAsync(CreateResultViewModel model)
        {
            var u = await _adminService.GetUserByIdAsync(model.UserId);
            var c = _adminService.GetCountryNameById(u.Country);
            var n = _adminService.GetNationalityById(u.Nationality);
            var r = new Result
            {
                UserId = model.UserId,
                HeatId = model.HeatId,
                Club = "", //TODO
                Country = c,
                Created = DateTime.Now,
                FinalTime = "",
                Gender = u.Gender,
                Modified = DateTime.Now,
                Name = u.FirstName + " " + u.LastName,
                Nationality = n,
                Notes = "",
                Status = 0
            };
            await _repo.AddAsync(r);
        }

        public Result GetResult(string userId, int heatId)
        {
            return _repo.GetByUserIdAndHeatId(userId, heatId);
        }

        public async Task RemoveAsync(Result r)
        {
            await _repo.RemoveAsync(r);
        }


        public Boolean Edit(Result r)
        {
            return _repo.Edit(r);
        }
        public async Task<Boolean> EditAsync(Result r)
        {
            return await _repo.EditAsync(r);
        }

        public int NumberOfTimes()
        {
            return _repo.NumberOfTimes();
        }
    }
}
