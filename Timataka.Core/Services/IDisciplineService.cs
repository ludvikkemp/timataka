using System.Collections.Generic;
using System.Threading.Tasks;
using Timataka.Core.Models.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using Timataka.Core.Models.ViewModels.DisciplineViewModels;

namespace Timataka.Core.Services
{
    public interface IDisciplineService
    {
        Task<Discipline> AddAsync(Discipline s);
        Task<Discipline> EditAsync(Discipline s);
        int Remove(int DisciplineId);
        IEnumerable<Discipline> GetAllDisciplines();
        Discipline GetDisciplineById(int DisciplineId);
        List<SelectListItem> GetSportsListItems();
        int GetNextId();
        IEnumerable<DisciplineViewModel> GetDisciplinesBySportId(int  id);
    }
}