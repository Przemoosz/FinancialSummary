namespace FinancialSummary.Domain.Entities.Bonds.FixedInterest
{
	using Abstract.Entities;
	using Factories;

	public sealed class TOS: BondBase
	{
		public TOS(uint startYear, uint startMonth, decimal interestRate): base()
		{
			Name = BondNameFactory.Create<TOS>(startMonth, startYear);
			StartYear = startYear;
			StartMonth = startMonth;
			DurationInYears = 3;
			InterestRate = interestRate;
			
			Profit = 0;
			Type = BondType.FixedInterest;
		}
	}
}