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
        public Task<int> Edit(Sport s)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Function to remove a given sport.
        /// </summary>
        /// <param name="SportId"></param>
        /// <returns>Id of the sport removed</returns>
        public Task<int> Remove(int SportId)
        {
            throw new NotImplementedException();
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
            Sport s = await _repo.GetByIdAsync(SportId);
            return s;
        }


    }
}
