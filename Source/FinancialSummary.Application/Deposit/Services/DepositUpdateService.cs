namespace FinancialSummary.Application.Deposit.Services;

using Contracts.Repository;
using Domain.Entities.Deposit;
using Extensions;

internal sealed class DepositUpdateService: IDepositUpdateService
{
	private readonly IRepository<DepositEntity> _repository;

	public DepositUpdateService(IRepository<DepositEntity> repository)
	{
		_repository = repository;
	}
		
	public async Task UpdateAsync(Guid id, UpdateDepositEntity updateDepositEntity, CancellationToken cancellationToken)
	{
		DepositEntity depositEntity = await _repository.GetByIdAsync(id, cancellationToken);

		depositEntity
			.UpdateProperty(x => x.Name, updateDepositEntity.Name)
			.UpdateProperty(x => x.Cash, updateDepositEntity.Cash)
			.UpdateProperty(x => x.CapitalizationPerYear, updateDepositEntity.CapitalizationPerYear)
			.UpdateProperty(x => x.InterestRate, updateDepositEntity.InterestRate)
			.UpdateProperty(x => x.ModifyDate, DateTime.UtcNow);

		await _repository.UpdateAsync(cancellationToken);
	}
}