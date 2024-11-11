namespace FinancialSummary.Presentation;

using Abstraction.Factories;
using Factories;
using Microsoft.Extensions.DependencyInjection;

public static class PresentationInstaller
{
	public static void InstallPresentation(this IServiceCollection serviceCollection)
	{
		serviceCollection.AddTransient<IProblemDetailsFactory, ProblemDetailsFactory>();
	}
}