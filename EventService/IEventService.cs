﻿using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Linq;
using System.Text;
using EventService.DTO;

namespace EventService
{
    public interface IEventService
    {
        public Task<int> CreateEvent(CreateEventDTO eventData);

        public bool CancelEvent(int eventId);

        public Task<EventDTO> UpdateEventDetails(int eventId,EventDTO eventData);


        public Task<List<EventDTO>> ViewAllEvents();

        public Task<EventDTO> ViewEvent(int eventId);

        public Task PublishEventToBroker(EventDTO dto);
    }
}
