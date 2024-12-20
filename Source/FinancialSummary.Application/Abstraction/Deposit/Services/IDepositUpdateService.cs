namespace FinancialSummary.Application.Abstraction.Deposit.Services;

using FinancialSummary.Domain.Entities.Deposit;

internal interface IDepositUpdateService
{
	public Task UpdateAsync(Guid id, UpdateDepositEntity updateDepositEntity, CancellationToken cancellationToken);
}