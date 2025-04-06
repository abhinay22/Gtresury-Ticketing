using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketingService.DTOs;
using Ticketing.Core.Entities;

namespace TicketingService
{
    public class TicketMapper : Profile
    {
        public TicketMapper()
        {
            CreateMap<EventTicketInventory, TicketDTO>().ForMember(x => x.EventTicketInventoryId, y => y.Ignore());

            CreateMap<TicketTierReservation, BookingTicketTier>()
             .ForMember(dest => dest.ReservedQuantity, opt => opt.MapFrom(src => src.ReservedQuantity))
            .ForMember(dest => dest.TierName, opt => opt.MapFrom(src => src.TierName))
            .ForMember(dest => dest.TotalPrice, opt => opt.Ignore());

            CreateMap<BookTicketDTO, Booking>()
             .ForMember(dest => dest.status, opt => opt.MapFrom(src => src.status.ToString()));

            CreateMap<Booking, BookTicketDTO>()
             .ForMember(dest => dest.status, opt => opt.MapFrom(src => src.status.ToString()));

            CreateMap<BookingTicketTier, TicketTierReservation>()
         .ForMember(dest => dest.ReservedQuantity, opt => opt.MapFrom(src => src.ReservedQuantity))
        .ForMember(dest => dest.TierName, opt => opt.MapFrom(src => src.TierName));


            // Map from DTO to EF BookingTicketTier

        }
    }
}
