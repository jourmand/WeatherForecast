using System.Net;
using System.Net.Http.Headers;
using System.Text.Json;
using WeatherForecast.Core.ApplicationService.Commands.WeatherAggregate.CreateWeatherRequest;
using Xunit;
using Xunit.Abstractions;

namespace Test.WeatherForecast.WebApi;

public class ApiControllerTests
{
    private readonly ITestOutputHelper _outputHelper;

    public ApiControllerTests(ITestOutputHelper outputHelper)
    {
        _outputHelper = outputHelper;
    }

    [Fact]
    public async Task POST_JsonRaw_Responds_OK()
    {
        await using var application = new PlaygroundApplication();

        var jsonString = JsonSerializer.Serialize(new CreateWeatherRequestCommand
        {
            Temperature = 10,
            Timestamp = DateTime.Now
        });
        using var jsonContent = new StringContent(jsonString);
        jsonContent.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json");

        using var client = application.CreateClient();
        using var response = await client.PostAsync("/weatherforecast", jsonContent);

        _outputHelper.WriteLine(await response.Content.ReadAsStringAsync());
        Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        Assert.Equal(10,
            JsonSerializer.Deserialize<CreateWeatherDto>(await response.Content.ReadAsStringAsync(),
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true })?.Temperature);
    }

    [Fact]
    public async Task POST_JsonRaw_Responds_Timestamp_BadRequest()
    {
        await using var application = new PlaygroundApplication();

        var jsonString = JsonSerializer.Serialize(new CreateWeatherRequestCommand
        {
            Temperature = 10,
            Timestamp = DateTime.Now.AddDays(-1)
        });
        using var jsonContent = new StringContent(jsonString);
        jsonContent.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json");

        using var client = application.CreateClient();
        using var response = await client.PostAsync("/weatherforecast", jsonContent);

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        _outputHelper.WriteLine(await response.Content.ReadAsStringAsync());
        Assert.Contains("Forecast cannot be in the past",
                                  await response.Content.ReadAsStringAsync());
    }

    [Fact]
    public async Task POST_JsonRaw_Responds_Temperature_BadRequest()
    {
        await using var application = new PlaygroundApplication();

        var jsonString = JsonSerializer.Serialize(new CreateWeatherRequestCommand
        {
            Temperature = 100,
            Timestamp = DateTime.Now
        });
        using var jsonContent = new StringContent(jsonString);
        jsonContent.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json");

        using var client = application.CreateClient();
        using var response = await client.PostAsync("/weatherforecast", jsonContent);

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        _outputHelper.WriteLine(await response.Content.ReadAsStringAsync());
        Assert.Contains("Temperature cannot be more than plus 60 and less than minus 60 degrees",
                       await response.Content.ReadAsStringAsync());
    }

    [Fact]
    public async Task GET_WeatherForecast_Responds_OK()
    {
        await using var application = new PlaygroundApplication();

        using var client = application.CreateClient();
        using var response = await client.GetAsync("/weatherforecast");

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        _outputHelper.WriteLine(await response.Content.ReadAsStringAsync());
    }
}
