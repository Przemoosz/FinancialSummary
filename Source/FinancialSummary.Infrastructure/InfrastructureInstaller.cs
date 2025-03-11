namespace FinancialSummary.Infrastructure;

using System.Diagnostics.CodeAnalysis;
using Abstract.DatabaseContext;
using Application.Contracts.Providers.Cpi;
using Application.Contracts.Repository;
using Cpi;
using DatabaseContext;
using Domain.Abstract.Entities;
using Domain.Entities;
using Domain.Entities.Bonds.AntiInflationary;
using Domain.Entities.Bonds.FixedInterest;
using Domain.Entities.Bonds.FloatingInterest;
using Domain.Entities.Deposit;
using Microsoft.Extensions.DependencyInjection;
using Repository;

[ExcludeFromCodeCoverage]
public static class InfrastructureInstaller
{
	public static void InstallInfrastructure(this IServiceCollection serviceCollection)
	{
		serviceCollection.AddMemoryCache();
		serviceCollection.AddSingleton<ICpiProvider, CpiProvider>();
		serviceCollection.AddSingleton<ICpiClient, CpiClient>();
		serviceCollection.AddScoped<IRepository<Guid, DepositEntity>, DepositRepository>();
		serviceCollection.AddScoped<IRepository<string, TwoYearsFloatingInterestBondType>, BondTypesRepository<TwoYearsFloatingInterestBondType>>();
		serviceCollection.AddScoped<IRepository<string, OneYearFloatingInterestBondType>, BondTypesRepository<OneYearFloatingInterestBondType>>();
		serviceCollection.AddScoped<IRepository<string, FourYearsAntiInflationaryBondType>, BondTypesRepository<FourYearsAntiInflationaryBondType>>();
		serviceCollection.AddScoped<IRepository<string, ThreeYearsFixedInterestBondType>, BondTypesRepository<ThreeYearsFixedInterestBondType>>();
		serviceCollection.AddScoped<IRepository<string, TenYearsAntiInflationaryBondType>, BondTypesRepository<TenYearsAntiInflationaryBondType>>();
	}
}