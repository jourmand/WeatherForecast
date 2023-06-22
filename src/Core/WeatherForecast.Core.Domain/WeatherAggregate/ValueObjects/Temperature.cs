using WeatherForecast.Core.Domain.Commons;

namespace WeatherForecast.Core.Domain.WeatherAggregate.ValueObjects;

public class Temperature : Value<Temperature>
{
    public int Value { get; private set; }
    public Temperature(int value)
    {
        Value = value;
    }
    public static Temperature Create(int value)
    {
        if (value is > 60 or < -60)
            throw new WeatherExceptions.InvalidEntityState($"Temperature cannot be more than plus 60 and less than minus 60 degrees");
        return new Temperature(value);
    }

    public static implicit operator int(Temperature value) => value.Value;
}

