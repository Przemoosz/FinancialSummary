namespace FinancialSummary.Infrastructure.Repository
{
	using Abstract.DatabaseContext;
	using Application.Contracts.Repository;
	using Domain.Abstract.Entities;
	using Microsoft.EntityFrameworkCore;

	internal sealed class BondTypesRepository<TBondType>: IRepository<string, TBondType> where TBondType: BondTypeBase, IEntity<string>
	{
		private readonly IBondTypesContext _context;
		private readonly DbSet<TBondType> _dbSet;

		public BondTypesRepository(IBondTypesContext context)
		{
			_context = context;
			_dbSet = context.Set<TBondType>();
		}
		
		public Task<TBondType> GetByKeyAsync(string key, CancellationToken cancellationToken)
		{
			return _dbSet.FirstOrDefaultAsync(s => s.Id.Equals(key), cancellationToken);
		}

		public Task<bool> ExistsAsync(string key, CancellationToken cancellationToken)
		{
			return _dbSet.AnyAsync(x => x.Id.Equals(key), cancellationToken);
		}

		public IAsyncEnumerable<TBondType> GetAll(CancellationToken cancellationToken)
		{
			return _dbSet.AsAsyncEnumerable();
		}

		public async Task AddAsync(TBondType depositEntity, CancellationToken cancellationToken)
		{
			await _dbSet.AddAsync(depositEntity, cancellationToken);
			await _context.SaveChangesAsync(cancellationToken);
		}

		public Task DeleteAsync(string key, CancellationToken cancellationToken)
		{
			return _dbSet.Where(s => s.Id.Equals(key)).ExecuteDeleteAsync(cancellationToken);
		}

		public async Task UpdateAsync(CancellationToken cancellationToken)
		{
			await _context.SaveChangesAsync(cancellationToken);
		}
	}
}