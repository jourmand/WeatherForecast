
namespace WeatherForecast.Core.Domain.Commons;

public interface IDateTimeProvider
{
    public DateTime UtcNow { get; }
    public DateOnly DateOnlyNow { get; }
}


public class DateTimeProvider : IDateTimeProvider
{
    public DateTime UtcNow => DateTime.UtcNow;
    public DateOnly DateOnlyNow => DateOnly.FromDateTime(DateTime.UtcNow);
}