using WeatherForecast.Core.Domain.Commons;
using WeatherForecast.Core.Domain.WeatherAggregate.ValueObjects;
using static WeatherForecast.Core.Domain.WeatherAggregate.WeatherCommand;

namespace WeatherForecast.Core.Domain.WeatherAggregate;
public class Weather : AggregateRoot
{
    public Temperature Temperature { get; private set; }
    public Timestamp Timestamp { get; private set; }

    public static Weather Create(CreateWeatherCommand weatherCommand) =>
       new () {
           Id = weatherCommand.Id,
           Temperature = Temperature.Create(weatherCommand.Temperature), 
           Timestamp = Timestamp.Create(weatherCommand.Timestamp, weatherCommand.DateTimeProvider) 
       };
}
