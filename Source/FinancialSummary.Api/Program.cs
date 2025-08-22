namespace FinancialSummary.Api;

using Infrastructure.DatabaseContext;
using Microsoft.EntityFrameworkCore;

public class Program
{
    public static async Task Main(string[] args)
    {
        WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

        builder.Services.ConfigureMiddleware();
        builder.Services.ConfigureServices();
        builder.Services.AddHttpClient();
        builder.Services.ConfigureDatabase(builder.Configuration);
        builder.Services.AddControllers()
            .AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.Converters.Add(new System.Text.Json.Serialization.JsonStringEnumConverter());
            });;
        WebApplication app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
            // await ApplyMigrations(app);
        }
        await ApplyMigrations(app);
        app.UseHttpsRedirection();

        app.MapControllers();
        
        app.UseAuthorization();

        await app.RunAsync();
    }

    private static async Task ApplyMigrations(WebApplication app)
    {
        using IServiceScope scope = app.Services.CreateScope();
        
        await using DatabaseContext databaseContext = scope.ServiceProvider.GetRequiredService<DatabaseContext>();

        await databaseContext.Database.MigrateAsync();
    }
}
