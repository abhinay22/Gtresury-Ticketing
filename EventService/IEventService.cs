using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Linq;
using System.Text;
using EventService.DTO;

namespace EventService
{
    public interface IEventService
    {
        public bool CreateEvent(CreateEventDTO eventData);

        public bool CancelEvent(Guid eventId);

        public bool UpdateEventDetails(Guid eventId,EventDTO eventData);


        public List<EventDTO> ViewAllEvents();

        public EventDTO ViewEvent(Guid eventId);
    }
}
