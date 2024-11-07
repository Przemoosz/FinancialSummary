namespace FinancialSummary.Application.Deposit.Commands.Queries;

using Abstraction.Queries;
using Domain.Entities;

public sealed record GetDepositGetByIdQuery(Guid Id) : GetByIdQuery<DepositEntity>(Id);
