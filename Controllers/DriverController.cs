using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RedBusService.Entities;
using RedBusService.Requests;
using RedBusService.Responses;

namespace RedBusService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DriverController : ControllerBase
    {
        BusServiceDbContext BusServicedbContext;
        IConfiguration configuration;
        public DriverController(IConfiguration configuration, BusServiceDbContext dbContext)
        {
            this.configuration = configuration;
            this.BusServicedbContext = dbContext;
        }
        [HttpGet]
        public List<DriverResponse> Get()
        {
            var DriverGet = BusServicedbContext.Drivers.Select(mn => new DriverResponse
            {
                Id=mn.Id,
                DriverName = mn.DriverName,
                DriverLicense = mn.DriverLicense,
                DateOfExpiry = mn.DateOfExpiry,
                DrivingDetails = mn.DrivingDetails.ToString(),
                YearOfExperience = mn.YearOfExperience,
                TrafficRuleViolation = mn.TrafficRuleViolation

            }).ToList();
            return DriverGet;


        }
        [HttpGet("{id}")]
        public ActionResult<DriverResponse> GetSpecific(int id)
        {
            var DriverGet= BusServicedbContext.Drivers
            .Select(mn => new DriverResponse
            {
                DriverName = mn.DriverName,
                DriverLicense = mn.DriverLicense,
                DateOfExpiry = mn.DateOfExpiry,
                DrivingDetails = mn.DrivingDetails.ToString(),
                YearOfExperience = mn.YearOfExperience,
                TrafficRuleViolation = mn.TrafficRuleViolation

            }).FirstOrDefault(i => i.Id == id);
            if (DriverGet == null) return NotFound("driver not found");
            return DriverGet;


        }





        [HttpPost]
        public bool Post(DriverRequest request)
        {
            var DriverPost = new Driver
            {
                DriverName = request.DriverName,
                DriverLicense = request.DriverLicense,
                DrivingDetails = request.DrivingDetails,
                DateOfExpiry = request.DateOfExpiry,
                TrafficRuleViolation = request.TrafficRuleViolation,
                YearOfExperience = request.YearOfExperience
            };
            BusServicedbContext.Add(DriverPost);
            BusServicedbContext.SaveChanges();

            return true;
        }
        [HttpPut("{id}")]
        public ActionResult<bool> Put(int id,DriverRequest request)
        {
            var Update = BusServicedbContext.Drivers.FirstOrDefault(i => i.Id == id);

            if (Update == null) return NotFound("Bus not Found");

            Update.DriverName = request.DriverName;
            Update.DriverLicense = request.DriverLicense;
            Update.DrivingDetails = request.DrivingDetails;
            Update.DateOfExpiry = request.DateOfExpiry;
            Update.TrafficRuleViolation = request.TrafficRuleViolation;
            Update.YearOfExperience = request.YearOfExperience;

            BusServicedbContext.Add(Update);
            BusServicedbContext.SaveChanges();


            return Ok(true);
        }

        [HttpDelete("{id}")]
        public ActionResult<bool> Delete(int id)
        {
            var DeleteBus = BusServicedbContext.Drivers.FirstOrDefault(i => i.Id == id);
            if (DeleteBus == null)
                return NotFound("Bus not Found");

            BusServicedbContext.Remove(DeleteBus);
            BusServicedbContext.SaveChanges();


            return Ok(true);
        }
        

    }
}
