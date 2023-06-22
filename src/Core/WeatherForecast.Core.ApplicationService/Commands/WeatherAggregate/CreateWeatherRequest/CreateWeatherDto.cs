namespace WeatherForecast.Core.ApplicationService.Commands.WeatherAggregate.CreateWeatherRequest;
public class CreateWeatherDto
{
    public Guid Id { get; set; }
    public int Temperature { get; set; }
    public DateOnly Timestamp { get; set; }
}

