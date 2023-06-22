
namespace WeatherForecast.Core.Domain.WeatherAggregate.Contracts;
public interface IWeatherRepository
{
    Task CreateAsync(Weather weather, CancellationToken cancellationToken = default);
    Task<Weather> GetAsync(Guid id, CancellationToken cancellationToken = default);
}

