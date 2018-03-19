using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Timataka.Core.Models.Entities;
using Timataka.Core.Models.ViewModels.AdminViewModels;

namespace Timataka.Core.Data.Repositories
{
    public interface ISportRepository : IDisposable
    {
        void Insert(Sport entity);
        Task InsertAsync(Sport entity);

        IEnumerable<Sport> Get();
        IEnumerable<SportsViewModel> GetListOfSportsViewModels();

        Sport GetById(int id);
        Task<Sport> GetByIdAsync(int id);

        void Edit(Sport entity);
        Task EditAsync(Sport entity);

        void Remove(Sport entity);
        Task RemoveAsync(Sport entity);

        Task<Sport> GetSportByNameAsync(string sportName);
    }
}
