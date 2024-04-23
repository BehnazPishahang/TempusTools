using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TakeHomeTest.Server.Migrations
{
    /// <inheritdoc />
    public partial class CreateLocation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
          
            migrationBuilder.AddColumn<Guid>(
                name: "LocationId",
                table: "WeatherForecast",
                type: "TEXT",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Location",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Location", x => x.Id);
                });

            migrationBuilder.InsertData(
       table: "Location",
       columns: new[] { "Id", "Name" },
       values: new object[,]
       {
            { Guid.NewGuid(), "Location 1" }

       });

            migrationBuilder.Sql(@"
       UPDATE WeatherForecast
SET LocationId = (
    SELECT Id
    FROM Location
           
        );");

            migrationBuilder.CreateIndex(
                name: "IX_WeatherForecast_LocationId",
                table: "WeatherForecast",
                column: "LocationId");

            migrationBuilder.AddForeignKey(
                name: "FK_WeatherForecast_Location_LocationId",
                table: "WeatherForecast",
                column: "LocationId",
                principalTable: "Location",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
           
        }
    }
}
