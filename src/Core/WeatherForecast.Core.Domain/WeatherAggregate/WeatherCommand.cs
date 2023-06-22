using WeatherForecast.Core.Domain.Commons;

namespace WeatherForecast.Core.Domain.WeatherAggregate;
public static class WeatherCommand
{
    public class CreateWeatherCommand
    {
        public Guid Id { get; set; }
        public int Temperature { get; set; }
        public DateTime Timestamp { get; set; }
        public IDateTimeProvider DateTimeProvider { get; set; }
    }
}

