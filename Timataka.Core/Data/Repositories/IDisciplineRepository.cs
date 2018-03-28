using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Timataka.Core.Models.Entities;
using Timataka.Core.Models.ViewModels.DisciplineViewModels;

namespace Timataka.Core.Data.Repositories
{
    public interface IDisciplineRepository : IDisposable
    {
        void Insert(Discipline d);
        Task InsertAsync(Discipline d);

        IEnumerable<Discipline> Get();

        Discipline GetById(int id);
        Task<Discipline> GetByIdAsync(int id);

        void Edit(Discipline d);
        Task EditAsync(Discipline d);

        void Remove(Discipline d);
        Task RemoveAsync(Discipline d);

        List<Sport> GetSports();
        IEnumerable<DisciplineViewModel> GetDisciplinesBySportId(int id);
    }
}
