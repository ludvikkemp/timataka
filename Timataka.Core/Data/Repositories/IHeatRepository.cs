using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Timataka.Core.Models.Entities;

namespace Timataka.Core.Data.Repositories
{
    public interface IHeatRepository : IDisposable
    {
        void Insert(Heat h);
        Task InsertAsync(Heat h);

        IEnumerable<Heat> Get();

        Heat GetById(int id);
        Task<Heat> GetByIdAsync(int id);

        void Edit(Heat h);
        Task EditAsync(Heat h);

        void Remove(Heat h);
        Task RemoveAsync(Heat h);

    }
}
