using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RedBusService.Entities;
using RedBusService.Requests;
using RedBusService.Responses;

namespace RedBusService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BusDriverController : ControllerBase
    {

        BusServiceDbContext BusServicedbContext;
        IConfiguration configuration;
        public BusDriverController(IConfiguration configuration, BusServiceDbContext dbContext)
        {
            this.configuration = configuration;
            this.BusServicedbContext = dbContext;
        }
        [HttpPost]
        public bool Post(BusDriverRequest request)
        {
            var busDriver = new BusDriver
            {
                BusId=request.BusId,
                DriverId=request.DriverId,
            };
            return true;
        }



        [HttpGet("BusesWithoutDrivers")]
        public ActionResult<BusResponse> Get()
        {
            var bus = BusServicedbContext.BusDrivers.Select(bus => new BusResponse
            {
                Id = bus.Id,
                RouteNumber = bus.bus.RouteNumber.ToString(),
                Capacity = bus.bus.Capacity,
                Condition = bus.bus.Condition,
                fuelLevel = bus.bus.fuelLevel.ToString(),
                TimeOfArrival = bus.bus.TimeOfArrival,
                MaintainenceStates = bus.bus.MaintainenceStates.ToString(),

            }).ToList().Where(any => any.driver.BusId == null);
            if (bus == null) return NotFound("bus not found");

            return Ok(bus);

        }

        [HttpGet("DriverssWithoutBuses")]
        public ActionResult<BusResponse> Get(int id)
        {
            var bus = BusServicedbContext.BusDrivers.Select(mn => new DriverResponse
            {
                DriverName = mn.driver.DriverName,
                DriverLicense = mn.driver.DriverLicense,
                DateOfExpiry = mn.driver.DateOfExpiry,
                DrivingDetails = mn.driver.DrivingDetails.ToString(),
                YearOfExperience = mn.driver.YearOfExperience,
                TrafficRuleViolation = mn.driver.TrafficRuleViolation

            }).ToList().Where(any => any.bus.DriverId == null);
            if (bus == null) return NotFound("bus not found");

            return Ok(bus);

        }


    }
}
