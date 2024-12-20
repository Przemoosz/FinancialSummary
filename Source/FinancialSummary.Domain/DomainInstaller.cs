namespace FinancialSummary.Domain;

using System.Diagnostics.CodeAnalysis;
using Abstract.Factories;
using Factories;
using Microsoft.Extensions.DependencyInjection;

[ExcludeFromCodeCoverage]
public static class DomainInstaller
{
	public static void InstallDomain(this IServiceCollection serviceCollection)
	{
		serviceCollection.AddTransient<IDepositEntityFactory, DepositEntityFactory>();
	}
}