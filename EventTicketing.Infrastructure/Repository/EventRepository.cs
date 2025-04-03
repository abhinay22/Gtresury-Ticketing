using EventTicketing.Core;
using EventTicketing.Core.Entities;
using EventTicketing.Infrastructure.DBContext;
using Microsoft.EntityFrameworkCore;

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

        public async void  DeleteEvent(int eventId)
        {  
            var  ent = await _dbContext.Events.FirstOrDefaultAsync(e => e.EventId == eventId);
            if (ent != null)
            {
                _dbContext.Remove(ent);
            }
           
            await _dbContext.SaveChangesAsync();
           
        }

        public List<Event> GetAllEvents()
        {
                throw new NotImplementedException();
        }

        public Event GetEvent(int eventId)
        {
            throw new NotImplementedException();
        }

        public void UpdateEvent(int eventId, Event eventData)
        {
            throw new NotImplementedException();
        }
    }
}
