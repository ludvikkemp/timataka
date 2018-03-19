using System;
using System.Collections.Generic;
using System.Text;
using Timataka.Core.Data.Repositories;
using System.Threading.Tasks;
using Timataka.Core.Models.Entities;
using Timataka.Core.Models.ViewModels.EventViewModels;

namespace Timataka.Core.Services
{
    public class EventService : IEventService
    {
        private readonly IEventRepository _repo;

        public EventService(IEventRepository repo)
        {
            _repo = repo;
        }

        public EventService()
        {
            //To be able to create instance in unit test.
        }

        /// <summary>
        /// Function to add a Event.
        /// </summary>
        /// <param name="e"></param>
        /// <returns>ID of event added or exception if event exists</returns>
        public async Task<Event> Add(EventViewModel e)
        {
            /*
            if (GetEventByName(e.Name) != null)
            {
                throw new Exception("Event already exists");
            }
            */
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
                Deleted = false
            };

            await _repo.InsertAsync(entity);
            
            return entity;
        }

        /// <summary>
        /// Get a event by its Name
        /// </summary>
        /// <param name="EventName"></param>
        /// <returns></returns>
        public async Task<Event> GetEventByName(string EventName)
        {
            var e = await _repo.GetEventByNameAsync(EventName);
            return e;
        }

        /// <summary>
        /// Get a event by its ID.
        /// </summary>
        /// <param name="eventId"></param>
        /// <returns>Sport with a given ID.</returns>
        public async Task<Event> GetEventById(int eventId)
        {
            var e = await _repo.GetByIdAsync(eventId);
            return e;
        }

        /// <summary>
        /// Function to edit a event.
        /// </summary>
        /// <param name="e"></param>
        /// <returns>Id of the event edited</returns>
        public async Task<Event> Edit(Event e)
        {
            await _repo.EditAsync(e);
            return e;
        }

        /// <summary>
        /// Function to remove a given event.
        /// </summary>
        /// <param name="eventId"></param>
        /// <returns>Id of the event removed</returns>
        public async Task<int> Remove(int EventId)
        {
            var e = await GetEventById(EventId);
            await _repo.RemoveAsync(e);
            return EventId;
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

        public IEnumerable<Event> GetAllEventsOfInstance(int Id)
        {
            var events = _repo.GetEventsForInstance(Id);
            return events;
        }
    }
}
