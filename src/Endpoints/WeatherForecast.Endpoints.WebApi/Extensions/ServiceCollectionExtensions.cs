using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Reflection;
using WeatherForecast.Core.ApplicationService.Commands.WeatherAggregate.CreateWeatherRequest;
using WeatherForecast.Core.Domain.Commons;
using WeatherForecast.Infrastructures.Data.Commons;

namespace WeatherForecast.Endpoints.WebApi.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddServiceRegistry(this IServiceCollection services, IConfiguration configuration)
    {

        services.AddScoped<IUnitOfWork, UnitOfWork>();
        //services.AddScoped<IWeatherRepository, WeatherRepository>();
        services.AddScoped<IDateTimeProvider, DateTimeProvider>();

        services.Scan(s => s.FromAssemblies(Assembly.Load(typeof(WeatherForecastDbContext).GetTypeInfo().Assembly.GetName().Name),
                Assembly.Load(typeof(CreateWeatherRequestCommand).GetTypeInfo().Assembly.GetName().Name),
                Assembly.Load(typeof(IUnitOfWork).GetTypeInfo().Assembly.GetName().Name),
                Assembly.Load(typeof(UnitOfWork).GetTypeInfo().Assembly.GetName().Name))
            .AddClasses(c => c.Where(type => type.Name.EndsWith("Repository")))
            .AsImplementedInterfaces()
            .WithScopedLifetime());

        return services;
    }

    public static IServiceCollection ConfigMediatR(this IServiceCollection services)
    {
        services.AddMediatR(x => x.RegisterServicesFromAssemblies(Assembly.Load(typeof(CreateWeatherRequestCommand).GetTypeInfo().Assembly.GetName().Name),
                Assembly.Load(typeof(WeatherForecastDbContext).GetTypeInfo().Assembly.GetName().Name)));

        return services;
    }

    public static IServiceCollection ConfigApiBehavior(this IServiceCollection services)
    {
        services.Configure<ApiBehaviorOptions>(options =>
        {
            options.InvalidModelStateResponseFactory = actionContext =>
                                new BadRequestObjectResult(actionContext.ModelState);
        });
        return services;
    }

    public static IServiceCollection ConfigSwagger(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddEndpointsApiExplorer();

        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "Weather Forecast HTTP API",
                Version = "v1.0",
                Description = "The Weather Forecast Service HTTP API",
            });

        });
        return services;
    }

    public static IServiceCollection AddCustomizedDataStore(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<WeatherForecastDbContext>(options => options.UseSqlite(configuration.GetConnectionString("DatabaseConnection"),
            b => b.MigrationsAssembly(typeof(Program).GetTypeInfo().Assembly.GetName().Name)));
        return services;
    }
}
