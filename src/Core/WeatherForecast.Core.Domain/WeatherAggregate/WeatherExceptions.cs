using WeatherForecast.Core.Domain.Commons;

namespace WeatherForecast.Core.Domain.WeatherAggregate;
public static class WeatherExceptions
{
    public class InvalidEntityState : Exception
    {
        public IEnumerable<Error> Errors { get; set; }

        public InvalidEntityState(string error) =>
            Errors = new[] { new Error(error) };
    }
}
