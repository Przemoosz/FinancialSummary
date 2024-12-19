namespace FinancialSummary.Domain.Factories;

using Abstract.Factories;
using Entities;
using Entities.Deposit;
using Shared.Extensions;

internal sealed class DepositEntityFactory: IDepositEntityFactory
{
	public DepositEntity Create(string name, decimal cash, decimal interestRate, int capitalizationPerYear,
		DateTime startDate, DateTime finishDate)
	{
		DepositEntity depositEntity = new DepositEntity(name,
			cash,
			interestRate,
			capitalizationPerYear,
			startDate.ToShortDate(),
			finishDate.ToShortDate());
		return depositEntity;
	}
}