using CarManagmentApi.Entities;
using Microsoft.EntityFrameworkCore;

namespace CarManagmentApi.Data
{
    public partial class CarManagmentAppContext : DbContext
    {
        public CarManagmentAppContext()
        {
        }

        public CarManagmentAppContext(DbContextOptions<CarManagmentAppContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Garage> Garages { get; set; }
        public virtual DbSet<Car> Cars { get; set; }
        public virtual DbSet<Maintenance> Maintenances { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Car>()
                .HasMany(c => c.Garages)
                .WithMany(g => g.Cars)
                .UsingEntity(j => j.ToTable("Car_Garage_Join"));

            modelBuilder.Entity<Maintenance>()
                .HasOne(m => m.Garage)
                .WithMany(g => g.Maintenances)
                .HasForeignKey(m => m.GarageId);

            // Seed Data
            modelBuilder.Entity<Garage>().HasData(
                new Garage { Id = 1, Name = "Garage A", Location = "Downtown", City = "City A", Capacity = 20 },
                new Garage { Id = 2, Name = "Garage B", Location = "Uptown", City = "City B", Capacity = 15 }
            );

            modelBuilder.Entity<Car>().HasData(
                new Car { Id = 1, Make = "Toyota", Model = "Camry", ProductionYear = 2020, LicensePlate = "XYZ123" },
                new Car { Id = 2, Make = "Honda", Model = "Civic", ProductionYear = 2021, LicensePlate = "ABC789" }
            );

            modelBuilder.Entity<Maintenance>().HasData(
                new Maintenance { Id = 1, CarId = 1, GarageId = 1, ServiceType = "Oil Change", ScheduledDate = DateTime.Now.AddDays(7) },
                new Maintenance { Id = 2, CarId = 2, GarageId = 2, ServiceType = "Tire Rotation", ScheduledDate = DateTime.Now.AddDays(10) }
            );

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}