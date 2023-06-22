using WeatherForecast.Core.Domain.Commons;

namespace WeatherForecast.Core.Domain.WeatherAggregate.ValueObjects;

public class Timestamp : Value<Timestamp>
{
    public DateOnly Value { get; private set; }
    public Timestamp(DateOnly value)
    {
        Value = value;
    }

    public static Timestamp Create(DateTime value, IDateTimeProvider dateTime)
    {
        if (value.Date >= dateTime.UtcNow.Date)
            return new Timestamp(DateOnly.FromDateTime(value));

        throw new WeatherExceptions.InvalidEntityState($"Forecast cannot be in the past");
    }

    public static implicit operator DateOnly(Timestamp value) => value.Value;
}
