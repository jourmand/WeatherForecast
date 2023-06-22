using WeatherForecast.Core.Domain.WeatherAggregate;
using WeatherForecast.Core.Domain.WeatherAggregate.Contracts;
using WeatherForecast.Infrastructures.Data.Commons;

namespace WeatherForecast.Infrastructures.Data.WeatherAggregate.Repositories;
public class WeatherRepository : IWeatherRepository
{
    private readonly WeatherForecastDbContext _weatherForecastDbContext;

    public WeatherRepository(WeatherForecastDbContext weatherForecastDbContext)
    {
        _weatherForecastDbContext = weatherForecastDbContext;
    }

    public async Task CreateAsync(Weather weather, CancellationToken cancellationToken = default)
    {
        await _weatherForecastDbContext.WeatherData.AddAsync(weather, cancellationToken);
    }

    public async Task<Weather> GetAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _weatherForecastDbContext.WeatherData.FindAsync(id);
    }
}

