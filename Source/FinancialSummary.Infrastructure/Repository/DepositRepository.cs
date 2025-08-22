namespace FinancialSummary.Infrastructure.Repository;

using System.Diagnostics.CodeAnalysis;
using Abstract.DatabaseContext;
using Application.Contracts.Repository;
using Domain.Entities.Deposit;
using Microsoft.EntityFrameworkCore;

[ExcludeFromCodeCoverage]
internal sealed class DepositRepository: IRepository<Guid, DepositEntity>
{
	private readonly IDatabaseContext _databaseContext;

	public DepositRepository(IDatabaseContext databaseContext)
	{
		_databaseContext = databaseContext;
	}
	
	public Task<DepositEntity> GetByKeyAsync(Guid key, CancellationToken cancellationToken)
	{
		return _databaseContext.Deposits.FirstOrDefaultAsync(s => s.Id.Equals(key), cancellationToken);
	}

	public Task<bool> ExistsAsync(Guid key, CancellationToken cancellationToken)
	{
		return _databaseContext.Deposits.AnyAsync(x => x.Id.Equals(key), cancellationToken);
	}

	public IAsyncEnumerable<DepositEntity> GetAll(CancellationToken cancellationToken)
	{
		return _databaseContext.Deposits.AsAsyncEnumerable();
	}

	public async Task AddAsync(DepositEntity entity, CancellationToken cancellationToken)
	{
		await _databaseContext.Deposits.AddAsync(entity, cancellationToken);
		await _databaseContext.SaveChangesAsync(cancellationToken);
	}

	public async Task AddManyAsync(IEnumerable<DepositEntity> entities, CancellationToken cancellationToken)
	{
		foreach (DepositEntity entity in entities)
		{
			await _databaseContext.Deposits.AddAsync(entity, cancellationToken);
		}
		await _databaseContext.SaveChangesAsync(cancellationToken);
	}

	public Task DeleteAsync(Guid key, CancellationToken cancellationToken)
	{
		return _databaseContext.Deposits.Where(s => s.Id.Equals(key)).ExecuteDeleteAsync(cancellationToken);
	}
	
	public async Task UpdateAsync(CancellationToken cancellationToken)
	{
		await _databaseContext.SaveChangesAsync(cancellationToken);
	}
	
	public ValueTask DisposeAsync()
	{
		return _databaseContext.DisposeAsync();
	}
}