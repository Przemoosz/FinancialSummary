namespace FinancialSummary.Infrastructure;

using Application.Contracts.Repository;
using Microsoft.Extensions.DependencyInjection;
using Repository;

public static class InfrastructureInstaller
{
	public static void InstallInfrastructure(this IServiceCollection serviceCollection)
	{
		serviceCollection.AddScoped<IDepositRepository, DepositRepository>();
	}
}