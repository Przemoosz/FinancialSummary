namespace FinancialSummary.Application.Deposit.Requests;

using MediatR;
using Result;

public sealed record DeleteDepositRequest(Guid Id) : IRequest<OperationResult>;
