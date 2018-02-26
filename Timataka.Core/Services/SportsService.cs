using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Timataka.Core.Data.Repositories;
using Timataka.Core.Models.Entities;

namespace Timataka.Core.Services
{
    public class SportsService : ISportsService
    {
        private readonly ISportsRepository _repo;

        public SportsService(ISportsRepository repo)
        {
            _repo = repo;
        }

        public SportsService()
        {
            //To be able to create instance in unit tests
        }

        /// <summary>
        /// Function to add a sport.
        /// </summary>
        /// <param name="s"></param>
        /// <returns>ID of sport added</returns>
        public async Task<Sport> Add(Sport s)
        {
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
        /// <param name="SportId"></param>
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
        IEnumerable<Sport> ISportsService.GetAllSports()
        {
            var Sports = _repo.Get();
            return Sports;
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

    }
}
