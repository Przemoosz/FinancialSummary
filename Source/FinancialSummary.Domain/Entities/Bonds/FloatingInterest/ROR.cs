namespace FinancialSummary.Domain.Entities.Bonds.FloatingInterest
{
	using FinancialSummary.Domain.Abstract.Entities;
	using FinancialSummary.Domain.Factories;

	public sealed class ROR: BondBase
	{
		public ROR(uint startYear, uint startMonth, decimal interestRate, decimal profit): base()
		{
			Name = BondNameFactory.Create<ROR>(startMonth, startYear);
			StartYear = startYear;
			StartMonth = startMonth;
			DurationInYears = 1;
			InterestRate = interestRate;
			Profit = profit;
			
			Type = BondType.FloatingInterest;
		}
	}
}