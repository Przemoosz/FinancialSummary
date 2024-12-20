namespace FinancialSummary.Application.Deposit.Queries;

using System.Diagnostics.CodeAnalysis;
using FinancialSummary.Application.Abstraction.Queries;
using Domain.Entities;
using MediatR;
using Result;

[ExcludeFromCodeCoverage]
public sealed record GetDepositGetByIdQuery(Guid Id) : IRequest<OperationResult>;
