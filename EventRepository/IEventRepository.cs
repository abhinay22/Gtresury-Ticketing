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

        void UpdateEvent(int eventId, Event eventData);

        List<Event> GetAllEvents();

        Event GetEvent(int eventId);




    }
}
