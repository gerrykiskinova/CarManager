using Microsoft.AspNetCore.Mvc;

namespace ApiCarManagment.Filters
{
    public class MaintenancesMonthlyFilter
    {
        public int? GarageId { get; set; }

        public DateTime StartMonth { get; set; }

        public DateTime EndMonth { get; set; }
    }
}
