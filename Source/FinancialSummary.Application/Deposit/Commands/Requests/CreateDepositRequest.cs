namespace FinancialSummary.Application.Deposit.Commands.Requests;

using MediatR;
using Result;

public sealed record CreateDepositRequest(string Name,
	double Cash,
	double InterestRate,
	int CapitalizationPerYear,
	DateTime StartDate,
	DateTime FinishDate) : IRequest<OperationResult>;
	
	
	