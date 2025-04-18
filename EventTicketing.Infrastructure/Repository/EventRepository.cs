﻿using EventTicketing.Core;
using EventTicketing.Core.Entities;
using EventTicketing.Infrastructure.DBContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace EventRepository
{
    public class EventRepository : IEventRepository
    {
        private readonly EventDBContext _dbContext;
        public EventRepository(EventDBContext context)
        {
            _dbContext = context;
        }
        public async Task<int> AddEvent(Event eventData)
        {
            _dbContext.Events.Add(eventData);
            await _dbContext.SaveChangesAsync();
            return eventData.EventId;
        }

        public async void DeleteEvent(int eventId)
        {
            var ent = await _dbContext.Events.FirstOrDefaultAsync(e => e.EventId == eventId);
            if (ent != null)
            {
                _dbContext.Remove(ent);
            }

            await _dbContext.SaveChangesAsync();

        }

        public async Task<List<Event>> GetAllEvents()
        {
            return await _dbContext.Events.
            Include(x => x.pricingTier).
             Include(x => x.venue).
            ToListAsync();
        }

        public async Task<Event> GetEvent(int eventId)
        {
            return await _dbContext.Events.AsNoTracking().
                Include(x => x.pricingTier).AsNoTracking().
                 Include(x => x.venue).AsNoTracking().
                FirstOrDefaultAsync(x => x.EventId == eventId);
        }

        public async Task<Event> UpdateEvent(int eventId, Event eventData)
        {
            _dbContext.Events.Update(eventData);
            await _dbContext.SaveChangesAsync();
            var updatedEvent = _dbContext.Entry(eventData).Entity;
            return updatedEvent;
        }
    }
}
