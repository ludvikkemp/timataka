using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Timataka.Core.Data.Repositories;
using Timataka.Core.Models.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using Timataka.Core.Models.ViewModels.DisciplineViewModels;

namespace Timataka.Core.Services
{
    public class DisciplineService : IDisciplineService
    {
        private readonly IDisciplineRepository _repo;

        public DisciplineService(IDisciplineRepository repo)
        {
            _repo = repo;
        }

        public DisciplineService()
        {
            //To be able to create instance in unit tests
        }

        /// <summary>
        /// Function to add a discipline.
        /// </summary>
        /// <param name="d"></param>
        /// <returns>ID of discipline added</returns>
        public async Task<Discipline> AddAsync(Discipline d)
        {
            await _repo.InsertAsync(d);
            return d;
        }

        /// <summary>
        /// Function to edit a discipline.
        /// </summary>
        /// <param name="d"></param>
        /// <returns>Id of the discipline edited</returns>
        public async Task<Discipline> EditAsync(Discipline d)
        {
            await _repo.EditAsync(d);
            return d;
        }

        /// <summary>
        /// Function to remove a given discipline.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Id of the discipline removed</returns>
        public async Task<int> RemoveAsync(int id)
        {
            await _repo.RemoveAsync(await GetDisciplineByIdAsync(id));
            return id;
        }

        /// <summary>
        /// Get list of all discipline.
        /// </summary>
        /// <returns></returns>
        IEnumerable<Discipline> IDisciplineService.GetAllDisciplines()
        {
            var disciplines = _repo.Get();
            return disciplines;
        }

        /// <summary>
        /// Get a discipline by its ID.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Discipline with a given ID.</returns>
        public async Task<Discipline> GetDisciplineByIdAsync(int id)
        {
            var d = await _repo.GetByIdAsync(id);
            return d;
        }

        /// <summary>
        /// Get all disciplines for a given sport
        /// </summary>
        /// <param name="id"></param>
        /// <returns>List of disciplines</returns>
        public IEnumerable<DisciplineViewModel> GetDisciplinesBySportId(int id)
        {
            var models = _repo.GetDisciplinesBySportId(id);
            return models;
        }
    }
}
