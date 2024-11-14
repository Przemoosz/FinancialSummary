namespace FinancialSummary.Infrastructure.Repository;

using Abstract.DatabaseContext;
using Application.Contracts.Repository;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

internal class DepositRepository: IRepository<DepositEntity>
{
	private readonly IDepositContext _depositContext;

	public DepositRepository(IDepositContext depositContext)
	{
		_depositContext = depositContext;
	}
	
	public Task<DepositEntity> GetByIdAsync(Guid id, CancellationToken cancellationToken)
	{
		return _depositContext.Deposits.FirstOrDefaultAsync(s => s.Id.Equals(id), cancellationToken);
	}

	public Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken)
	{
		return _depositContext.Deposits.AnyAsync(x => x.Id.Equals(id), cancellationToken);
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

	public Task DeleteAsync(Guid id, CancellationToken cancellationToken)
	{
		return _depositContext.Deposits.Where(s => s.Id.Equals(id)).ExecuteDeleteAsync(cancellationToken);
	}

	public ValueTask DisposeAsync()
	{
		return _depositContext.DisposeAsync();
	}
}