using System;
using Microsoft.EntityFrameworkCore.Migrations;
using TakeHomeTest.Server.Services;

#nullable disable

namespace TakeHomeTest.Server.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "WeatherForecast",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Date = table.Column<DateOnly>(type: "TEXT", nullable: false),
                    TemperatureC = table.Column<int>(type: "INTEGER", nullable: false),
                    TemperatureF = table.Column<int>(type: "INTEGER", nullable: false),
                    Summary = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WeatherForecast", x => x.Id);
                });
            var rnd = new Random(DateTime.Now.Millisecond);
            for (int i = 0; i < 10; i++)
            {
                var id = Guid.NewGuid();
                var date = new DateTime(2024, 3, rnd.Next(1, 30));
                var temperatureC = rnd.Next(-5, 50);
                var temperatureF = temperatureC * 1.8 + 32;
                var summary = WeatherForecastService.GetSummary(temperatureC);

                migrationBuilder.InsertData(
                    "WeatherForecast", ["Id", "Date", "TemperatureC", "TemperatureF", "Summary"], [id, date, temperatureC, temperatureF, summary]
                );
            }
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WeatherForecast");
        }
    }
}
