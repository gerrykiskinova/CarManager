using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CarManagmentApi.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Cars",
                columns: new[] { "Id", "LicensePlate", "Make", "Model", "ProductionYear" },
                values: new object[,]
                {
                    { 1L, "XYZ123", "Toyota", "Camry", 2020 },
                    { 2L, "ABC789", "Honda", "Civic", 2021 }
                });

            migrationBuilder.InsertData(
                table: "Garages",
                columns: new[] { "Id", "Capacity", "City", "Location", "Name" },
                values: new object[,]
                {
                    { 1L, 20, "City A", "Downtown", "Garage A" },
                    { 2L, 15, "City B", "Uptown", "Garage B" }
                });

            migrationBuilder.InsertData(
                table: "Maintenances",
                columns: new[] { "Id", "CarId", "GarageId", "ScheduledDate", "ServiceType" },
                values: new object[,]
                {
                    { 1L, 1L, 1L, new DateTime(2025, 1, 2, 20, 14, 31, 317, DateTimeKind.Local).AddTicks(801), "Oil Change" },
                    { 2L, 2L, 2L, new DateTime(2025, 1, 5, 20, 14, 31, 317, DateTimeKind.Local).AddTicks(843), "Tire Rotation" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Maintenances",
                keyColumn: "Id",
                keyValue: 1L);

            migrationBuilder.DeleteData(
                table: "Maintenances",
                keyColumn: "Id",
                keyValue: 2L);

            migrationBuilder.DeleteData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 1L);

            migrationBuilder.DeleteData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 2L);

            migrationBuilder.DeleteData(
                table: "Garages",
                keyColumn: "Id",
                keyValue: 1L);

            migrationBuilder.DeleteData(
                table: "Garages",
                keyColumn: "Id",
                keyValue: 2L);
        }
    }
}
