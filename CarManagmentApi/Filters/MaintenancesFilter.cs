namespace ApiCarManagment.Filters
{
    public class MaintenancesFilter
    {
        public long? CarId { get; set; }
        public int? GarageId { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}
