namespace ApiCarManagment.Dtos
{
    public class CarDto
    {
        public long Id { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public int ProductionYear { get; set; }
        public string LicensePlate { get; set; }
        public List<GaragesDto>? Garages { get; set; }
        public List<long> GarageIds { get; set; }
        public CarDto(long id, string make, string model, int productionYear, string licensePlate, List<GaragesDto>? garages, List<long> garageIds)
        {
            Id = id;
            Make = make;
            Model = model;
            ProductionYear = productionYear;
            LicensePlate = licensePlate;
            Garages = garages;
            GarageIds = garageIds;
        }
    }
}
