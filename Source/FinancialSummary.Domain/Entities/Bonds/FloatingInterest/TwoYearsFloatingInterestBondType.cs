namespace FinancialSummary.Domain.Entities.Bonds.FloatingInterest
{
	using System.ComponentModel.DataAnnotations;
	using FinancialSummary.Domain.Abstract.Entities;
	using Factories;

	public sealed class TwoYearsFloatingInterestBondType: BondTypeBase
	{
		[Required]
		public decimal Profit { get; init; }
		public TwoYearsFloatingInterestBondType(uint startYear, uint startMonth, decimal firstYearInterestRate, decimal profit): base(BondNameFactory.Create<TwoYearsFloatingInterestBondType>(startYear, startMonth))
		{
			StartYear = startYear;
			StartMonth = startMonth;
			DurationInYears = 2;
			FirstYearInterestRate = firstYearInterestRate;
			Profit = profit;
		}
	}
}