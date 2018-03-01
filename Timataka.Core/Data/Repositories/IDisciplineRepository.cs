using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Timataka.Core.Models.Entities;

namespace Timataka.Core.Data.Repositories
{
    public interface IDisciplineRepository : IDisposable
    {
        void Insert(Discipline entity);
        Task InsertAsync(Discipline entity);

        IEnumerable<Discipline> Get();

        Discipline GetById(int id);
        Task<Discipline> GetByIdAsync(int id);

        void Edit(Discipline entity);
        Task EditAsync(Discipline entity);

        void Remove(Discipline entity);
        Task RemoveAsync(Task<Discipline> entity);

        List<Sport> GetSports();
    }
}
