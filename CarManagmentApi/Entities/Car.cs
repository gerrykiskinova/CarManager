using System.ComponentModel.DataAnnotations;

namespace CarManagmentApi.Entities
{
    public class Car
    {
        public long Id { get; set; }
        public string Model { get; set; }
        public int ProductionYear { get; set; }
        public string Make { get; set; }
        public string LicensePlate { get; set; }
        public ICollection<Garage> Garages { get; set; } = new List<Garage>();
    }
}
