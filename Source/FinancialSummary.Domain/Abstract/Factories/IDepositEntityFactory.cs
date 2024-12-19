namespace FinancialSummary.Domain.Abstract.Factories;

using Domain.Entities.Deposit;
using FinancialSummary.Domain.Entities;

public interface IDepositEntityFactory
{
	DepositEntity Create(string name, decimal cash, decimal interestRate, int capitalizationPerYear,
		DateTime startDate, DateTime finishDate);
}