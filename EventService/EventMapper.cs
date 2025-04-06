using AutoMapper;
using EventContract;
using EventService.DTO;
using EventTicketing.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventService
{
    public class EventMapper :Profile
    {
        public EventMapper()
        {
            CreateMap<CreateEventDTO, Event>().ForMember(x=>x.EventId,y=>y.Ignore());
            CreateMap<VenueDTO, EventTicketing.Core.Entities.Venue>().ForMember(x => x.VenueId, y => y.Ignore());
            CreateMap<PricingTierDTO, EventTicketing.Core.Entities.PricingTier>();
            CreateMap<EventTicketing.Core.Entities.PricingTier, PricingTierDTO>();
            CreateMap<Event, EventDTO>();
            CreateMap<EventDTO, Event>();
            CreateMap<EventTicketing.Core.Entities.Venue, VenueDTO>();
            CreateMap<VenueDTO, EventContract.Venue>(); ;
            CreateMap<PricingTierDTO, EventContract.PricingTier>();
            CreateMap<EventDTO, EventActivated>();
            
            // CreateMap<List<Event>, List<EventDTO>>();
        }
    }
}
