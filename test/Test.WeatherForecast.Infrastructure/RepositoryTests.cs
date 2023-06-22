using FluentAssertions;
using Test.WeatherForecast.Infrastructure.Infrastructure;
using WeatherForecast.Core.Domain.Commons;
using WeatherForecast.Core.Domain.WeatherAggregate;
using WeatherForecast.Infrastructures.Data.WeatherAggregate.Repositories;
using Xunit;

namespace Test.WeatherForecast.Infrastructure;

public class RepositoryTests : DatabaseTestBase
{
    private readonly WeatherRepository _testWeatherRepository;

    public RepositoryTests()
    {
        _testWeatherRepository = new WeatherRepository(Context);
    }

    [Fact]
    public void Add_New_Weather()
    {
        var item = Weather.Create(new WeatherCommand.CreateWeatherCommand
        {
            DateTimeProvider = new DateTimeProvider(),
            Timestamp = DateTime.Now,
            Temperature = 10,
            Id = Guid.NewGuid()
        });
        var result = Context.WeatherData
            .Add(item);

        result.Entity.Should().BeOfType<Weather>();
        result.Entity.Timestamp.Should().Be(item.Timestamp);
    }

    [Theory]
    [InlineData("654b7573-9501-436a-ad36-94c5696ac28f")]
    [InlineData("971316e1-4966-4426-b1ea-a36c9dde1066")]
    public async void Weather_Item_Should_Available(string id)
    {
        var result = await _testWeatherRepository.GetAsync(Guid.Parse(id));

        result.Should().BeOfType<Weather>();
        result.Id.Should().Be(result.Id);
    }
}
