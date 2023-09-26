using RedBusService.Entities;

namespace RedBusService.Responses
{
    public class DriverResponse
    {
        public int Id { get; set; }
        public string DriverName { get; set; }
        public string DriverLicense { get; set; }
        public DateTime DateOfExpiry { get; set; }
        public bool TrafficRuleViolation { get; set; }
        public int YearOfExperience { get; set; }

        public string DrivingDetails { get; set; }
        public BusDriver bus { get; set; }

    }
}
