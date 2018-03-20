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
        /// <param name="s"></param>
        /// <returns>ID of discipline added</returns>
        public async Task<Discipline> Add(Discipline d)
        {
            await _repo.InsertAsync(d);
            return d;
        }

        /// <summary>
        /// Function to edit a discipline.
        /// </summary>
        /// <param name="s"></param>
        /// <returns>Id of the discipline edited</returns>
        public async Task<Discipline> Edit(Discipline d)
        {
            await _repo.EditAsync(d);
            return d;
        }

        /// <summary>
        /// Function to remove a given discipline.
        /// </summary>
        /// <param name="DisciplineId"></param>
        /// <returns>Id of the discipline removed</returns>
        public int Remove(int DisciplineId)
        {
            _repo.Remove(GetDisciplineById(DisciplineId));
            return DisciplineId;
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
        /// <param name="DisciplineId"></param>
        /// <returns>Discipline with a given ID.</returns>
        public Discipline GetDisciplineById(int DisciplineId)
        {
            var d = _repo.GetById(DisciplineId);
            return d;
        }

        public List<SelectListItem> GetSportsListItems()
        {
            List<SelectListItem> selectSportsListItems =
                new List<SelectListItem>();

            var listOfSports = _repo.GetSports();

            foreach (var item in listOfSports)
            {
                selectSportsListItems.Add(
                    new SelectListItem
                    {
                        Text = item.Name,
                        Value = item.Id.ToString()
                    });
            }

            return selectSportsListItems;
        }

        public int GetNextId()
        {
            int maxId = -1;
            var disciplines = _repo.Get();

            foreach(var discipline in disciplines)
            {
                if(discipline.Id > maxId)
                {
                    maxId = discipline.Id;
                }
            }

            return maxId + 1;
        }

        public IEnumerable<DisciplineViewModel> GetDisciplinesBySportId(int id)
        {
            var models = _repo.GetDisciplinesBySportId(id);
            return models;
        }
    }
}
