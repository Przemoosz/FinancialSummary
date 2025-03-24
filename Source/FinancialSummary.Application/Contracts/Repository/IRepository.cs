namespace FinancialSummary.Application.Contracts.Repository;

using Domain.Abstract.Entities;

public interface IRepository<in TKey, TEntity> where TEntity: IEntity<TKey>
{
	Task<TEntity> GetByKeyAsync(TKey key, CancellationToken cancellationToken);
	Task<bool> ExistsAsync(TKey key, CancellationToken cancellationToken);
	
	IAsyncEnumerable<TEntity> GetAll(CancellationToken cancellationToken);
	
	Task AddAsync(TEntity entity, CancellationToken cancellationToken);
	Task AddManyAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken);
	
	Task DeleteAsync(TKey key, CancellationToken cancellationToken);
	Task UpdateAsync(CancellationToken cancellationToken);

}