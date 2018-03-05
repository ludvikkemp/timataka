using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Timataka.Core.Data.Repositories;
using Timataka.Core.Models.Entities;

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
        /// <param name="s"></param>
        /// <returns>ID of sport added or exception if sport exists</returns>
        public async Task<Sport> Add(Sport s)
        {
            if(GetSportByName(s.Name) != null)
            {
                throw new Exception("Sport already exists");
            }
            await _repo.InsertAsync(s);
            return s;
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
        public async Task<int> Remove(int SportId)
        {
            var s = await GetSportById(SportId);
            await _repo.RemoveAsync(s);
            return SportId;
        }

        /// <summary>
        /// Get list of all sports.
        /// </summary>
        /// <returns></returns>
        IEnumerable<Sport> ISportService.GetAllSports()
        {
            var sports = _repo.Get();
            return sports;
        }

        /// <summary>
        /// Get a sport by its ID.
        /// </summary>
        /// <param name="SportId"></param>
        /// <returns>Sport with a given ID.</returns>
        public async Task<Sport> GetSportById(int SportId)
        {
            var s = await _repo.GetByIdAsync(SportId);
            return s;
        }

        /// <summary>
        /// Get a sport by its Name
        /// </summary>
        /// <param name="SportName"></param>
        /// <returns></returns>
        public async Task<Sport> GetSportByName(string SportName)
        {
            var s = await _repo.GetSportByNameAsync(SportName);
            return s;
        }

    }
}
