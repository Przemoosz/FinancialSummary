namespace FinancialSummary.Application.Deposit.Queries;

using FinancialSummary.Application.Abstraction.Queries;
using FinancialSummary.Domain.Entities;

public sealed record GetDepositGetByIdQuery(Guid Id) : GetByIdQuery<DepositEntity>(Id);
