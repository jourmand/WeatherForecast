using WeatherForecast.Core.Domain.Commons;
using WeatherForecast.Core.Domain.WeatherAggregate;
using WeatherForecast.Infrastructures.Data.Commons;

namespace Test.WeatherForecast.Infrastructure.Infrastructure
{
    public class DatabaseInitializer
    {
        public static void Initialize(WeatherForecastDbContext context)
        {
            if (!context.WeatherData.Any())
                SeedWeatherData(context);
        }

        private static void SeedWeatherData(WeatherForecastDbContext context)
        {
            var weatherItems = new[]
            {
                Weather.Create(new WeatherCommand.CreateWeatherCommand
                {
                    Id = Guid.Parse("654b7573-9501-436a-ad36-94c5696ac28f"),
                    DateTimeProvider = new DateTimeProvider(),
                    Temperature = 10,
                    Timestamp  = DateTime.Now
                }),
                Weather.Create(new WeatherCommand.CreateWeatherCommand
                {
                    Id = Guid.Parse("971316e1-4966-4426-b1ea-a36c9dde1066"),
                    DateTimeProvider = new DateTimeProvider(),
                    Temperature = 60,
                    Timestamp  = DateTime.Now.AddDays(2)
                }),
                Weather.Create(new WeatherCommand.CreateWeatherCommand
                {
                    Id = Guid.Parse("971316e1-4966-4426-b1ea-a36c9dde1067"),
                    DateTimeProvider = new DateTimeProvider(),
                    Temperature = 40,
                    Timestamp  = DateTime.Now.AddDays(1)
                }),
                Weather.Create(new WeatherCommand.CreateWeatherCommand
                {
                    Id = Guid.Parse("971316e1-4966-4426-b1ea-a36c9dde1068"),
                    DateTimeProvider = new DateTimeProvider(),
                    Temperature = 5,
                    Timestamp  = DateTime.Now.AddDays(4)
                }),
            };

            context.WeatherData.AddRange(weatherItems);
            context.SaveChanges();
        }

    }
}
