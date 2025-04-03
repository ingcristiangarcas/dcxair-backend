using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DCXAir.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddFlightCarrierAndNumber : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FlightCarrier",
                table: "Flights",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "FlightNumber",
                table: "Flights",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FlightCarrier",
                table: "Flights");

            migrationBuilder.DropColumn(
                name: "FlightNumber",
                table: "Flights");
        }
    }
}
