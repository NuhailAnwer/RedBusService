
namespace RedBusService.Entities
{
    public class Bus
    {
        public int Id { get; set; }
        public int Capacity { get; set; }
        public routeNumber RouteNumber { get; set; }
        public string Condition { get; set; }
        public fuelLevel fuelLevel { get; set; }
        public DateTime TimeOfArrival { get; set; }
        public maintainenceStates MaintainenceStates { get; set; }
        public  BusDriver busdriver { get; set; }


    }

    public enum fuelLevel
        {
            Full = 1,
            Half,
            Reserverdfuel
        }
        public enum routeNumber
        {
            R1 = 1,
            R2,
            R3, R4, R5, Rr6, Rr7, Rr8, Rr9
        }
        public enum maintainenceStates
        {
            InService=1,
            UnderMaintainence,
            OutOfOrder
        }
    
}

