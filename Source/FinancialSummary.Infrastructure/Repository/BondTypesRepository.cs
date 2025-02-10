namespace FinancialSummary.Infrastructure.Repository
{
	using Application.Contracts.Repository;
	using Domain.Abstract.Entities;

	internal sealed class BondTypesRepository: IRepository<string, BondTypeBase>
	{
		public Task<BondTypeBase> GetByKeyAsync(string key, CancellationToken cancellationToken)
		{
			throw new NotImplementedException();
		}

		public Task<bool> ExistsAsync(string key, CancellationToken cancellationToken)
		{
			throw new NotImplementedException();
		}

		public IAsyncEnumerable<BondTypeBase> GetAll(CancellationToken cancellationToken)
		{
			throw new NotImplementedException();
		}

		public Task AddAsync(BondTypeBase depositEntity, CancellationToken cancellationToken)
		{
			throw new NotImplementedException();
		}

		public Task DeleteAsync(string key, CancellationToken cancellationToken)
		{
			throw new NotImplementedException();
		}

		public Task UpdateAsync(CancellationToken cancellationToken)
		{
			throw new NotImplementedException();
		}
	}
}