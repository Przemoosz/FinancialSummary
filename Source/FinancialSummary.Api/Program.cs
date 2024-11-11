namespace FinancialSummary.Api;

using Application;
using Domain.Entities;
using Infrastructure.Abstract.DatabaseContext;
using Infrastructure.DatabaseContext;
using MediatR;
using Microsoft.EntityFrameworkCore;

public class Program
{
    public static async Task Main(string[] args)
    {
        WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

        builder.Services.ConfigureMiddleware();
        builder.Services.ConfigureServices();
        builder.Services.ConfigureDatabase(builder.Configuration);
        builder.Services.AddControllers();
        WebApplication app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
          //  await ApplyMigrations(app);
        }
        //await ApplyMigrations(app);
        app.UseHttpsRedirection();

        app.MapControllers();
        
        app.UseAuthorization();

        
        
        
        string[] summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        app.MapGet("/weatherforecast", (HttpContext httpContext, DepositContext depositContext) =>
            {
                DepositEntity depositEntity = new DepositEntity("ds", 1, 1, 1, DateTime.Now, DateTime.Now);
                
                depositContext.Deposits.Add(depositEntity);
                depositContext.SaveChanges();
            
            WeatherForecast[] forecast =  Enumerable.Range(1, 5).Select(index =>
                new WeatherForecast
                {
                    Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                    TemperatureC = Random.Shared.Next(-20, 55),
                    Summary = summaries[Random.Shared.Next(summaries.Length)]
                })
                .ToArray();
            return forecast;
        })
        .WithName("GetWeatherForecast")
        .WithOpenApi();

        await app.RunAsync();
    }

    private static async Task ApplyMigrations(WebApplication app)
    {
        using IServiceScope scope = app.Services.CreateScope();

        await using DepositContext context = scope.ServiceProvider.GetRequiredService<DepositContext>();

        await context.Database.MigrateAsync();
    }
}
