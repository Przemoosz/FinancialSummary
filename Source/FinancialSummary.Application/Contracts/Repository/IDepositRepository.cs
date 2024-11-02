namespace FinancialSummary.Application.Contracts.Repository;

using Domain.Entities;

public interface IDepositRepository: IAsyncDisposable
{
	Task<DepositEntity> GetByIdAsync(Guid id, CancellationToken cancellationToken);
	
	IAsyncEnumerable<DepositEntity> GetAll(CancellationToken cancellationToken);
	
	Task AddAsync(DepositEntity depositEntity, CancellationToken cancellationToken);
	
	Task DeleteAsync(Guid id, CancellationToken cancellationToken);
}