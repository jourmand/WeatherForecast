using Microsoft.EntityFrameworkCore;
using WeatherForecast.Core.Domain.WeatherAggregate;

namespace WeatherForecast.Infrastructures.Data.Commons;
public class WeatherForecastDbContext : DbContext
{
    public WeatherForecastDbContext(DbContextOptions<WeatherForecastDbContext> options) : base(options)
    {
    }

    public DbSet<Weather> WeatherData { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.ApplyConfigurationsFromAssembly(GetType().Assembly);
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        var changeCount = base.SaveChangesAsync(cancellationToken);
        return changeCount;
    }
}

