namespace ApiCarManagment.Filters
{
    public class CarsFilter
    {
        public string? CarMake { get; set; }
        public long? GarageId { get; set; }
        public int? FromYear { get; set; }
        public int? ToYear { get; set; }
    }

}
