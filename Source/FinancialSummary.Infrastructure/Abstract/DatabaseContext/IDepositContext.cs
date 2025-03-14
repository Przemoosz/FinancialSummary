namespace FinancialSummary.Infrastructure.Abstract.DatabaseContext;

using Domain.Entities;
using Domain.Entities.Deposit;
using Microsoft.EntityFrameworkCore;

public interface IDepositContext: IAsyncDisposable
{
	DbSet<DepositEntity> Deposits { get; }
	Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}