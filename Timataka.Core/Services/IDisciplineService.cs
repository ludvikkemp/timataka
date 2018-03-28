using System.Collections.Generic;
using System.Threading.Tasks;
using Timataka.Core.Models.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using Timataka.Core.Models.ViewModels.DisciplineViewModels;

namespace Timataka.Core.Services
{
    public interface IDisciplineService
    {
        Task<Discipline> AddAsync(Discipline d);
        Task<Discipline> EditAsync(Discipline d);
        Task<int> RemoveAsync(int id);
        IEnumerable<Discipline> GetAllDisciplines();
        Task<Discipline> GetDisciplineByIdAsync(int id);
        IEnumerable<DisciplineViewModel> GetDisciplinesBySportId(int id);
    }
}