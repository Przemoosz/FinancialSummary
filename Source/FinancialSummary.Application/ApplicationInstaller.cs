namespace FinancialSummary.Application;

using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using Abstraction.Deposit.Services;
using BondTypes.Behaviors;
using BondTypes.Requests;
using BondTypes.Validators;
using Deposit.Behaviors;
using Deposit.Requests;
using Deposit.Services;
using Deposit.Validators;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Result;

[ExcludeFromCodeCoverage]
public static class ApplicationInstaller
{
    public static void InstallApplication(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
        serviceCollection.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        RegisterDepositServices(serviceCollection);
    }

    private static void RegisterBondTypesServices(IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<IValidator<CreateBondTypeRequest>, CreateBondTypeValidator>();
        serviceCollection.AddScoped<IPipelineBehavior<CreateBondTypeRequest, OperationResult>, CreateBondTypeBehavior>();
    }

    private static void RegisterDepositServices(IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<IValidator<CreateDepositRequest>, DepositCreateRequestValidator>();
        serviceCollection.AddScoped<IValidator<DeleteDepositRequest>, DepositDeleteRequestValidator>();
        serviceCollection.AddScoped<IValidator<UpdateDepositRequest>, DepositUpdateRequestValidator>();
        serviceCollection.AddScoped<IPipelineBehavior<CreateDepositRequest, OperationResult>, CreateDepositBehavior>();
        serviceCollection.AddScoped<IPipelineBehavior<DeleteDepositRequest, OperationResult>, DeleteDepositBehavior>();
        serviceCollection.AddScoped<IPipelineBehavior<UpdateDepositRequest, OperationResult>, UpdateDepositBehavior>();
        serviceCollection.AddScoped<IDepositUpdateService, DepositUpdateService>();
    }
}