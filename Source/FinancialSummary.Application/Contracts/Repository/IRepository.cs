namespace FinancialSummary.Application.Contracts.Repository;

using Domain.Abstract.Entities;

public interface IRepository<TEntity> where TEntity: IEntity
{
	Task<TEntity> GetByIdAsync(Guid id, CancellationToken cancellationToken);
	
	IAsyncEnumerable<TEntity> GetAll(CancellationToken cancellationToken);
	
	Task AddAsync(TEntity depositEntity, CancellationToken cancellationToken);
	
	Task DeleteAsync(Guid id, CancellationToken cancellationToken);
}