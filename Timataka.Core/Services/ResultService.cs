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
using Timataka.Core.Models.ViewModels.ResultViewModels;
using Timataka.Core.Models.ViewModels.ChipViewModels;
using System.Linq;

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
        
       
        private readonly IAdminService _adminService;

        public ResultService(IResultRepository repo, IAdminService adminService)
        {
            _repo = repo;
            _adminService = adminService;
        }

        private readonly ICompetitionService _competitionService;
        private readonly IChipService _chipService;

        public ResultService(IResultRepository repo, IAdminService adminService, ICompetitionService competitionService, IChipService chipService)
        {
            _repo = repo;
            _competitionService = competitionService;
            _adminService = adminService;
            _chipService = chipService;
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

        public async Task EditAsync(Result r)
        {
            await _repo.EditAsync(r);
        }

        //TimingDB

        public int NumberOfTimes()
        {
            return _repo.NumberOfTimes();
        }

        public void GetTimes()
        {
            var results = _repo.GetResultsFromTimingDb();
            int competitionInstanceId = 0;
            IEnumerable<Heat> heats = null;
            foreach(var r in results)
            {
                if(competitionInstanceId == 0 || competitionInstanceId != r.CompetitionInstanceId)
                {
                    competitionInstanceId = r.CompetitionInstanceId;
                    heats = _competitionService.GetHeatsInCompetitionInstance(r.CompetitionInstanceId);
                }
                foreach (var h in heats)
                {
                    //Can only have one entry for a single chip
                    var exists = (from c in _chipService.GetChipsInHeat(h.Id)
                                  where r.ChipCode == c.ChipCode
                                  select c).SingleOrDefault();
                    if (exists != null)
                    {
                        if (r.Time01 != 0)
                        {
                            Time time01 = new Time { ChipCode = exists.ChipCode, HeatId = h.Id, RawTime = r.Time01, TimeNumber = 1, Type = TimeType.Start };
                            _repo.AddTime(time01);
                        }
                        if (r.Time02 != 0)
                        {
                            Time time02 = new Time { ChipCode = exists.ChipCode, HeatId = h.Id, RawTime = r.Time02, TimeNumber = 2, Type = TimeType.Finish };
                            _repo.AddTime(time02);
                        }
                    }
                }
            }

        }
    }
}
