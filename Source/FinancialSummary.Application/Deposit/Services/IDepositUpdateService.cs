namespace FinancialSummary.Application.Deposit.Services;

using Domain.Entities.Deposit;

internal interface IDepositUpdateService
{
	public Task UpdateAsync(Guid id, UpdateDepositEntity updateDepositEntity, CancellationToken cancellationToken);
}