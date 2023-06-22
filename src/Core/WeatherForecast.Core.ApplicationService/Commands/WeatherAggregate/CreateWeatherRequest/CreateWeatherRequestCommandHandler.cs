using MediatR;
using WeatherForecast.Core.Domain.Commons;
using WeatherForecast.Core.Domain.WeatherAggregate;
using WeatherForecast.Core.Domain.WeatherAggregate.Contracts;
using static WeatherForecast.Core.Domain.WeatherAggregate.WeatherCommand;

namespace WeatherForecast.Core.ApplicationService.Commands.WeatherAggregate.CreateWeatherRequest;
public class CreateWeatherRequestCommandHandler : IRequestHandler<CreateWeatherRequestCommand, CreateWeatherDto>
{
    private readonly IWeatherRepository _weatherRepository;
    private readonly IDateTimeProvider _dateTimeProvider;
    private readonly IUnitOfWork _unitOfWork;

    public CreateWeatherRequestCommandHandler(IWeatherRepository weatherRepository,
        IDateTimeProvider dateTimeProvider,
        IUnitOfWork unitOfWork)
    {
        _weatherRepository = weatherRepository;
        _dateTimeProvider = dateTimeProvider;
        _unitOfWork = unitOfWork;
    }

    public async Task<CreateWeatherDto> Handle(CreateWeatherRequestCommand request, CancellationToken cancellationToken)
    {
        var weather = Weather.Create(new CreateWeatherCommand
        {
            Id = Guid.NewGuid(),
            Temperature = request.Temperature,
            Timestamp = request.Timestamp,
            DateTimeProvider = _dateTimeProvider
        });
        
        await _weatherRepository.CreateAsync(weather, cancellationToken);
        await _unitOfWork.SaveAsync(cancellationToken);

        return new CreateWeatherDto
        {
            Id = weather.Id,
            Temperature = weather.Temperature,
            Timestamp = weather.Timestamp
        };
    }
}

