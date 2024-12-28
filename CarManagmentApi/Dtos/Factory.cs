using ApiCarManagment.Dtos;
using CarManagmentApi.Data;
using CarManagmentApi.Entities;
using Microsoft.EntityFrameworkCore;

namespace CarManagmentApi.Dtos
{
    public static class Factory
    {
        public static CarDto ToDto(Car car)
        {
            return new CarDto(
                 car.Id,
                 car.Make,
                 car.Model,
                 car.ProductionYear,
                 car.LicensePlate,
                 car.Garages.Select(g => ToDto(g)).ToList(),
                 car.Garages.Select(g => g.Id).ToList()
            );
        }
        public static GaragesDto ToDto(Garage garage)
        {
            return new GaragesDto(
                garage.Id,
                garage.Location,
                garage.Name,
                garage.City,
                garage.Capacity
            );
        }
        public static MaintenanceDto ToDto(Maintenance maintenance)
        {
            return new MaintenanceDto(
                maintenance.Id,
                maintenance.Car.Id,
                maintenance.Car.Model,
                maintenance.ServiceType,
                DateOnly.FromDateTime(maintenance.ScheduledDate),
                maintenance.Garage.Id,
                maintenance.Garage.Name
            );
        }
        public static async Task<Car> ToEntity(CarManagmentAppContext _context, CarDto carDto)
        {
            List<Garage?> garages = (await Task.WhenAll(carDto.GarageIds.Select(async id => await _context.Garages.FindAsync(id))))
                                  .Where(g => g != null)
                                  .ToList();
            Car car = new()
            {
                Id = carDto.Id,
                Make = carDto.Make,
                Model = carDto.Model,
                ProductionYear = carDto.ProductionYear,
                LicensePlate = carDto.LicensePlate,
                Garages = garages
            };

            return car;
        }
        public static async Task<Maintenance> ToEntity(CarManagmentAppContext _context, MaintenanceDto maintenanceDto)
        {
            Car car = await _context.Cars.FindAsync(maintenanceDto.CarId);
            Garage garage = await _context.Garages.FindAsync(maintenanceDto.GarageId);
            Maintenance maintenance = new()
            {
                Id = maintenanceDto.Id,
                Car = car,
                ServiceType = maintenanceDto.ServiceType,
                ScheduledDate = maintenanceDto.ScheduledDate.ToDateTime(TimeOnly.MinValue),
                Garage = garage
            };

            return maintenance;
        }
        public static Garage ToEntity(GaragesDto garagesDto)
        {
            Garage garage = new()
            {
                Id = garagesDto.Id,
                Name = garagesDto.Name,
                Location = garagesDto.Location,
                City = garagesDto.City,
                Capacity = garagesDto.Capacity,
            };

            return garage;
        }
    }
}
