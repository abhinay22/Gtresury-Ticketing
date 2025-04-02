namespace Event.api.UnitTest;

using Event.api.Controllers;
using EventService;
using EventService.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;


public class EventContollerUnitTest
{
    private  Mock<IEventService> svc;

    private EventController _contoller;

    private Mock<ILogger<EventController>> logger;

    public EventContollerUnitTest()
    {
       svc = new Mock<IEventService>();

        logger = new Mock<ILogger<EventController>>();

        _contoller = new EventController(svc.Object,logger.Object);

       

    }

    [Fact]
    // checks if the service layer is called from CreateActionContrtoller
    public void CreateEventController_Calls_ServicelayerMethod()
    {
        svc.Setup(y => y.CreateEvent(It.IsAny<CreateEventDTO>())).Returns(true);

        CreatedAtActionResult result=(CreatedAtActionResult)_contoller.CreateEvent(null);
    

        svc.Verify(x=>x.CreateEvent(It.IsAny<CreateEventDTO>()),Times.Once);

    }
}