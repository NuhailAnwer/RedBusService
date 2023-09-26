using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RedBusService.Entities;
using RedBusService.Requests;
using RedBusService.Responses;
using System.Reflection.Metadata.Ecma335;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace RedBusService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BusController : ControllerBase
    {
        BusServiceDbContext BusServicedbContext;
        IConfiguration configuration;
        public BusController(IConfiguration configuration, BusServiceDbContext dbContext)
        {
            this.configuration = configuration;
            this.BusServicedbContext = dbContext;
        }
        [HttpGet]
        public List<BusResponse> Get()
        {
            var Response = BusServicedbContext.Buses.Select(bus => new BusResponse
            {
                Id = bus.Id,
                RouteNumber = bus.RouteNumber.ToString(),
                Capacity = bus.Capacity,
                Condition = bus.Condition,
                fuelLevel = bus.fuelLevel.ToString(),
                TimeOfArrival = bus.TimeOfArrival,
                MaintainenceStates = bus.MaintainenceStates.ToString(),
               

            }).ToList() ;
            
            return Response;

        }
        [HttpPost]
        public bool Post(BusRequest request)
        {
            var busPost = new Bus
            {
                Capacity = request.Capacity,
                Condition = request.Condition,
                fuelLevel=request.fuelLevel,
                RouteNumber=request.RouteNumber,
                MaintainenceStates=request.MaintainenceStates,
                TimeOfArrival=request.TimeOfArrival
            
            };
            BusServicedbContext.Buses.Add(busPost);
            BusServicedbContext.SaveChanges();

            return true;
        }

        [HttpPut("{id}")]
        public ActionResult<bool> Put(int id, BusRequest request)
        {
            var Update = BusServicedbContext.Buses.FirstOrDefault(i=>i.Id==id);

            if (Update == null) return NotFound("Bus not Found");
            
            Update.Capacity = request.Capacity;
            Update.Condition = request.Condition;
            Update.fuelLevel = request.fuelLevel;
            Update.RouteNumber = request.RouteNumber;
            Update.MaintainenceStates = request.MaintainenceStates;
            Update.TimeOfArrival = request.TimeOfArrival;
            
            
            BusServicedbContext.Add(Update);
            BusServicedbContext.SaveChanges();


            return Ok(true);
        }
        [HttpDelete("{id}")]
        public ActionResult<bool> Delete(int id )
        {
            var DeleteBus = BusServicedbContext.Buses.FirstOrDefault(i => i.Id == id);
            if (DeleteBus == null)
                return NotFound("Bus not Found");

            BusServicedbContext.Remove(DeleteBus);
            BusServicedbContext.SaveChanges();


            return Ok(true);
        }/*
        [HttpGet("BusesWithoutDrivers")]
        public ActionResult<BusResponse> GetBus()
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
            return Ok(bus);

        }*/




    }
}
