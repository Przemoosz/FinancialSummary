namespace FinancialSummary.Api;

using Domain.Entities;
using Infrastructure.DatabaseContext;
using Microsoft.EntityFrameworkCore;

public class Program
{
    public static async Task Main(string[] args)
    {
        WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

        builder.Services.ConfigureMiddleware();
        builder.Services.ConfigureServices();
        Console.WriteLine(builder.Configuration.GetConnectionString("FinancialSummaryDatabase"));
        builder.Services.AddDbContext<IDepositContext, DepositContext>(options =>
        {
            options.UseNpgsql(builder.Configuration.GetConnectionString("FinancialSummaryDatabase"));
        });
        
        
        WebApplication app = builder.Build();
        
        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
            await ApplyMigrations(app);
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();

        string[] summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        app.MapGet("/weatherforecast", (HttpContext httpContext, DepositContext depositContext) =>
            {

                DepositEntity depositEntity = new DepositEntity()
                {
                    CreationDate = DateTime.Now,
                    StartDate = DateTime.Now,
                    FinishDate = DateTime.Now,
                    Id = Guid.NewGuid(),
                    CapitalizationPerYear = 1,
                    Cash = 100,
                    InterestRate = 1,
                    Name = "dsds"
                };
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
