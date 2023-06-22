using MediatR;
using System.ComponentModel.DataAnnotations;

namespace WeatherForecast.Core.ApplicationService.Commands.WeatherAggregate.CreateWeatherRequest;
public class CreateWeatherRequestCommand : IRequest<CreateWeatherDto>
{
    [Required]
    [Range(-60, 60, ErrorMessage = "Temperature cannot be more that +60 and less than -60 degrees")]
    public int Temperature { get; set; }

    [Required]
    public DateTime Timestamp { get; set; }
}

