using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Timataka.Core.Models.Entities;

namespace Timataka.Core.Data.Repositories
{
    public interface ISportsRepository : IDisposable
    {
        void Insert(Sport entity);
        Task InsertAsync(Sport entity);

        IEnumerable<Sport> Get();

        Sport GetById(int id);
        Task<Sport> GetByIdAsync(int id);

        void Edit(Sport entity);
        Task EditAsync(Sport entity);

        void Remove(Sport entity);
        Task RemoveAsync(Task<Sport> entity);

    }
}
