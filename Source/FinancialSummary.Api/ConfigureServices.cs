namespace FinancialSummary.Api;

using Application;
using Domain;
using Infrastructure;
using Infrastructure.Abstract.DatabaseContext;
using Infrastructure.DatabaseContext;
using Microsoft.EntityFrameworkCore;
using Presentation;
using Shared;

internal static class StartupConfiguration
{
	public static void ConfigureServices(this IServiceCollection serviceCollection)
	{
		serviceCollection.InstallApplication();
		serviceCollection.InstallDomain();
		serviceCollection.InstallInfrastructure();
		serviceCollection.InstallPresentation();
		serviceCollection.InstallShared();
	}
	
	public static void ConfigureMiddleware(this IServiceCollection serviceCollection)
	{
		serviceCollection.AddAuthorization();
		serviceCollection.AddEndpointsApiExplorer();
		serviceCollection.AddSwaggerGen();
	}

	public static void ConfigureDatabase(this IServiceCollection serviceCollection,
		ConfigurationManager configurationManager)
	{
		serviceCollection.AddDbContext<IDepositContext, DepositContext>(options =>
		{
			options.UseNpgsql(configurationManager.GetConnectionString("FinancialSummaryDatabase"),  b => b.MigrationsAssembly("FinancialSummary.Api"));
		});
		
		serviceCollection.AddDbContext<IBondsContext, BondsContext>(options =>
		{
			options.UseNpgsql(configurationManager.GetConnectionString("FinancialSummaryDatabase"),  b => b.MigrationsAssembly("FinancialSummary.Api"));
		});
	}
}