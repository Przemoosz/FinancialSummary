namespace FinancialSummary.Domain.Entities.Bonds.FloatingInterest
{
	using System.ComponentModel.DataAnnotations;
	using FinancialSummary.Domain.Abstract.Entities;
	using Factories;

	public sealed class OneYearFloatingInterestBondType: BondTypeBase
	{
		[Required]
		public decimal Profit { get; init; }
		
		public OneYearFloatingInterestBondType(uint startYear, uint startMonth, decimal firstYearInterestRate, decimal profit): base(BondNameFactory.Create<OneYearFloatingInterestBondType>(startYear, startMonth))
		{
			StartYear = startYear;
			StartMonth = startMonth;
			DurationInYears = 1;
			FirstYearInterestRate = firstYearInterestRate;
			Profit = profit;
		}
	}
}