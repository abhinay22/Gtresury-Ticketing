using AutoMapper;
using EventContract;
using EventService.DTO;
using EventTicketing.Core;
using EventTicketing.Core.Entities;
using MassTransit;
using Event = EventTicketing.Core.Entities.Event;

namespace EventService
{
    public class EventService : IEventService
    {
        public readonly IEventRepository _repo;
        private readonly IMapper _mapper;
        private readonly IPublishEndpoint _endpoint;
        public EventService(IEventRepository repo, IMapper map, IPublishEndpoint endpoint)
        {
            _repo = repo;
            _mapper = map;
            _endpoint = endpoint;
        }

        public bool CancelEvent(int eventId)
        {
            throw new NotImplementedException();
        }

        public async Task<int> CreateEvent(CreateEventDTO eventData)
        {
            Event obj = _mapper.Map<CreateEventDTO, Event>(eventData);
            int id = await _repo.AddEvent(obj);
            return id;

        }

        public async Task PublishEventToBroker(EventDTO dto)
        {
            EventActivated activated = _mapper.Map<EventDTO, EventActivated>(dto);
            await _endpoint.Publish<EventActivated>(activated);
        }

        public async Task<EventDTO> UpdateEventDetails(int eventId, EventDTO eventData)
        {
            Event eventEnt = _mapper.Map<EventDTO, Event>(eventData);
            Event updated = await _repo.UpdateEvent(eventId, eventEnt);
            return _mapper.Map<Event, EventDTO>(updated);
        }

        public async Task<List<EventDTO>> ViewAllEvents()
        {
            List<Event> events = await _repo.GetAllEvents();
            if (events == null)
            {
                return null;
            }
            else
            {
                return _mapper.Map<List<Event>, List<EventDTO>>(events);
            }
        }

        public async Task<EventDTO> ViewEvent(int eventId)
        {
            Event ent = await _repo.GetEvent(eventId);
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
