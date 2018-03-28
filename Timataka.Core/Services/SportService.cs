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
        /// <param name="id"></param>
        /// <returns>Id of the sport removed</returns>
        public async Task<int> Remove(int id)
        {
            var s = await GetSportByIdAsync(id);
            await _repo.RemoveAsync(s);
            return id;
        }

        /// <summary>
        /// Get list of all sports.
        /// </summary>
        /// <returns>List of all sports</returns>
        public IEnumerable<SportsViewModel> GetAllSports()
        {
            var sports = _repo.GetListOfSportsViewModels();
            return sports;
        }

        /// <summary>
        /// Get a sport by its ID.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Sport with a given ID.</returns>
        public async Task<Sport> GetSportByIdAsync(int id)
        {
            var s = await _repo.GetByIdAsync(id);
            return s;
        }

        /// <summary>
        /// Get a sport by its Name
        /// </summary>
        /// <param name="name"></param>
        /// <returns>Sport with the given name.</returns>
        public async Task<Sport> GetSportByName(string name)
        {
            var s = await _repo.GetSportByNameAsync(name);
            return s;
        }

    }
}
