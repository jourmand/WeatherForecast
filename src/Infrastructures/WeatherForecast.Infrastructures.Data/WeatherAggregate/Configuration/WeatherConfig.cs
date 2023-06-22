using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using WeatherForecast.Core.Domain.WeatherAggregate;

namespace WeatherForecast.Infrastructures.Data.WeatherAggregate.Configuration;

public class WeatherConfig : IEntityTypeConfiguration<Weather>
{
    public void Configure(EntityTypeBuilder<Weather> builder)
    {
        builder.ToTable("WeatherData");
        builder.HasKey(c => c.Id);
        builder.Property(c => c.Id)
            .ValueGeneratedNever();

        builder.OwnsOne(c => c.Temperature, d =>
        {
            d.Property(e => e.Value)
            .IsRequired()
            .HasColumnName("Temperature");
        });

        builder.OwnsOne(c => c.Timestamp, d =>
        {
            d.Property(e => e.Value)
            .IsRequired()
            .HasColumnName("Timestamp");
        });
    }
}

