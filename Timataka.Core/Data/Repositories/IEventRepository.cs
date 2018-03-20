using System;
using System.Collections.Generic;
using System.Text;
using Timataka.Core.Models.Entities;
using System.Threading.Tasks;
using Timataka.Core.Models.ViewModels.EventViewModels;

namespace Timataka.Core.Data.Repositories
{
    public interface IEventRepository : IDisposable
    {
        Task<Event> InsertAsync(Event entity);
        Task<Event> GetEventByNameAsync(string eventName);
        Task<Event> GetByIdAsync(int id);
        void Edit(Event entity);
        Task EditAsync(Event entity);
        void Remove(Event entity);
        Task RemoveAsync(Event entity);
        IEnumerable<Event> Get();
        IEnumerable<EventViewModel> GetEventsForInstance(int Id);
        Task<EventViewModel> GetEventByIdAsync(int id);
    }
}
