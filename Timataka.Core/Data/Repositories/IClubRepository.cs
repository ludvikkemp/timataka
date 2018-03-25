using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Timataka.Core.Models.Entities;

namespace Timataka.Core.Data.Repositories
{
    public interface IClubRepository
    {
        void Insert(Club c);
        Task InsertAsync(Club c);
        IEnumerable<Club> Get();
        Club GetById(int id);
        Task<Club> GetByIdAsync(int id);
        void Edit(Club c);
        Task EditAsync(Club c);
        void Remove(Club c);
        Task RemoveAsync(Club c);
        Task<Club> GetClubByNameAsync(string cName);
        
    }
}
