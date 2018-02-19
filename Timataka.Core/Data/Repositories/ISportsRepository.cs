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

    }
}
