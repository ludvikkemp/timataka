using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Timataka.Core.Data.Repositories;
using Timataka.Core.Models.Entities;
using Timataka.Core.Models.ViewModels.AdminViewModels;

namespace Timataka.Core.Services
{
    public class SportService : ISportService
    {
        private readonly ISportRepository _repo;

        public SportService(ISportRepository repo)
        {
            _repo = repo;
        }

        public SportService()
        {
            //To be able to create instance in unit tests
        }

        public async Task<Sport> Add(SportsViewModel model)
        {
            var sports = await _repo.GetSportByNameAsync(model.Name);
            var sport = new Sport { Name = model.Name };
            if (sports == null)
            {
                await _repo.InsertAsync(sport);
            }
            return sport;
        }

        public async Task<Sport> Edit(Sport s)
        {
            await _repo.EditAsync(s);
            return s;
        }

        public async Task<int> Remove(int id)
        {
            var s = await GetSportByIdAsync(id);
            await _repo.RemoveAsync(s);
            return id;
        }

        public IEnumerable<SportsViewModel> GetAllSports()
        {
            var sports = _repo.GetListOfSportsViewModels();
            return sports;
        }

        public async Task<Sport> GetSportByIdAsync(int id)
        {
            var s = await _repo.GetByIdAsync(id);
            return s;
        }

        public async Task<Sport> GetSportByName(string name)
        {
            var s = await _repo.GetSportByNameAsync(name);
            return s;
        }

    }
}
