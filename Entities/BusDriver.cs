namespace RedBusService.Entities
{
    public class BusDriver
    {
       public int Id { get; set; } 
        public int BusId { get; set; }
        public int DriverId { get; set; }
        public Bus bus { get; set; }
        public Driver driver { get; set; }
    
    }
}
