namespace FinancialSummary.Infrastructure.Repository;

using System.Diagnostics.CodeAnalysis;
using Abstract.DatabaseContext;
using Application.Contracts.Repository;
using Domain.Entities.Deposit;
using Microsoft.EntityFrameworkCore;

[ExcludeFromCodeCoverage]
internal class DepositRepository: IRepository<Guid, DepositEntity>
{
	private readonly IDepositContext _depositContext;

	public DepositRepository(IDepositContext depositContext)
	{
		_depositContext = depositContext;
	}
	
	public Task<DepositEntity> GetByKeyAsync(Guid key, CancellationToken cancellationToken)
	{
		return _depositContext.Deposits.FirstOrDefaultAsync(s => s.Id.Equals(key), cancellationToken);
	}

	public Task<bool> ExistsAsync(Guid key, CancellationToken cancellationToken)
	{
		return _depositContext.Deposits.AnyAsync(x => x.Id.Equals(key), cancellationToken);
	}

	public IAsyncEnumerable<DepositEntity> GetAll(CancellationToken cancellationToken)
	{
		return _depositContext.Deposits.AsAsyncEnumerable();
	}

	public async Task AddAsync(DepositEntity depositEntity, CancellationToken cancellationToken)
	{
		await _depositContext.Deposits.AddAsync(depositEntity, cancellationToken);
		await _depositContext.SaveChangesAsync(cancellationToken);
	}

	public Task DeleteAsync(Guid key, CancellationToken cancellationToken)
	{
		return _depositContext.Deposits.Where(s => s.Id.Equals(key)).ExecuteDeleteAsync(cancellationToken);
	}
	
	public async Task UpdateAsync(CancellationToken cancellationToken)
	{
		await _depositContext.SaveChangesAsync(cancellationToken);
	}
	
	public ValueTask DisposeAsync()
	{
		return _depositContext.DisposeAsync();
	}
}