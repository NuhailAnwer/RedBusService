using RedBusService.Entities;

namespace RedBusService.Requests
{
    public class BusRequest
    {
        public int Capacity { get; set; }
        public routeNumber RouteNumber { get; set; }
        public string Condition { get; set; }
        public fuelLevel fuelLevel { get; set; }
        public DateTime TimeOfArrival { get; set; }
        public maintainenceStates MaintainenceStates { get; set; }

    }
}
