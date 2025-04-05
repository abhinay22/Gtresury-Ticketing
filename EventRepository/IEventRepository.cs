using EventTicketing.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventTicketing.Core
{
    public interface IEventRepository
    {
       public Task<int> AddEvent(Event eventData);

        void DeleteEvent(int eventId);

        Task<Event> UpdateEvent(int eventId, Event eventData);

        Task<List<Event>> GetAllEvents();

        Task<Event> GetEvent(int eventId);




    }
}
