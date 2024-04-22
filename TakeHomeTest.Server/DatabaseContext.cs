namespace TakeHomeTest.Server
{
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;
    using TakeHomeTest.Server.Domain;

    public class DatabaseContext : DbContext
    {
        #region DbSets
        public DbSet<WeatherForecast> WeatherForecast { get; set; }

        public DbSet<Location> Location { get; set; } 
        #endregion

        public DatabaseContext(DbContextOptions<DatabaseContext> options)
            : base(options)
        {
            if (Database.IsRelational())
            {
                Database.SetCommandTimeout(System.TimeSpan.FromMinutes(5));
            }
        }

    }

}
