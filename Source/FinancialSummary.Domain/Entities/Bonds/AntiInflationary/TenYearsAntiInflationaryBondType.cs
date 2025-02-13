namespace FinancialSummary.Domain.Entities.Bonds.AntiInflationary
{
	using System.ComponentModel.DataAnnotations;
	using Factories;
	using FinancialSummary.Domain.Abstract.Entities;

	public sealed class TenYearsAntiInflationaryBondType : BondTypeBase
	{
		[Required]
		public decimal Profit { get; init; }
		
		public TenYearsAntiInflationaryBondType(uint startYear, uint startMonth, decimal firstYearInterestRate, decimal profit) : base(BondNameFactory.Create<TenYearsAntiInflationaryBondType>(startYear, startMonth))
		{
			StartYear = startYear;
			StartMonth = startMonth;
			DurationInYears = 10;
			FirstYearInterestRate = firstYearInterestRate;
			Profit = profit;

		}
	}
}