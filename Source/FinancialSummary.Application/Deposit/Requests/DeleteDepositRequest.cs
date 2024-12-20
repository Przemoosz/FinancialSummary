namespace FinancialSummary.Application.Deposit.Requests;

using System.Diagnostics.CodeAnalysis;
using MediatR;
using Result;

[ExcludeFromCodeCoverage]
public sealed record DeleteDepositRequest(Guid Id) : IRequest<OperationResult>;
