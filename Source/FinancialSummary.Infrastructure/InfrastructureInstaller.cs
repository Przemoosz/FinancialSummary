namespace FinancialSummary.Infrastructure;

using System.Diagnostics.CodeAnalysis;
using Abstract.DatabaseContext;
using Application.Contracts.Repository;
using DatabaseContext;
using Domain.Abstract.Entities;
using Domain.Entities;
using Domain.Entities.Deposit;
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