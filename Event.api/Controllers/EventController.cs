using Microsoft.AspNetCore.Mvc;
using EventService;
using EventService.DTO;

namespace Event.api.Controllers
{
    [Route("api/[controller]")]
    public class EventController : Controller
    {
        private readonly IEventService _repo;

        private readonly ILogger<EventController> _logger;

        public EventController(IEventService repo, ILogger<EventController> logger)
        {
            _repo = repo;
            _logger = logger;
        }

        [HttpGet("GetEvent")]
        public async Task<IActionResult> GetEvent([FromQuery] int EventId)
        {
           EventDTO dto= await _repo.ViewEvent(EventId);
            if (dto != null)
            {
                return Ok(dto);
            }
            else
            {
               return NotFound();
            }
        }
        [HttpGet("GetAllEvents")]
        public async Task<IActionResult> GetEvents()
        {
            List<EventDTO> dtos = await _repo.ViewAllEvents();
            if (dtos != null)
            {
                return Ok(dtos);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost("CreateEvent")]
        public async Task<IActionResult> CreateEvent([FromBody] CreateEventDTO dto)
        {
          int eventId= await _repo.CreateEvent(dto);

            return CreatedAtAction("GetEvent", new { EventId = eventId }, dto);
        }
    }
}
