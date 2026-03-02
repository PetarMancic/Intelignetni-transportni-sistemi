using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DeltaDrive.Migrations
{
    /// <inheritdoc />
    public partial class _212addingLocationcolumns : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "Location_Latitude",
                table: "Vehicles",
                type: "double precision",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Location_Longitude",
                table: "Vehicles",
                type: "double precision",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "DestinationLocation_Latitude",
                table: "Rides",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "DestinationLocation_Longitude",
                table: "Rides",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "StartLocation_Latitude",
                table: "Rides",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "StartLocation_Longitude",
                table: "Rides",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Location_Latitude",
                table: "Vehicles");

            migrationBuilder.DropColumn(
                name: "Location_Longitude",
                table: "Vehicles");

            migrationBuilder.DropColumn(
                name: "DestinationLocation_Latitude",
                table: "Rides");

            migrationBuilder.DropColumn(
                name: "DestinationLocation_Longitude",
                table: "Rides");

            migrationBuilder.DropColumn(
                name: "StartLocation_Latitude",
                table: "Rides");

            migrationBuilder.DropColumn(
                name: "StartLocation_Longitude",
                table: "Rides");
        }
    }
}
