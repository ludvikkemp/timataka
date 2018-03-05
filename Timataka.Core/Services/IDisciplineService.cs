using System.Collections.Generic;
using System.Threading.Tasks;
using Timataka.Core.Models.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Timataka.Core.Services
{
    public interface IDisciplineService
    {
        Task<Discipline> Add(Discipline s);
        Task<Discipline> Edit(Discipline s);
        int Remove(int DisciplineId);
        IEnumerable<Discipline> GetAllDisciplines();
        Discipline GetDisciplineById(int DisciplineId);
        List<SelectListItem> GetSportsListItems();
        int GetNextId();
    }
}