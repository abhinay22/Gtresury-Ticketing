using Microsoft.AspNetCore.Mvc;
using EventService;
using EventService.DTO;
using Azure;
using Microsoft.AspNetCore.JsonPatch;
using MassTransit;

namespace Event.api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]  // Explicitly declare as ApiController for automatic model validation
    public class EventController : ControllerBase
    {
        private readonly IEventService _svc;
        private readonly ILogger<EventController> _logger;

        public EventController(IEventService repo, ILogger<EventController> logger)
        {
            _svc = repo;
            _logger = logger;
        }

        [HttpGet("GetEvent")]
        public async Task<IActionResult> GetEvent([FromQuery] int EventId)
        {
            try
            {
                _logger.LogInformation("Getting event details for EventId: {EventId}", EventId);

                EventDTO dto = await _svc.ViewEvent(EventId);
                if (dto != null)
                {
                    _logger.LogInformation("Event found for EventId: {EventId}", EventId);
                    return Ok(dto);
                }
                else
                {
                    _logger.LogWarning("Event not found for EventId: {EventId}", EventId);
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving event for EventId: {EventId}", EventId);
                return StatusCode(500, new ProblemDetails
                {
                    Status = 500,
                    Title = "Internal Server Error",
                    Detail = "An error occurred while processing your request.",
                    Instance = HttpContext.Request.Path
                });
            }
        }

        [HttpGet("GetAllEvents")]
        public async Task<IActionResult> GetEvents()
        {
            try
            {
                _logger.LogInformation("Fetching all events.");

                List<EventDTO> dtos = await _svc.ViewAllEvents();
                if (dtos != null && dtos.Count > 0)
                {
                    _logger.LogInformation("Successfully retrieved {EventCount} events.", dtos.Count);
                    return Ok(dtos);
                }
                else
                {
                    _logger.LogWarning("No events found.");
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving all events.");
                return StatusCode(500, new ProblemDetails
                {
                    Status = 500,
                    Title = "Internal Server Error",
                    Detail = "An error occurred while processing your request.",
                    Instance = HttpContext.Request.Path
                });
            }
        }

        [HttpPut("changeEventData/{id}")]
        public async Task<IActionResult> ChangeEventData([FromRoute] int id, [FromBody] EventDTO dto)
        {
            try
            {
                _logger.LogInformation("Attempting to update event data for EventId: {EventId}", id);

                var eventdto = await _svc.ViewEvent(id);
                if (eventdto == null)
                {
                    _logger.LogWarning("Event not found for EventId: {EventId}", id);
                    return NotFound();
                }

                EventDTO evDto = await _svc.UpdateEventDetails(id, dto);
                _logger.LogInformation("Event updated successfully for EventId: {EventId}", id);
                return Ok(evDto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while updating event data for EventId: {EventId}", id);
                return StatusCode(500, new ProblemDetails
                {
                    Status = 500,
                    Title = "Internal Server Error",
                    Detail = "An error occurred while processing your request.",
                    Instance = HttpContext.Request.Path
                });
            }
        }

        [HttpPost("CreateEvent")]
        public async Task<IActionResult> CreateEvent([FromBody] CreateEventDTO dto)
        {
            try
            {
                _logger.LogInformation("Creating new event with provided data.");

                int eventId = await _svc.CreateEvent(dto);
                _logger.LogInformation("Event created successfully with EventId: {EventId}", eventId);

                return CreatedAtAction("GetEvent", new { EventId = eventId }, dto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while creating event.");
                return StatusCode(500, new ProblemDetails
                {
                    Status = 500,
                    Title = "Internal Server Error",
                    Detail = "An error occurred while processing your request.",
                    Instance = HttpContext.Request.Path
                });
            }
        }

        [HttpPatch("activateEvent/{id}")]
        public async Task<IActionResult> ActivateEvent([FromRoute] int id, [FromBody] JsonPatchDocument<EventDTO> doc)
        {
            try
            {
                _logger.LogInformation("Attempting to activate event with EventId: {EventId}", id);

                EventDTO eventdto = await _svc.ViewEvent(id);
                if (eventdto == null)
                {
                    _logger.LogWarning("Event not found for EventId: {EventId}", id);
                    return NotFound();
                }

                doc.ApplyTo(eventdto, ModelState);
                if (!ModelState.IsValid)
                {
                    _logger.LogWarning("Model state is invalid for EventId: {EventId}", id);
                    return BadRequest(ModelState);
                }

                EventDTO evDto = await _svc.UpdateEventDetails(id, eventdto);
                if (evDto != null)
                {
                    await _svc.PublishEventToBroker(evDto);
                    _logger.LogInformation("Event activated and published for EventId: {EventId}", id);
                }

                return Ok(evDto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while activating event with EventId: {EventId}", id);
                return StatusCode(500, new ProblemDetails
                {
                    Status = 500,
                    Title = "Internal Server Error",
                    Detail = "An error occurred while processing your request.",
                    Instance = HttpContext.Request.Path
                });
            }
        }
    }
}
