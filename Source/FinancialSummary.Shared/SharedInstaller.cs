namespace FinancialSummary.Shared;

using System.Diagnostics.CodeAnalysis;
using Abstraction.Wrappers;
using Microsoft.Extensions.DependencyInjection;
using Wrappers;

[ExcludeFromCodeCoverage]
public static class SharedInstaller
{
	public static void InstallShared(this IServiceCollection serviceCollection)
	{
		serviceCollection.AddTransient<IDateTimeWrapper, DateTimeWrapper>();
	}
}