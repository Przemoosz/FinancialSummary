namespace FinancialSummary.Application;

using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

public static class ApplicationInstaller
{
    public static void InstallApplication(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
    }
}