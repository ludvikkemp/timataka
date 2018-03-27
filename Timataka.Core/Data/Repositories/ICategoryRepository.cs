using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Timataka.Core.Models.Entities;

namespace Timataka.Core.Data.Repositories
{
    public interface ICategoryRepository
    {
        void Insert(Category c);
        Task InsertAsync(Category c);
        IEnumerable<Category> Get();
        Category GetById(int id);
        Task<Category> GetByIdAsync(int id);
        void Edit(Category c);
        Task EditAsync(Category c);
        void Remove(Club c);
        Task RemoveAsync(Club c);
        Task<Club> GetClubByNameAsync(string cName);
    }
}
