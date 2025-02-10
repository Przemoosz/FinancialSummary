namespace FinancialSummary.Domain.Entities.Bonds.AntiInflationary
{
	using FinancialSummary.Domain.Abstract.Entities;
	using FinancialSummary.Domain.Factories;

	public sealed class COI: BondTypeBase
	{
		public COI(uint startYear, uint startMonth, decimal interestRate, decimal profit): base()
		{
			Name = BondNameFactory.Create<COI>(startMonth, startYear);
			StartYear = startYear;
			StartMonth = startMonth;
			DurationInYears = 4;
			InterestRate = interestRate;
			Profit = profit;
			
			Type = BondType.AntiInflationary;
		}
	}
}