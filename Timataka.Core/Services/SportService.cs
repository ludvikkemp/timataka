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

        /// <summary>
        /// Function to add a sport.
        /// </summary>
        /// <param name="model"></param>
        /// <returns>ID of sport added or exception if sport exists</returns>
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

        /// <summary>
        /// Function to edit a sport.
        /// </summary>
        /// <param name="s"></param>
        /// <returns>Id of the sport edited</returns>
        public async Task<Sport> Edit(Sport s)
        {
            await _repo.EditAsync(s);
            return s;
        }

        /// <summary>
        /// Function to remove a given sport.
        /// </summary>
        /// <param name="sportId"></param>
        /// <returns>Id of the sport removed</returns>
        public async Task<int> Remove(int sportId)
        {
            var s = await GetSportByIdAsync(sportId);
            await _repo.RemoveAsync(s);
            return sportId;
        }

        /// <summary>
        /// Get list of all sports.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<SportsViewModel> GetAllSports()
        {
            var sports = _repo.GetListOfSportsViewModels();
            return sports;
        }

        /// <summary>
        /// Get a sport by its ID.
        /// </summary>
        /// <param name="sportId"></param>
        /// <returns>Sport with a given ID.</returns>
        public async Task<Sport> GetSportByIdAsync(int sportId)
        {
            var s = await _repo.GetByIdAsync(sportId);
            return s;
        }

        /// <summary>
        /// Get a sport by its Name
        /// </summary>
        /// <param name="sportName"></param>
        /// <returns></returns>
        public async Task<Sport> GetSportByName(string sportName)
        {
            var s = await _repo.GetSportByNameAsync(sportName);
            return s;
        }

    }
}
