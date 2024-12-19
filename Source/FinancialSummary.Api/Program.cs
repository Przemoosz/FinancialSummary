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
            // await ApplyMigrations(app);
        }
        // await ApplyMigrations(app);
        app.UseHttpsRedirection();

        app.MapControllers();
        
        app.UseAuthorization();

        await app.RunAsync();
    }

    private static async Task ApplyMigrations(WebApplication app)
    {
        using IServiceScope scope = app.Services.CreateScope();

        await using DepositContext context = scope.ServiceProvider.GetRequiredService<DepositContext>();

        await context.Database.MigrateAsync();
    }
}
