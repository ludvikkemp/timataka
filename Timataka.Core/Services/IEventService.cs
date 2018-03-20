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
        Task<Event> AddAsync(EventViewModel e);
        Task<Event> GetEventByIdAsync(int eventId);
        Task<Event> EditAsync(Event e);
        Task<int> RemoveAsync(int EventId);
        IEnumerable<Event> GetAllEvents();
        IEnumerable<EventViewModel> GetEventsByCompetitionInstanceId(int id);
        Task<EventViewModel> GetEventViewModelByIdAsync(int eventId);
        Task<int> EditEventViewModelAsync(EventViewModel model);
    }
}
