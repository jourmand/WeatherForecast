using MediatR;
using Microsoft.EntityFrameworkCore;
using WeatherForecast.Core.Domain.Commons;
using WeatherForecast.Infrastructures.Data.Commons;

namespace WeatherForecast.Infrastructures.Data.Queries.GetWeeklyWeatherForecast;
public class GetWeeklyWeatherForecastQueryHandler : IRequestHandler<GetWeeklyWeatherForecastQuery, string>
{
    private readonly WeatherForecastDbContext _dbContext;
    private readonly IDateTimeProvider _dateTimeProvider;

    public GetWeeklyWeatherForecastQueryHandler(WeatherForecastDbContext dbContext,
        IDateTimeProvider dateTimeProvider)
    {
        _dbContext = dbContext;
        _dateTimeProvider = dateTimeProvider;
    }
    public async Task<string> Handle(GetWeeklyWeatherForecastQuery request, CancellationToken cancellationToken)
    {
        if (!_dbContext.WeatherData.Any())
            return "Unknown";
        
        var averageTemp = await _dbContext.WeatherData
        .Where(t => t.Timestamp.Value <= _dateTimeProvider.DateOnlyNow.AddDays(7))
        .AverageAsync(t => t.Temperature.Value, cancellationToken);

        return averageTemp switch
        {
            var temp when temp < 0 => "Freezing",
            var temp when temp >= 0 && temp < 10 => "Bracing",
            var temp when temp >= 10 && temp < 15 => "Chilly",
            var temp when temp >= 15 && temp < 20 => "Cool",
            var temp when temp >= 20 && temp < 25 => "Mild",
            var temp when temp >= 25 && temp < 30 => "Warm",
            var temp when temp >= 30 && temp < 35 => "Balmy",
            var temp when temp >= 35 && temp < 40 => "Hot",
            var temp when temp >= 40 && temp < 45 => "Sweltering",
            var temp when temp >= 45 => "Scorching",
            _ => "Unknown",
        };
    }
}

