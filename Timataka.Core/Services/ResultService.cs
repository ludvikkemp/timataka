using System.Collections.Generic;
using Timataka.Core.Data.Repositories;
using Timataka.Core.Models.ViewModels.HomeViewModels;
using System;
using System.Threading.Tasks;
using Timataka.Core.Models.Entities;
using Timataka.Core.Models.ViewModels;
using System.Linq;
using Timataka.Core.Models.ViewModels.UserViewModels;
using Timataka.Core.Models.ViewModels.ChipViewModels;

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
                Club = null, //TODO
                Country = c,
                Created = DateTime.Now,
                FinalTime = null,
                Gender = u.Gender,
                Modified = DateTime.Now,
                Name = u.FirstName + " " + u.LastName,
                Nationality = n,
                Notes = null,
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

        //TimingDB

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
                    heats = _repo.GetHeatsInCompetitionInstance(r.CompetitionInstanceId);
                }
                int counter = 0;
                foreach (var h in heats)
                {
                    //Can only have one entry for a single chip
                    IEnumerable<ChipInHeatViewModel> exists = (from c in _repo.GetChipsInHeat(h.Id)
                                                      where r.ChipCode == c.ChipCode
                                                      select c).ToList();
                    if(exists.Count() == 0)
                    {
                        break;
                    }
                    var existsInHeat = (from e in exists
                                  where e.HeatId == h.Id
                                  select e).SingleOrDefault();
                    if (existsInHeat != null)
                    {
                        if (r.Time01 != 0)
                        {
                            Time time01 = new Time { ChipCode = existsInHeat.ChipCode, HeatId = h.Id, RawTime = r.Time01, TimeNumber = 1, Type = TimeType.Start };
                            Time time = _repo.GetTime(h.Id, existsInHeat.ChipCode, 1);
                            //Add time if it does not exist
                            if (time == null)
                            {
                                _repo.AddTime(time01);
                            }
                            //Update time if different
                            else if (time01.RawTime != time.RawTime)
                            {
                                _repo.Remove(h.Id, existsInHeat.ChipCode, 1);
                                _repo.AddTime(time01);
                            }
                            
                        }
                        if (r.Time02 != 0)
                        {
                            Time time02 = new Time { ChipCode = existsInHeat.ChipCode, HeatId = h.Id, RawTime = r.Time02, TimeNumber = 2, Type = TimeType.Finish };
                            Time time = _repo.GetTime(h.Id, existsInHeat.ChipCode, 2);
                            //Add time if it does not exist
                            if (time == null)
                            {
                                _repo.AddTime(time02);
                            }
                            //Update time if different
                            else if (time02.RawTime != time.RawTime)
                            {
                                _repo.Remove(h.Id, existsInHeat.ChipCode, 1);
                                _repo.AddTime(time02);
                            }
                        }
                        counter++;
                    }
                    if (counter == exists.Count())
                    {
                        counter = 0;
                        break;
                    }
                }
            }
        }
    }
}
