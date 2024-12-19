namespace FinancialSummary.Application.Deposit.Requests;

using System.Diagnostics.CodeAnalysis;
using MediatR;
using Result;

[ExcludeFromCodeCoverage]
public sealed record UpdateDepositRequest(Guid Id, 
	string Name,
	decimal? Cash,
	decimal? InterestRate,
	int? CapitalizationPerYear) : IRequest<OperationResult>;