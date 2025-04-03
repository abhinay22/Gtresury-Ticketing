using EventService.DTO;
using EventTicketing.Core;

namespace EventService
{
    public class EventService :IEventService
    {
        public readonly IEventRepository _repo;
        public EventService(IEventRepository repo)
        {
            repo = _repo;
        }

        public bool CancelEvent(int eventId)
        {
            throw new NotImplementedException();
        }

        public bool CreateEvent(CreateEventDTO eventData)
        {
            throw new NotImplementedException();
        }

        public bool UpdateEventDetails(int eventId, EventDTO eventData)
        {
            throw new NotImplementedException();
        }

        public List<EventDTO> ViewAllEvents()
        {
            throw new NotImplementedException();
        }

        public EventDTO ViewEvent(int eventId)
        {
            throw new NotImplementedException();
        }
    }
}
