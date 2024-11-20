namespace FinancialSummary.Presentation;

using System.Diagnostics.CodeAnalysis;
using Abstraction.Factories;
using Factories;
using Microsoft.Extensions.DependencyInjection;

[ExcludeFromCodeCoverage]
public static class PresentationInstaller
{
	public static void InstallPresentation(this IServiceCollection serviceCollection)
	{
		serviceCollection.AddTransient<IProblemDetailsFactory, ProblemDetailsFactory>();
	}
}