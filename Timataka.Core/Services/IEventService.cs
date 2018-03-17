using System;
using System.Collections.Generic;
using System.Text;
using Timataka.Core.Models.Entities;
using System.Threading.Tasks;
using Timataka.Core.Models.ViewModels.EventViewModels;

namespace Timataka.Core.Services
{
    public interface IEventService
    {
        Task<Event> Add(EventViewModel e);
        Task<Event> GetEventById(int EventId);
        Task<Event> Edit(Event e);
        Task<int> Remove(int EventId);
        IEnumerable<Event> GetAllEvents();
        IEnumerable<Event> GetAllEventsOfInstance(int id);
    }
}
