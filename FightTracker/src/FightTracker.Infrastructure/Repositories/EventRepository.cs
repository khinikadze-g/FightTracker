using FightTracker.Core.Entities;
using FightTracker.Core.Interfaces;
using FightTracker.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FightTracker.Infrastructure.Repositories
{
    public class EventRepository : IEventRepository
    {
        private readonly ApplicationDbContext dbContext;

        public EventRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<Event> AddEventAsync(Event Event)
        {
            await dbContext.Events.AddAsync(Event);
            await dbContext.SaveChangesAsync();
            return Event;
        }

        public async Task<Event?> DeleteEventAsync(int Id)
        {
            var existingEvent = await dbContext.Events.FirstOrDefaultAsync(e => e.Id == Id);
            if (existingEvent == null)
            {
                return null;
            }
            dbContext.Events.Remove(existingEvent);
            await dbContext.SaveChangesAsync();
            return existingEvent;
        }

        public async Task<Event?> GetEventByIdAsync(int Id)
        {
            return await dbContext.Events.FirstOrDefaultAsync(e => e.Id == Id);

        }

        public async Task<List<Event>> GetEventsAsync()
        {
            return await dbContext.Events.ToListAsync();
        }

        public async Task<Event?> UpdateEventAsync(int Id, Event Event)
        {
            var existingEvent = dbContext.Events.FirstOrDefault(e => e.Id == Id);
            if (existingEvent == null)
            {
                return null;
            }
            existingEvent.Name = Event.Name;
            existingEvent.Date = Event.Date;
            existingEvent.Location = Event.Location;
            existingEvent.Fights = Event.Fights;
            await dbContext.SaveChangesAsync();
            return existingEvent;
        }
    }
}
