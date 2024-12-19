namespace FinancialSummary.Infrastructure.Repository;

using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;
using System.Reflection;
using Abstract.DatabaseContext;
using Application.Contracts.Repository;
using Application.Deposit.Requests;
using Domain.Abstract.Entities;
using Domain.Entities;
using Domain.Entities.Deposit;
using Extensions;
using Extensions.Expressions;
using FluentValidation.Internal;
using Microsoft.EntityFrameworkCore;

[ExcludeFromCodeCoverage]
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
	
	public async Task UpdateEntityPropertyAsync<TProperty>(Guid id, Expression<Func<IEntity, TProperty>> expression, TProperty value, CancellationToken cancellationToken)
	{
		DepositEntity depositEntity = await GetByIdAsync(id, cancellationToken);
		
		depositEntity.UpdateProperty(expression, value);
		
		depositEntity.ModifyDate = DateTime.UtcNow;
		
		await _depositContext.SaveChangesAsync(cancellationToken);
	}
	
	public ValueTask DisposeAsync()
	{
		return _depositContext.DisposeAsync();
	}
}