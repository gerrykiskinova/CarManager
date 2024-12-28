namespace ApiCarManagment.Dtos
{
    public class MaintenanceDto
    {
        public long Id { get; set; }
        public long CarId { get; set; }
        public string? CarName { get; set; }
        public string ServiceType { get; set; }
        public DateOnly ScheduledDate { get; set; }
        public long GarageId { get; set; }
        public string? GarageName { get; set; }
        public MaintenanceDto(long id, long carId, string? carName, string serviceType, DateOnly scheduledDate, long garageId, string? garageName)
        {
            Id = id;
            CarId = carId;
            CarName = carName;
            ServiceType = serviceType;
            ScheduledDate = scheduledDate;
            GarageId = garageId;
            GarageName = garageName;
        }
    }
}
