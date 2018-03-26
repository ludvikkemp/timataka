using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Timataka.Core.Models.Entities;

namespace Timataka.Core.Data.Repositories
{
    public interface IDeviceRepository : IDisposable
    {
        Boolean Add(Device d);
        Task<Boolean> AddAsync(Device d);

        IEnumerable<Device> Get();

        Device GetById(int id);
        Task<Device> GetByIdAsync(int id);

        Boolean Edit(Device d);
        Task<Boolean> EditAsync(Device d);

        Boolean Remove(Device d);
        Task<Boolean> RemoveAsync(Device d);
    }
}
