namespace FinancialSummary.Domain.Entities.Bonds.FixedInterest
{
	using Abstract.Entities;
	using Factories;

	public sealed class ThreeYearsFixedInterestBondType: BondTypeBase
	{
		public ThreeYearsFixedInterestBondType(uint startYear, uint startMonth, decimal firstYearInterestRate): base(BondNameFactory.Create<ThreeYearsFixedInterestBondType>(startYear, startMonth))
		{
			StartYear = startYear;
			StartMonth = startMonth;
			DurationInYears = 3;
			FirstYearInterestRate = firstYearInterestRate;
		}
	}
}