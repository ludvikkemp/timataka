using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Timataka.Core.Models.Entities;

namespace Timataka.Core.Data.Repositories
{
    public interface IDiciplinesRepository : IDisposable
    {
        void Insert(Dicipline entity);
        Task InsertAsync(Dicipline entity);

        IEnumerable<Dicipline> Get();

        Dicipline GetById(int id);
        Task<Dicipline> GetByIdAsync(int id);

    }
}
