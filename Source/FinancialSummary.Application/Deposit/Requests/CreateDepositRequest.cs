namespace FinancialSummary.Application.Deposit.Requests;

using System.Diagnostics.CodeAnalysis;
using Domain.Entities.Deposit;
using Result;
using MediatR;

[ExcludeFromCodeCoverage]
public sealed record CreateDepositRequest(
	string Name,
	decimal Cash,
	decimal InterestRate,
	int CapitalizationPerYear,
	DateTime StartDate,
	DateTime FinishDate) : IRequest<OperationResult>;