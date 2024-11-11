namespace FinancialSummary.Application.Deposit.Requests;

using FinancialSummary.Application.Result;
using MediatR;

public sealed record CreateDepositRequest(string Name,
	decimal Cash,
	decimal InterestRate,
	int CapitalizationPerYear,
	DateTime StartDate,
	DateTime FinishDate) : IRequest<OperationResult>;
	
	
	