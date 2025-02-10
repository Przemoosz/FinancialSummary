namespace FinancialSummary.Domain.Entities.Bonds.AntiInflationary
{
	using System.ComponentModel.DataAnnotations;
	using FinancialSummary.Domain.Abstract.Entities;
	using FinancialSummary.Domain.Factories;

	public sealed class FourYearsAntiInflationaryBondType: BondTypeBase
	{
		[Required]
		public decimal Profit { get; init; }
		
		public FourYearsAntiInflationaryBondType(uint startYear, uint startMonth, decimal firstYearInterestRate, decimal profit): base(BondNameFactory.Create<FourYearsAntiInflationaryBondType>(startMonth, startYear))
		{
			StartYear = startYear;
			StartMonth = startMonth;
			DurationInYears = 4;
			FirstYearInterestRate = firstYearInterestRate;
			Profit = profit;
		}
	}
}