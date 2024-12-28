using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CarManagmentApi.Entities
{
    public class Maintenance
    {
        public long Id { get; set; }
        public long CarId { get; set; }
        public Car Car { get; set; }
        public long GarageId { get; set; }
        public Garage Garage { get; set; }
        public string ServiceType { get; set; }
        public DateTime ScheduledDate { get; set; }
    }
}
