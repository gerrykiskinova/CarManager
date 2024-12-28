using System.ComponentModel.DataAnnotations;

namespace CarManagmentApi.Entities
{
    public class Garage
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public int Capacity { get; set; }

        public string City { get; set; }
        public ICollection<Maintenance> Maintenances { get; set; } = new List<Maintenance>();
        public ICollection<Car> Cars { get; set; } = new List<Car>();
    }
}
