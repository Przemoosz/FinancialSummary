namespace FinancialSummary.Infrastructure.Repository
{
	using Abstract.DatabaseContext;
	using Application.Contracts.Repository;
	using Domain.Entities;
	using Domain.Entities.Deposit;
	using Microsoft.EntityFrameworkCore;

	internal sealed class CpiRepository: IRepository<string, CpiEntity>
	{
		private readonly IDatabaseContext _databaseContext;

		public CpiRepository(IDatabaseContext databaseContext)
		{
			_databaseContext = databaseContext;
		}
	
		public Task<CpiEntity> GetByKeyAsync(string key, CancellationToken cancellationToken)
		{
			return _databaseContext.Cpi.FirstOrDefaultAsync(s => s.Id.Equals(key), cancellationToken);
		}

		public Task<bool> ExistsAsync(string key, CancellationToken cancellationToken)
		{
			return _databaseContext.Cpi.AnyAsync(x => x.Id.Equals(key), cancellationToken);
		}

		public IAsyncEnumerable<CpiEntity> GetAll(CancellationToken cancellationToken)
		{
			return _databaseContext.Cpi.AsAsyncEnumerable();
		}

		public async Task AddAsync(CpiEntity entity, CancellationToken cancellationToken)
		{
			await _databaseContext.Cpi.AddAsync(entity, cancellationToken);
			await _databaseContext.SaveChangesAsync(cancellationToken);
		}

		public async Task AddManyAsync(IEnumerable<CpiEntity> entities, CancellationToken cancellationToken)
		{
			foreach (CpiEntity entity in entities)
			{
				await _databaseContext.Cpi.AddAsync(entity, cancellationToken);
			}
			await _databaseContext.SaveChangesAsync(cancellationToken);
		}

		public Task DeleteAsync(string key, CancellationToken cancellationToken)
		{
			return _databaseContext.Cpi.Where(s => s.Id.Equals(key)).ExecuteDeleteAsync(cancellationToken);
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
}