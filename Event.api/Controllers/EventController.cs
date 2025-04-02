using Microsoft.AspNetCore.Mvc;
using EventService;
using EventService.DTO;

namespace Event.api.Controllers
{
    [Route("api/{contoller}")]
    public class EventController : Controller
    {
        private readonly IEventService _repo;

        private readonly ILogger<EventController> _logger;

        public EventController(IEventService repo, ILogger<EventController> logger)
        {
            _repo = repo;
            _logger = logger;
        }

        [HttpPost("CreateEvent")]
        public IActionResult CreateEvent([FromBody] CreateEventDTO dto)
        {
          bool result= _repo.CreateEvent(dto);

            return CreatedAtAction("",null);
        }
    }
}
