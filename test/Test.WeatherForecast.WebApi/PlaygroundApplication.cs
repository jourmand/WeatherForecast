using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WeatherForecast.Infrastructures.Data.Commons;

namespace Test.WeatherForecast.WebApi;

internal class PlaygroundApplication : WebApplicationFactory<Program>
{
    private readonly string _environment;

    public PlaygroundApplication(string environment = "Development")
    {
        _environment = environment;
    }

    protected override IHost CreateHost(IHostBuilder builder)
    {
        builder.UseEnvironment(_environment);

        builder.ConfigureServices(services =>
        {
            services.AddScoped(sp =>
            {
                return new DbContextOptionsBuilder<WeatherForecastDbContext>()
                .UseInMemoryDatabase("WeatherForecastTest")
                .UseApplicationServiceProvider(sp)
                .Options;
            });
        });

        return base.CreateHost(builder);
    }
}
