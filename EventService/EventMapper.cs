using AutoMapper;
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
            CreateMap<VenueDTO, Venue>().ForMember(x => x.VenueId, y => y.Ignore());
            CreateMap<PricingTierDTO,PricingTier>().ForMember(x=>x.PricingTierId,y=>y.Ignore());
            CreateMap<PricingTier, PricingTierDTO>();
            CreateMap<Event, EventDTO>();
            CreateMap<Venue, VenueDTO>();
            // CreateMap<List<Event>, List<EventDTO>>();
        }
    }
}
