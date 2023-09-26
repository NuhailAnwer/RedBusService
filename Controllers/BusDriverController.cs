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
            BusServicedbContext.BusDrivers.Add(busDriver);
            BusServicedbContext.SaveChanges();

            return true;
        }

        [HttpDelete("Delete_Driver_of_Bus")]
        public ActionResult<bool> DeleteDriverBus(int id) 
        {
            var Del = BusServicedbContext.BusDrivers.FirstOrDefault(i => i.DriverId == id);
           
            if (Del == null) return NotFound("bus not found");
            BusServicedbContext.Remove(Del);
            BusServicedbContext.SaveChanges();
            return true;

        }

        [HttpDelete("Delete_Bus_of_Driver")]
        public ActionResult<bool> DeleteBusDriver(int id)
        {
            var Del = BusServicedbContext.BusDrivers.FirstOrDefault(i => i.BusId == id);

            if (Del == null) return NotFound("bus not found");
            BusServicedbContext.Remove(Del);
            BusServicedbContext.SaveChanges();
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

            }).ToList();
            if (bus == null) return NotFound("bus not found");
            //Where(any => any.bus.busdriver.DriverId == 0)
            return Ok(bus);

        }

        [HttpGet("DriverssWithoutBuses")]
        public ActionResult<BusResponse> GetDriverWithout()
        {
            var bus = BusServicedbContext.Drivers.Where(bd => bd.busdriver.DriverId == bd.busdriver.driver.Id).Select(mn => new DriverResponse
            {
                DriverName = mn.DriverName,
                DriverLicense = mn.DriverLicense,
                DateOfExpiry = mn.DateOfExpiry,
                DrivingDetails = mn.DrivingDetails.ToString(),
                YearOfExperience = mn.YearOfExperience,
                TrafficRuleViolation = mn.TrafficRuleViolation

            }).ToList();

            
            if (bus == null) return NotFound("bus not found");

            return Ok(bus);

        }


    }
}
