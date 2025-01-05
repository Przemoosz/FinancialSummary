namespace FinancialSummary.Domain.Entities.Bonds.FloatingInterest
{
	using FinancialSummary.Domain.Abstract.Entities;
	using FinancialSummary.Domain.Factories;

	public sealed class DOR: BondBase
	{
		public DOR(uint startYear, uint startMonth, decimal interestRate, decimal profit): base()
		{
			Name = BondNameFactory.Create<DOR>(startMonth, startYear);
			StartYear = startYear;
			StartMonth = startMonth;
			DurationInYears = 2;
			InterestRate = interestRate;
			Profit = profit;
			
			Type = BondType.FloatingInterest;
		}
	}
}