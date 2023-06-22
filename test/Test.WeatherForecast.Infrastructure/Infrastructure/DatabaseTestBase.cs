using Microsoft.EntityFrameworkCore;
using WeatherForecast.Infrastructures.Data.Commons;

namespace Test.WeatherForecast.Infrastructure.Infrastructure;

public class DatabaseTestBase : IDisposable
{
    protected readonly WeatherForecastDbContext Context;

    public DatabaseTestBase()
    {
        var options = new DbContextOptionsBuilder<WeatherForecastDbContext>().UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;

        Context = new WeatherForecastDbContext(options);

        Context.Database.EnsureCreated();

        DatabaseInitializer.Initialize(Context);
    }

    public void Dispose()
    {
        Context.Database.EnsureDeleted();
        Context.Dispose();
    }
}
