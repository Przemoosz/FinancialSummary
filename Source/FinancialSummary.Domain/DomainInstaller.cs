namespace FinancialSummary.Domain;

using Abstract.Factories;
using Factories;
using Microsoft.Extensions.DependencyInjection;

public static class DomainInstaller
{
	public static void InstallDomain(this IServiceCollection serviceCollection)
	{
		serviceCollection.AddTransient<IDepositEntityFactory, DepositEntityFactory>();
	}
}