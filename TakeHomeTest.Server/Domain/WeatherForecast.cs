using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

namespace TakeHomeTest.Server.Domain
{
    public class WeatherForecast
    {
        public Guid Id { get; set; }

        //[JsonConverter(typeof(DateOnlyConverter))]
        public DateOnly Date { get; set; }

        public int TemperatureC { get; set; }

        public int TemperatureF { get; set; }

        public string? Summary { get; set; }

        public bool IsTemperatureCSet { get; set; }

        public bool IsTemperatureFSet { get; set; }


        #region Foreign Key
        public Guid? LocationId { get; set; }
        #endregion


        #region Navigation
         public Location? Location { get; set; }
        #endregion


    }


}
