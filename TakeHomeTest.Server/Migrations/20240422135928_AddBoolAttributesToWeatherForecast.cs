using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TakeHomeTest.Server.Migrations
{
    /// <inheritdoc />
    public partial class AddBoolAttributesToWeatherForecast : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsTemperatureCSet",
                table: "WeatherForecast",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsTemperatureFSet",
                table: "WeatherForecast",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsTemperatureCSet",
                table: "WeatherForecast");

            migrationBuilder.DropColumn(
                name: "IsTemperatureFSet",
                table: "WeatherForecast");
        }
    }
}
