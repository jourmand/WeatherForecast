using WeatherForecast.Core.Domain.Commons;

namespace WeatherForecast.Infrastructures.Data.Commons;

public class UnitOfWork : IUnitOfWork
{
    private readonly WeatherForecastDbContext _dbContext;

    public UnitOfWork(WeatherForecastDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task SaveAsync(CancellationToken cancellationToken = default)
    {
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
    public void Save()
    {
        _dbContext.SaveChanges();
    }
}