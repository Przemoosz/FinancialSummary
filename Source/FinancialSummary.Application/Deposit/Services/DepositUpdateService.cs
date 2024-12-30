namespace FinancialSummary.Application.Deposit.Services;

using Abstraction.Deposit.Services;
using Contracts.Repository;
using Domain.Entities.Deposit;
using Extensions;
using Extensions.Entity;
using Shared.Abstraction.Wrappers;

internal sealed class DepositUpdateService: IDepositUpdateService
{
	private readonly IRepository<DepositEntity> _repository;
	private readonly IDateTimeWrapper _dateTimeWrapper;

	public DepositUpdateService(IRepository<DepositEntity> repository, IDateTimeWrapper dateTimeWrapper)
	{
		_repository = repository;
		_dateTimeWrapper = dateTimeWrapper;
	}
		
	public async Task UpdateAsync(Guid id, UpdateDepositEntity updateDepositEntity, CancellationToken cancellationToken)
	{
		DepositEntity depositEntity = await _repository.GetByIdAsync(id, cancellationToken);

		depositEntity
			.UpdateProperty(x => x.Name, updateDepositEntity.Name)
			.UpdateProperty(x => x.Cash, updateDepositEntity.Cash)
			.UpdateProperty(x => x.CapitalizationPerYear, updateDepositEntity.CapitalizationPerYear)
			.UpdateProperty(x => x.InterestRate, updateDepositEntity.InterestRate)
			.UpdateProperty(x => x.ModifyDate, _dateTimeWrapper.UtcNow);

		await _repository.UpdateAsync(cancellationToken);
	}
}