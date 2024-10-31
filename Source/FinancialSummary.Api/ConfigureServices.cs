namespace FinancialSummary.Api;

using Application;
using Domain;
using Infrastructure;
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
}