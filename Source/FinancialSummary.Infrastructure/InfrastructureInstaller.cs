namespace FinancialSummary.Infrastructure;

using System.Diagnostics.CodeAnalysis;
using Application.Contracts.Repository;
using Domain.Entities;
using Microsoft.Extensions.DependencyInjection;
using Repository;

[ExcludeFromCodeCoverage]
public static class InfrastructureInstaller
{
	public static void InstallInfrastructure(this IServiceCollection serviceCollection)
	{
		serviceCollection.AddScoped<IRepository<DepositEntity>, DepositRepository>();
	}
}