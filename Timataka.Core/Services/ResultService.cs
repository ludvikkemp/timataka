using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Timataka.Core.Data.Repositories;
using Timataka.Core.Models.Entities;
using Timataka.Core.Models.ViewModels;

namespace Timataka.Core.Services
{
    public class ResultService : IResultService
    {
        private readonly IResultRepository _repo;
        private readonly IAdminService _adminService;

        public ResultService(IResultRepository repo, IAdminService adminService)
        {
            _repo = repo;
            _adminService = adminService;
        }

        public void Add(CreateResultViewModel model)
        {
            var u = _adminService.GetUserById(model.UserId);
            var c = _adminService.GetCountryNameById(u.Country);
            var n = _adminService.GetNationalityById(u.Nationality);
            var r = new Result
            {
                UserId = u.Id,
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
            _repo.Add(r);
        }

        public Result GetResult(string userId, int heatId)
        {
            return _repo.GetByUserIdAndHeatId(userId, heatId);
        }
    }
}
