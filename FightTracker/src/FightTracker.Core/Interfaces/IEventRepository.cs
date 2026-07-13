using FightTracker.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FightTracker.Core.Interfaces
{
    public interface IEventRepository
    {
        Task<List<Event>> GetEventsAsync();
        Task<Event?> GetEventByIdAsync(int Id);
        Task<Event> AddEventAsync(Event Event);
        Task<Event?> UpdateEventAsync(int Id, Event Event);
        Task<Event?> DeleteEventAsync(int Id);

    }
}
