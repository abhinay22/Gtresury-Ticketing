using Microsoft.AspNetCore.Mvc;
using TicketingService.DTOs;
using Ticketing.Core;

namespace Tickets.api.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class TicketingController : ControllerBase
    {
        private readonly ITicketingService _svc;
        private readonly ILogger<TicketingController> _logger;

        public TicketingController(ITicketingService svc, ILogger<TicketingController> logger)
        {
            _svc = svc;
            _logger = logger;
        }

        // Get available ticket details
        [HttpGet("GetAvailableTicketDetails/{id}")]
        public async Task<IActionResult> GetAvailableTicketDetails([FromRoute] int id)
        {
            _logger.LogInformation("Started GetAvailableTicketDetails for Event ID: {EventId}", id);

            // Calls the service layer to get ticket details
            List<TicketDTO> dto = await _svc.GetAllTickets(id);

            if (dto == null || dto.Count == 0)
            {
                _logger.LogWarning("No tickets available for Event ID: {EventId}", id);
                return BadRequest("No tickets available for the given event.");
            }

            _logger.LogInformation("Successfully retrieved {TicketCount} tickets for Event ID: {EventId}", dto.Count, id);
            return Ok(dto);
        }

        // Reserve tickets
        [HttpPost("BookTicket")]
        public async Task<IActionResult> ReserveTicket([FromBody] BookTicketDTO dTO)
        {
            _logger.LogInformation("Started ReserveTicket for User: {UserEmail}, Event ID: {EventId}", dTO.userEmail, dTO.eventId);

            bool result = await _svc.ReserveTicketsForUser(dTO);
            int reservationId = await _svc.CreateReservation(dTO);

            if (result)
            {
                _logger.LogInformation("Successfully reserved tickets for User: {UserEmail}, Reservation ID: {ReservationId}", dTO.userEmail, reservationId);
                return Ok($"Booking with reference Id {reservationId} reserved for the next 5 minutes.");
            }
            else
            {
                _logger.LogWarning("Failed to reserve tickets for User: {UserEmail}, Event ID: {EventId}", dTO.userEmail, dTO.eventId);
                return BadRequest("Failed to reserve tickets.");
            }
        }

        // Simulate payment gateway approach with the booking id
        [HttpPost("MakePayment/{id}")]
        public async Task<IActionResult> MakePayment([FromRoute] int id, PaymentDTO dto)
        {
            _logger.LogInformation("Started MakePayment for Booking ID: {BookingId}", id);

            var bkdto = await _svc.GetBooking(id);

            if (bkdto == null)
            {
                var problemDetails = new ProblemDetails
                {
                    Status = StatusCodes.Status404NotFound,
                    Title = "Booking Not Found",
                    Detail = "The booking for specified reservation not found.",
                    Instance = HttpContext.Request.Path
                };

                _logger.LogWarning("Booking not found for Booking ID: {BookingId}", id);
                return NotFound(problemDetails);
            }

            if (dto.PaymentStatus?.ToUpper() == "CONFIRMED")
            {
                bool confirmationStatus = await _svc.ConfirmTicket(bkdto);
                if (confirmationStatus)
                {
                    _logger.LogInformation("Payment confirmed successfully for Booking ID: {BookingId}", id);
                    return Ok("Ticket created successfully.");
                }
                else
                {
                    _logger.LogWarning("Payment confirmation failed for Booking ID: {BookingId}", id);
                    return BadRequest("Confirmation failed.");
                }
            }

            // Cancel booking if payment is failed
            await _svc.CancelBooking(bkdto);
            _logger.LogWarning("Payment failed for Booking ID: {BookingId}, Cancelling booking.", id);
            return BadRequest("Booking cancelled as payment failed.");
        }
    }
}
