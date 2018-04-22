using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Timataka.Core.Data.Repositories;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Storage;
using Timataka.Core.Models.Entities;
using Timataka.Core.Models.ViewModels.EventViewModels;

namespace Timataka.Core.Services
{
    public class EventService : IEventService
    {
        private readonly IEventRepository _repo;
        private readonly IHeatService _heatService;
        private readonly ICompetitionRepository _competitionRepo;

        public EventService(IEventRepository repo, IHeatService heatService, ICompetitionRepository competitionRepo)
        {
            _repo = repo;
            _heatService = heatService;
            _competitionRepo = competitionRepo;
        }

        public EventService()
        {
            //To be able to create instance in unit test.
        }

        /// <summary>
        /// Function to add a Event and create one Heat in that event.
        /// </summary>
        /// <param name="e"></param>
        /// <returns>ID of event added or exception if event exists</returns>
        public async Task<Event> AddAsync(EventViewModel e)
        {

            var entity = new Event
            {
                ActiveChip = e.ActiveChip,
                CompetitionInstanceId = e.CompetitionInstanceId,
                DateFrom = e.DateFrom,
                DateTo = e.DateTo,
                CourseId = e.CourseId,
                DisciplineId = e.DisciplineId,
                DistanceOffset = e.DistanceOffset,
                Gender = e.Gender,
                Id = e.Id,
                Laps = e.Laps,
                Name = e.Name,
                Splits = e.Splits,
                StartInterval = e.StartInterval,
                Deleted = false,
            };

            var newEvent = await _repo.InsertAsync(entity);
            
            //Create one heat
            await _heatService.AddAsync(newEvent.Id);

            return entity;
        }

        /// <summary>
        /// Get a event by its Name
        /// </summary>
        /// <param name="eventName"></param>
        /// <returns></returns>
        public async Task<Event> GetEventByNameAsync(string eventName)
        {
            var e = await _repo.GetEventByNameAsync(eventName);
            return e;
        }

        /// <summary>
        /// Get a event by its ID.
        /// </summary>
        /// <param name="eventId"></param>
        /// <returns>Sport with a given ID.</returns>
        public async Task<Event> GetEventByIdAsync(int eventId)
        {
            var e = await _repo.GetByIdAsync(eventId);
            return e;
        }

        /// <summary>
        /// Get a eventViewModel by its ID.
        /// </summary>
        /// <param name="eventId"></param>
        /// <returns>Sport with a given ID.</returns>
        public async Task<EventViewModel> GetEventViewModelByIdAsync(int eventId)
        {
            var e = await _repo.GetEventByIdAsync(eventId);
            return e;
        }

        public async Task<int> EditEventViewModelAsync(EventViewModel model)
        {
            var editEvent = await _repo.GetByIdAsync(model.Id);

            editEvent.Name = model.Name;
            editEvent.Id = model.Id;
            editEvent.DateFrom = model.DateFrom;
            editEvent.DateTo = model.DateTo;
            editEvent.ActiveChip = model.ActiveChip;
            editEvent.CourseId = model.CourseId;
            editEvent.Deleted = false;
            editEvent.DisciplineId = model.DisciplineId;
            editEvent.StartInterval = model.StartInterval;
            editEvent.Splits = model.Splits;
            editEvent.Laps = model.Laps;
            editEvent.Gender = model.Gender;
            editEvent.DistanceOffset = model.DistanceOffset;

            await _repo.EditAsync(editEvent);
            return editEvent.CompetitionInstanceId;
        }

        /// <summary>
        /// Function get a drop down list of Events
        /// which only holds Id and Name properties
        /// </summary>
        /// <returns>A List of Event Drop Down List View Model</returns>
        public IEnumerable<EventDropDownListViewModel> GetEventDropDownList()
        {
            return _repo.GetDropDownList();
        }

        /// <summary>
        /// Function to edit a event.
        /// </summary>
        /// <param name="e"></param>
        /// <returns>Id of the event edited</returns>
        public async Task<Event> EditAsync(Event e)
        {
            await _repo.EditAsync(e);
            return e;
        }

        /// <summary>
        /// Function to remove a given event.
        /// </summary>
        /// <param name="eventId"></param>
        /// <returns>Id of the event removed</returns>
        public async Task<int> RemoveAsync(int eventId)
        {
            var e = await GetEventByIdAsync(eventId);
            await _repo.RemoveAsync(e);
            return eventId;
        }

        /// <summary>
        /// Get list of all events.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Event> GetAllEvents()
        {
            var events = _repo.Get();
            return events;
        }

        /// <summary>
        /// Get list of events for Instance by id.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<EventViewModel> GetEventsByCompetitionInstanceId(int id)
        {
            var events = _repo.GetEventsForInstance(id);
            return events;
        }

        public Event GetEventById(int eventId)
        {
            return _repo.GetById(eventId);
        }

        /// <summary>
        /// Returns list of all heats in all events for competition instance
        /// </summary>
        /// <param name="id">Competition instance id</param>
        /// <returns></returns>
        public IEnumerable<EventHeatViewModel> GetEventHeatListForCompetitionInstance(int id)
        {
            var events = GetEventsByCompetitionInstanceId(id);
            var result = (from e in events
                          join h in _heatService.GetAllHeats() on e.Id equals h.EventId
                          select new EventHeatViewModel
                          {
                              EventId = e.Id,
                              EventName = e.Name,
                              HeatId = h.Id,
                              HeatNumber = h.HeatNumber
                          }).ToList();
            return result;
        }

        public IEnumerable<Event> GetEventsByCompetitionInstanceIdAndUserId(int competitionInstanceId, string userId)
        {
            return _repo.GetEventByInstanceAndContestantId(competitionInstanceId, userId);
        }
    }
}
