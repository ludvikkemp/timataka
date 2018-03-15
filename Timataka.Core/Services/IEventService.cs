using System;
using System.Collections.Generic;
using System.Text;
using Timataka.Core.Models.Entities;
using System.Threading.Tasks;

namespace Timataka.Core.Services
{
    public interface IEventService
    {
        Task<Event> Add(Event e);
        Task<Event> GetEventById(int EventId);
        Task<Event> Edit(Event e);
        Task<int> Remove(int EventId);
    }
}
