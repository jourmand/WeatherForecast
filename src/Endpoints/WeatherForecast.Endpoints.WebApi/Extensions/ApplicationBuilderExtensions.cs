using Microsoft.EntityFrameworkCore;
using WeatherForecast.Endpoints.WebApi.Extensions.Middleware;
using WeatherForecast.Infrastructures.Data.Commons;

namespace WeatherForecast.Endpoints.WebApi.Extensions;

public static class ApplicationBuilderExtensions
{
    public static void CustomExceptionMiddleware(this IApplicationBuilder app) =>
            app.UseExceptionHandler(err => err.UseCustomErrors());

    public static IApplicationBuilder UseCustomizedSwagger(this IApplicationBuilder app)
    {
        app.UseSwagger()
            .UseSwaggerUI();
        return app;
    }

    public static async Task EnsureDb(this IServiceProvider service)
    {
        using var db = service.CreateScope().ServiceProvider.GetRequiredService<WeatherForecastDbContext>();
        if (db.Database.IsRelational())
        {
            await db.Database.MigrateAsync();
        }

    }
}
