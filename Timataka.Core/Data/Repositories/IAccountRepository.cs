using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Timataka.Core.Models.Entities;

namespace Timataka.Core.Data.Repositories
{
    public interface IAccountRepository : IDisposable
    {
        List<Country> GetNations();
        Country GetCountryById(int id);
    }
}
