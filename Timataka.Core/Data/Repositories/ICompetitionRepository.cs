using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Timataka.Core.Models.Entities;

namespace Timataka.Core.Data.Repositories
{
    public interface ICompetitionRepository : IDisposable
    {
        void Create(Competition c);
        Task InsertAsync(Competition c);

        IEnumerable<Competition> Get();

        Competition GetById(int Id);
        Task<Competition> GetByIdAsync(int Id);

        void Edit(Competition c);
        Task EditAsync(Competition c);

        void Remove(Competition c);
        Task RemoveAsync(Competition c);

        Task<Competition> GetCompetitionByNameAsync(string cName);
    }
}
