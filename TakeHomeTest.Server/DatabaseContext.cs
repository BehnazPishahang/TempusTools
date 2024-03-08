namespace TakeHomeTest.Server
{
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;

    public class DatabaseContext : DbContext
    {
        public DbSet<WeatherForecast> WeatherForecast { get; set; }

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
