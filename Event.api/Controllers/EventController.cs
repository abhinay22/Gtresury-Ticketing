using Microsoft.AspNetCore.Mvc;
using EventService;
using EventService.DTO;
using Azure;
using Microsoft.AspNetCore.JsonPatch;

namespace Event.api.Controllers
{
    [Route("api/v1/[controller]")]
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
            EventDTO dto = await _repo.ViewEvent(EventId);
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

        [HttpPut("changeEventData/{id}")]
        public async Task<IActionResult> ChangeEventData([FromRoute] int id, [FromBody] EventDTO dto)
        {
            var eventdto = await _repo.ViewEvent(id);
            if (eventdto == null)
            {
                return NotFound();
            }
            else
            {
                EventDTO evDto = await _repo.UpdateEventDetails(id, dto);
                return Ok(evDto);
            }
        }

        [HttpPost("CreateEvent")]
        public async Task<IActionResult> CreateEvent([FromBody] CreateEventDTO dto)
        {
            int eventId = await _repo.CreateEvent(dto);

            return CreatedAtAction("GetEvent", new { EventId = eventId }, dto);
        }

        [HttpPatch("activateEvent/{id}")]
        public async Task<IActionResult> ActivateEvent([FromRoute]int id,[FromBody]JsonPatchDocument<EventDTO> doc)
        {
           EventDTO eventdto= await _repo.ViewEvent(id);
            if (eventdto == null)
            {
                return NotFound();
            }
            else
            {
                doc.ApplyTo(eventdto,ModelState);
                if (!ModelState.IsValid)
                {
                    return BadRequest();
                }
                EventDTO evDto = await _repo.UpdateEventDetails(id, eventdto);
                return Ok(evDto);
            }

        }
    }
}
