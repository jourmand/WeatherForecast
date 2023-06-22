using MediatR;
using WeatherForecast.Core.ApplicationService.Commands.WeatherAggregate.CreateWeatherRequest;
using WeatherForecast.Endpoints.WebApi.Extensions;
using WeatherForecast.Infrastructures.Data.Queries.GetWeeklyWeatherForecast;

var builder = WebApplication.CreateBuilder(args);

builder.Host
    .CustomizeUseSerilog();

builder.Services
    .AddServiceRegistry(builder.Configuration)
    .ConfigApiBehavior()
    .AddCustomizedDataStore(builder.Configuration)
    .ConfigMediatR()
    .ConfigSwagger(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
    app.UseCustomizedSwagger();

await app.Services.EnsureDb();
app.CustomExceptionMiddleware();
app.UseHttpsRedirection();

app.MapPost("/weatherforecast", async Task<IResult> (CreateWeatherRequestCommand model, IMediator mediator) =>
{
    var result = await mediator.Send(model);
    return Results.Created($"/weatherforecast/{result.Id}", result);
})
.WithName("PostWeatherForecast");


app.MapGet("/weatherforecast", async Task<string> (IMediator mediator) =>
{
    return await mediator.Send(new GetWeeklyWeatherForecastQuery());
})
.WithName("GetWeatherForecast");

app.Run();

public partial class Program { }