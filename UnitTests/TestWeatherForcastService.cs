using Microsoft.EntityFrameworkCore;
using System.Reflection;
using TakeHomeTest.Server;
using TakeHomeTest.Server.Services;

namespace UnitTests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        #region Get tests
        [Test]
        public async Task WeatherForecastService_GetAllWeatherForcasts_SingleResult()
        {
            var options = new DbContextOptionsBuilder<DatabaseContext>()
                .UseInMemoryDatabase(databaseName: TestContext.CurrentContext.Test.Name)
                .Options;
            using var context = new DatabaseContext(options);

            context.WeatherForecast.Add(new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Today),
                TemperatureC = 32,
                TemperatureF = (int) (32 * 1.8 + 32),
                Summary = "Test"
            });
            
            context.SaveChanges();
            context.ChangeTracker.Clear();

            var weatherForecastService = new WeatherForecastService(context);

            var result = await weatherForecastService.GetAllWeatherForecasts();

            Assert.IsNotNull(result);
            Assert.That(result.Count, Is.EqualTo(1));
            Assert.That(result.First().Date, Is.EqualTo(DateOnly.FromDateTime(DateTime.Today)));
            Assert.That(result.First().TemperatureC, Is.EqualTo(32));
            Assert.That(result.First().TemperatureF, Is.EqualTo((int)(32 * 1.8 + 32)));
            Assert.That(result.First().Summary, Is.EqualTo("Test"));
        }

        [Test]
        public async Task WeatherForecastService_GetAllWeatherForcasts_ManyResults()
        {
            var options = new DbContextOptionsBuilder<DatabaseContext>()
                .UseInMemoryDatabase(databaseName: TestContext.CurrentContext.Test.Name)
                .Options;
            using var context = new DatabaseContext(options);

            context.WeatherForecast.Add(new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Today),
                TemperatureC = 32,
                TemperatureF = (int)(32 * 1.8 + 32),
                Summary = "Test1"
            });
            context.WeatherForecast.Add(new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Today.AddDays(1)),
                TemperatureC = 10,
                TemperatureF = (int)(10 * 1.8 + 32),
                Summary = "Test2"
            });
            context.WeatherForecast.Add(new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Today.AddDays(-1)),
                TemperatureC = 50,
                TemperatureF = (int)(50 * 1.8 + 32),
                Summary = "Test3"
            });

            context.SaveChanges();
            context.ChangeTracker.Clear();

            var weatherForecastService = new WeatherForecastService(context);

            var result = await weatherForecastService.GetAllWeatherForecasts();

            Assert.IsNotNull(result);
            Assert.That(result.Count, Is.EqualTo(3));
            var results = result.ToList();
            Assert.That(results.ElementAt(0).Summary, Is.EqualTo("Test1"));
            Assert.That(results.ElementAt(1).Summary, Is.EqualTo("Test2"));
            Assert.That(results.ElementAt(2).Summary, Is.EqualTo("Test3"));

        }
        
        [Test]
        public async Task WeatherForecastService_GetAllWeatherForcasts_NoResults()
        {
            var options = new DbContextOptionsBuilder<DatabaseContext>()
                .UseInMemoryDatabase(databaseName: TestContext.CurrentContext.Test.Name)
                .Options;
            using var context = new DatabaseContext(options);


            var weatherForecastService = new WeatherForecastService(context);

            var result = await weatherForecastService.GetAllWeatherForecasts();

            Assert.IsNotNull(result);
            Assert.That(result.Count, Is.EqualTo(0));
        }
        #endregion

        #region Update tests
        [Test]
        public async Task WeatherForecastService_UpdateForecast_FullUpdate()
        {
            var options = new DbContextOptionsBuilder<DatabaseContext>()
                .UseInMemoryDatabase(databaseName: TestContext.CurrentContext.Test.Name)
                .Options;
            using var context = new DatabaseContext(options);

            var id = Guid.NewGuid();

            context.WeatherForecast.Add(new WeatherForecast
            {
                Id = id,
                Date = DateOnly.FromDateTime(DateTime.Today),
                TemperatureC = 32,
                TemperatureF = (int)(32 * 1.8 + 32),
                Summary = "Test"
            });

            context.SaveChanges();
            context.ChangeTracker.Clear();

            var weatherForecastService = new WeatherForecastService(context);

            var result = await weatherForecastService.UpdateWeatherForecast(
                new WeatherForecast
                {
                    Id = id,
                    Date = DateOnly.FromDateTime(DateTime.Today.AddDays(1)),
                    TemperatureC = 20,
                    TemperatureF = 10,
                    Summary = "Test2"
                }
            );

            Assert.IsNotNull(result);
            Assert.That(result.Id, Is.EqualTo(id));
            Assert.That(result.Date, Is.EqualTo(DateOnly.FromDateTime(DateTime.Today.AddDays(1))));
            Assert.That(result.TemperatureC, Is.EqualTo(20));
            Assert.That(result.TemperatureF, Is.EqualTo(10));
            Assert.That(result.Summary, Is.EqualTo("Test2"));

            var dbResults = context.WeatherForecast.Where(wf => wf.Id == id).ToList().First();

            Assert.That(result.Id, Is.EqualTo(dbResults.Id));
            Assert.That(result.Date, Is.EqualTo(dbResults.Date));
            Assert.That(result.TemperatureC, Is.EqualTo(dbResults.TemperatureC));
            Assert.That(result.TemperatureF, Is.EqualTo(dbResults.TemperatureF));
            Assert.That(result.Summary, Is.EqualTo(dbResults.Summary));
        }

        [Test]
        public async Task WeatherForecastService_UpdateForecast_PartialUpdate()
        {
            var options = new DbContextOptionsBuilder<DatabaseContext>()
                .UseInMemoryDatabase(databaseName: TestContext.CurrentContext.Test.Name)
                .Options;
            using var context = new DatabaseContext(options);

            var id = Guid.NewGuid();

            context.WeatherForecast.Add(new WeatherForecast
            {
                Id = id,
                Date = DateOnly.FromDateTime(DateTime.Today),
                TemperatureC = 32,
                TemperatureF = (int)(32 * 1.8 + 32),
                Summary = "Test"
            });

            context.SaveChanges();
            context.ChangeTracker.Clear();

            var weatherForecastService = new WeatherForecastService(context);

            var result = await weatherForecastService.UpdateWeatherForecast(
                new WeatherForecast
                {
                    Id = id,
                    TemperatureF = 10,
                }
            );

            Assert.IsNotNull(result);
            Assert.That(result.Id, Is.EqualTo(id));
            Assert.That(result.Date, Is.EqualTo(DateOnly.FromDateTime(DateTime.Today)));
            Assert.That(result.TemperatureC, Is.EqualTo(32));
            Assert.That(result.TemperatureF, Is.EqualTo(10));
            Assert.That(result.Summary, Is.EqualTo("Test"));

            var dbResults = context.WeatherForecast.Where(wf => wf.Id == id).ToList().First();

            Assert.That(result.Id, Is.EqualTo(dbResults.Id));
            Assert.That(result.Date, Is.EqualTo(dbResults.Date));
            Assert.That(result.TemperatureC, Is.EqualTo(dbResults.TemperatureC));
            Assert.That(result.TemperatureF, Is.EqualTo(dbResults.TemperatureF));
            Assert.That(result.Summary, Is.EqualTo(dbResults.Summary));
        }

        [Test]
        public async Task WeatherForecastService_UpdateForecast_ForecastDoesNotExist()
        {
            var options = new DbContextOptionsBuilder<DatabaseContext>()
                .UseInMemoryDatabase(databaseName: TestContext.CurrentContext.Test.Name)
                .Options;
            using var context = new DatabaseContext(options);

            var id = Guid.NewGuid();

            var weatherForecastService = new WeatherForecastService(context);

            var result = await weatherForecastService.UpdateWeatherForecast(
                new WeatherForecast
                {
                    Id = id,
                    TemperatureF = 10,
                }
            );

            Assert.IsNull(result);

            var dbResults = context.WeatherForecast.Where(wf => wf.Id == id).ToList();

            Assert.That(dbResults.Count, Is.EqualTo(0));
        }
        #endregion

        #region Create Tests

        [Test]
        public async Task WeatherForecastService_CreateForecast_ForecastAlreadyExist()
        {
            var options = new DbContextOptionsBuilder<DatabaseContext>()
                .UseInMemoryDatabase(databaseName: TestContext.CurrentContext.Test.Name)
                .Options;
            using var context = new DatabaseContext(options);

            var id = Guid.NewGuid();

            context.WeatherForecast.Add(new WeatherForecast
            {
                Id = id,
                Date = DateOnly.FromDateTime(DateTime.Today),
                TemperatureC = 32,
                TemperatureF = (int)(32 * 1.8 + 32),
                Summary = "Test"
            });

            context.SaveChanges();
            context.ChangeTracker.Clear();

            var weatherForecastService = new WeatherForecastService(context);

            var newWeatherForecast = new WeatherForecast
            {
                Id = id,
                Date = DateOnly.FromDateTime(DateTime.Today),
                TemperatureC = 5,
                TemperatureF = 5,
                Summary = "Test"
            };

            var result = await weatherForecastService.CreateWeatherForecast(
                newWeatherForecast
            );

            Assert.IsNull(result);

            var dbResults = context.WeatherForecast.Where(wf => wf.Id == id).ToList();
            Assert.That(dbResults.Count, Is.EqualTo(1));
        }

        [Test]
        public async Task WeatherForecastService_CreateForecast_ForecastDoesNotExist()
        {
            var options = new DbContextOptionsBuilder<DatabaseContext>()
                .UseInMemoryDatabase(databaseName: TestContext.CurrentContext.Test.Name)
                .Options;
            using var context = new DatabaseContext(options);

            var id = Guid.NewGuid();

            var weatherForecastService = new WeatherForecastService(context);

            var newForecast = new WeatherForecast
            {
                Id = id,
                TemperatureF = 10,
                TemperatureC = 10,
                Summary = "test",
                Date = DateOnly.FromDateTime(DateTime.Today),
            };

            var result = await weatherForecastService.CreateWeatherForecast(
                newForecast
            );;

            Assert.IsNotNull(result);

            var dbResults = context.WeatherForecast.Where(wf => wf.Id == id).ToList();

            Assert.That(dbResults.Count, Is.EqualTo(1));
            Assert.That(dbResults.First().Id, Is.EqualTo(newForecast.Id));
            Assert.That(dbResults.First().TemperatureC, Is.EqualTo(newForecast.TemperatureC));
            Assert.That(dbResults.First().TemperatureF, Is.EqualTo(newForecast.TemperatureF));
            Assert.That(dbResults.First().Date, Is.EqualTo(newForecast.Date));
            Assert.That(dbResults.First().Summary, Is.EqualTo(newForecast.Summary));

        }
        #endregion

        #region Delete Tests

        [Test]
        public async Task WeatherForecastService_DeleteForecast_ForecastAlreadyExist()
        {
            var options = new DbContextOptionsBuilder<DatabaseContext>()
                .UseInMemoryDatabase(databaseName: TestContext.CurrentContext.Test.Name)
                .Options;
            using var context = new DatabaseContext(options);

            var id = Guid.NewGuid();

            context.WeatherForecast.Add(new WeatherForecast
            {
                Id = id,
                Date = DateOnly.FromDateTime(DateTime.Today),
                TemperatureC = 32,
                TemperatureF = (int)(32 * 1.8 + 32),
                Summary = "Test"
            });

            context.SaveChanges();
            context.ChangeTracker.Clear();

            var weatherForecastService = new WeatherForecastService(context);

            var result = await weatherForecastService.DeleteWeatherForecast(id);

            Assert.IsTrue(result);

            var dbResults = context.WeatherForecast.Where(wf => wf.Id == id).ToList();

            Assert.That(dbResults.Count, Is.EqualTo(0));

        }

        [Test]
        public async Task WeatherForecastService_DeleteForecast_ForecastDoesNotExist()
        {
            var options = new DbContextOptionsBuilder<DatabaseContext>()
                .UseInMemoryDatabase(databaseName: TestContext.CurrentContext.Test.Name)
                .Options;
            using var context = new DatabaseContext(options);

            var id = Guid.NewGuid();

            var weatherForecastService = new WeatherForecastService(context);

            var result = await weatherForecastService.DeleteWeatherForecast(id);

            Assert.IsFalse(result);

            var dbResults = context.WeatherForecast.Where(wf => wf.Id == id).ToList();

            Assert.That(dbResults.Count, Is.EqualTo(0));

        }
        #endregion
    }
}