using Serilog;

namespace WeatherForecast.Endpoints.WebApi.Extensions;

public static class HostBuilderExtensions
{
    public static ConfigureHostBuilder CustomizeUseSerilog(this ConfigureHostBuilder host)
    {
        host.UseSerilog((hostingContext, loggerConfiguration) =>
        {
            loggerConfiguration
                .ReadFrom.Configuration(hostingContext.Configuration)
                .Enrich.FromLogContext()
                .Enrich.WithProperty("ApplicationName", "WeatherForecast")
                .Enrich.WithProperty("Environment", hostingContext.HostingEnvironment);
        });
        return host;
    }
}
