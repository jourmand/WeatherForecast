using MediatR;

namespace WeatherForecast.Infrastructures.Data.Queries.GetWeeklyWeatherForecast;
public record GetWeeklyWeatherForecastQuery : IRequest<string>
{
}

