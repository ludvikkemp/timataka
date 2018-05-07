using System;
using System.Collections.Generic;
using System.Text;
using Timataka.Core.Models.Entities;
using System.Threading.Tasks;
using Timataka.Core.Models.Dto.CompetitionInstanceDTO;
using Timataka.Core.Models.ViewModels.EventViewModels;

namespace Timataka.Core.Services
{
    public interface IEventService
    {
        // *** ADD, EDIT & REMOVE *** ///
        Task<Event> AddAsync(EventViewModel e);
        Task<Event> EditAsync(Event e);
        Task<int> EditEventViewModelAsync(EventViewModel model);
        Task<int> RemoveAsync(int eventId);

        // *** GET *** //
        Task<Event> GetEventByIdAsync(int eventId);
        Event GetEventById(int eventId);
        IEnumerable<Event> GetAllEvents();
        IEnumerable<EventViewModel> GetEventsByCompetitionInstanceId(int id);
        Task<EventViewModel> GetEventViewModelByIdAsync(int eventId);
        IEnumerable<EventDropDownListViewModel> GetEventDropDownList();
        IEnumerable<EventHeatViewModel> GetEventHeatListForCompetitionInstance(int id);
        IEnumerable<Event> GetEventsByCompetitionInstanceIdAndUserId(int competitionInstanceId, string userId);
        IEnumerable<Event> GetNonDeletedEventsByCompetitionInstanceId(int competitionInstanceId);
    }
}
