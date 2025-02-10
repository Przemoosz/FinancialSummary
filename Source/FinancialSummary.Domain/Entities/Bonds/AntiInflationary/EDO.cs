namespace FinancialSummary.Domain.Entities.Bonds.AntiInflationary
{
	using Factories;
	using FinancialSummary.Domain.Abstract.Entities;

	public sealed class EDO: BondTypeBase
	{
		public EDO(uint startYear, uint startMonth, decimal interestRate, decimal profit): base()
		{
			Name = BondNameFactory.Create<EDO>(startMonth, startYear);
			StartYear = startYear;
			StartMonth = startMonth;
			DurationInYears = 10;
			InterestRate = interestRate;
			Profit = profit;
			
			Type = BondType.AntiInflationary;
		}
	}
}