namespace ApiCarManagment.Dtos
{
    public class GaragesDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public string City { get; set; }
        public int Capacity { get; set; }
        public GaragesDto(long id, string name, string location, string city, int capacity)
        {
            Id = id;
            Name = name;
            Location = location;
            City = city;
            Capacity = capacity;
        }
    }

}
