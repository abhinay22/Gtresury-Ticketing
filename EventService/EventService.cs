using AutoMapper;
using EventService.DTO;
using EventTicketing.Core;
using EventTicketing.Core.Entities;

namespace EventService
{
    public class EventService :IEventService
    {
        public readonly IEventRepository _repo;
        private readonly IMapper _mapper;
        public EventService(IEventRepository repo, IMapper map)
        {
            _repo = repo;
            _mapper = map;
        }

        public bool CancelEvent(int eventId)
        {
            throw new NotImplementedException();
        }

        public async Task<int> CreateEvent(CreateEventDTO eventData)
        {
            Event obj = _mapper.Map<CreateEventDTO, Event>(eventData);
            int id= await _repo.AddEvent(obj);
            return id;
           
        }

        public bool UpdateEventDetails(int eventId, EventDTO eventData)
        {
            throw new NotImplementedException();
        }

        public async Task<List<EventDTO>> ViewAllEvents()
        {
            List<Event> events= await _repo.GetAllEvents();
            if (events == null)
            {
                return null;
            }
            else
            {
                return _mapper.Map<List<Event>,List<EventDTO>>(events);
            }
        }

        public async  Task<EventDTO> ViewEvent(int eventId)
        {
          Event ent= await _repo.GetEvent(eventId);
            if (ent == null)
            {
                return null;
            }
            else
            {
                return _mapper.Map<Event, EventDTO>(ent);
            }
        }
    }
}
