using RedBusService.Entities;

namespace RedBusService.Requests
{
    public class DriverRequest
    {

        public string DriverName { get; set; }
        public string DriverLicense { get; set; }
        public DateTime DateOfExpiry { get; set; }
        public bool TrafficRuleViolation { get; set; }
        public int YearOfExperience { get; set; }

        public DrivingDetails DrivingDetails { get; set; }
    }
}
