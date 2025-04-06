namespace Event.api.UnitTest;

using AutoFixture;
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
    public async void CreateEventController_Calls_ServicelayerMethod()
    {
        svc.Setup(y => y.CreateEvent(It.IsAny<CreateEventDTO>())).ReturnsAsync(1);

        CreatedAtActionResult result= (CreatedAtActionResult) await _contoller.CreateEvent(null);

        svc.Verify(x=>x.CreateEvent(It.IsAny<CreateEventDTO>()),Times.Once);

    }

    [Fact]
    //checks that CreatedAtAction is returned by CreateEvent Action method
    public async void CreateEventActions_Returns_201_and_CreatedAtAction()
    {
         svc.Setup(y => y.CreateEvent(It.IsAny<CreateEventDTO>())).ReturnsAsync(1);
        //generate dummy Event Data
        var fixture = new Fixture();
        var dto=fixture.Create<CreateEventDTO>();
        var result= await _contoller.CreateEvent(dto);  
       

        Assert.IsType<CreatedAtActionResult>(result);

        
    }

    [Fact]
    // checks if the service layer is called from GetEvent Action method
    public async void GetEventAction_Calls_ServicelayerMethod()
    {
        svc.Setup(y=>y.ViewEvent(It.IsAny<Int16>())).ReturnsAsync(new EventDTO());
        _contoller = new EventController(svc.Object, logger.Object);

        await  _contoller.GetEvent(1);

        svc.Verify(x => x.ViewEvent(It.IsAny<Int16>()), Times.Once);

    }

    [Fact]
    // returns status ok if entity found
    public async void GetEventAction_Returns_200_When_EntityFound()
    {
        svc.Setup(y => y.ViewEvent(1)).ReturnsAsync(new EventDTO());
        _contoller = new EventController(svc.Object, logger.Object);

       IActionResult result= await _contoller.GetEvent(1);

       Assert.IsType<OkObjectResult>(result);

    }
    [Fact]
    // returns status Notfound if entity not found
    public async void GetEventAction_Returns_NotFound_When_EntityNotFound()
    {
        svc.Setup(y => y.ViewEvent(100)).ReturnsAsync((EventDTO)null);
        _contoller = new EventController(svc.Object, logger.Object);

        IActionResult result = await _contoller.GetEvent(100);

        Assert.IsType<NotFoundResult>(result);

    }


}