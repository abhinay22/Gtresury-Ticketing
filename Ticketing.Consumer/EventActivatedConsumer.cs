using AutoMapper;
using EventContract;
using MassTransit;
using StackExchange.Redis;
using System.Threading.Tasks;
using Ticketing.Core;
using Ticketing.Core.Entities;

namespace Ticketing.Consumer
{
    public class EventActivatedConsumer : IConsumer<EventActivated>
    {
        private readonly ITicketingRepository _repo;
        private readonly IConnectionMultiplexer _redisMulti;

        //  private readonly IMapper _mapper;

        public EventActivatedConsumer(ITicketingRepository repo, IConnectionMultiplexer redis)
        {
            _repo = repo;
            _redisMulti = redis;
        }

        public async Task Consume(ConsumeContext<EventActivated> context)
        {
            EventActivated eventMetadata = context.Message;
            List<EventTicketInventory> tkts = new();
            List<Task> stocks = new();
            var db = _redisMulti.GetDatabase();
            if (eventMetadata != null)
            {
                foreach (var item in eventMetadata.pricingTier)
                {

                    EventTicketInventory entity = new EventTicketInventory()
                    {
                        eventId = eventMetadata.eventId,
                        eventDescription = eventMetadata.eventDescription,
                        eventName = eventMetadata.eventName,
                        ticketTier = item.tierName,
                        pricePerTicket = item.price,
                        total = item.totalTicket,
                        available = item.totalTicket
                    };
                    tkts.Add(entity);



                }

                await _repo.CreateEventTicketRepository(tkts);
             
                foreach (var item in eventMetadata.pricingTier)
                {
                    var redisKey = $"event:{eventMetadata.eventId}:tier:{item.tierName}:stock";
                    stocks.Add(db.StringSetAsync(redisKey, item.totalTicket));
                }
                await Task.WhenAll(stocks);
            }
           



          









        }
    }
}
