using FluentAssertions;
using Moq;
using WeatherForecast.Core.Domain.Commons;
using WeatherForecast.Core.Domain.WeatherAggregate;
using WeatherForecast.Core.Domain.WeatherAggregate.ValueObjects;
using Xunit;

namespace Test.WeatherForecast.Domain;

public class AdditionalFields_Validation_Spec
{
    private readonly Mock<IDateTimeProvider> _dateTimeProviderMock;

    public AdditionalFields_Validation_Spec()
    {
        _dateTimeProviderMock = new Mock<IDateTimeProvider>();
    }

    [Fact]
    public void Temperature_Should_Be_Within_Range()
    {
        var temperature = Temperature.Create(10);
        temperature.Value.Should().Be(10);
    }

    [Fact]
    public void Temperature_Should_Be_Within_Range_With_Exception()
    {
        Assert.Throws<WeatherExceptions.InvalidEntityState>(() => Temperature.Create(100));
    }

    [Fact]
    public void Timestamp_Should_Be_In_The_Future()
    {
        _dateTimeProviderMock.Setup(m => m.UtcNow).Returns(new DateTime(2023, 06, 21));

        var timestamp = Timestamp.Create(DateTime.Now, _dateTimeProviderMock.Object);
        timestamp.Value.Should().Be(DateOnly.FromDateTime(DateTime.Now));
    }
    
    [Fact]
    public void Timestamp_Should_Be_In_The_Future_With_Exception()
    {
        _dateTimeProviderMock.Setup(m => m.UtcNow).Returns(new DateTime(2024, 06, 21));

        Assert.Throws<WeatherExceptions.InvalidEntityState>(() => Timestamp.Create(DateTime.Now.AddDays(-1), _dateTimeProviderMock.Object));
    }

    

}
