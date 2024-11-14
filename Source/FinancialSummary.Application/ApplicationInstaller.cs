namespace FinancialSummary.Application;

using System.Reflection;
using Deposit.Behaviours;
using Deposit.Requests;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Result;

public static class ApplicationInstaller
{
    public static void InstallApplication(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
        serviceCollection.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

        serviceCollection.AddScoped<IPipelineBehavior<CreateDepositRequest, OperationResult>, CreateDepositBehavior>();
        serviceCollection.AddScoped<IPipelineBehavior<DeleteDepositRequest, OperationResult>, DeleteDepositBehavior>();
    }
}