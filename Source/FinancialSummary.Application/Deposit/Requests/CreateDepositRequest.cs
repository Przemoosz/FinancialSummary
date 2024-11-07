namespace FinancialSummary.Application.Deposit.Requests;

using FinancialSummary.Application.Result;
using MediatR;

public sealed record CreateDepositRequest(string Name,
	double Cash,
	double InterestRate,
	int CapitalizationPerYear,
	DateTime StartDate,
	DateTime FinishDate) : IRequest<OperationResult>;
	
	
	