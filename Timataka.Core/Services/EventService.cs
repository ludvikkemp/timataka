using System;
using System.Collections.Generic;
using System.Text;
using Timataka.Core.Data.Repositories;
using System.Threading.Tasks;
using Timataka.Core.Models.Entities;

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
        public async Task<Event> Add(Event e)
        {
            if (GetEventByName(e.Name) != null)
            {
                throw new Exception("Sport already exists");
            }
            await _repo.InsertAsync(e);
            return e;
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
        public async Task<int> Remove(int eventId)
        {
            var e = await GetEventById(eventId);
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
    }
}
