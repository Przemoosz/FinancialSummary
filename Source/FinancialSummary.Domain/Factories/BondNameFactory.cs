namespace FinancialSummary.Domain.Factories
{
	using Abstract.Entities;
	using Enums.BondTypes;

	internal static class BondNameFactory
	{
		public static string Create<TBond>(uint startYear, uint startMonth) where TBond: BondTypeBase
		{
			uint durationInYears = typeof(TBond).Name switch
			{
				"TenYearsAntiInflationaryBondType" => 10,
				"FourYearsAntiInflationaryBondType" => 4,
				"ThreeYearsFixedInterestBondType" => 3,
				"TwoYearsFloatingInterestBondType" => 2,
				"OneYearFloatingInterestBondType" => 1,
				_ => throw new ArgumentException("Unexpected type.")
			};

			string name = typeof(TBond).Name switch
			{
				"TenYearsAntiInflationaryBondType" => "EDO",
				"FourYearsAntiInflationaryBondType" => "COI",
				"ThreeYearsFixedInterestBondType" => "TOS",
				"TwoYearsFloatingInterestBondType" => "DOR",
				"OneYearFloatingInterestBondType" => "ROR",
				_ => throw new ArgumentException("Unexpected type.")
			};
			
			if (startMonth > 12)
			{
				throw new ArgumentException("Start Month can not be greater that 12");
			}
			
			string endMonth = startMonth >= 10 ? startMonth.ToString() : $"0{startMonth}";

			uint endYear = startYear % 100 + durationInYears;
			
			string endYearString = endYear >=10 ? endYear.ToString() : $"0{endYear}";
			
			return $"{name}{endMonth}{endYearString}";
		}

		public static string Create(BondTypes bondType, uint startYear, uint startMonth)
		{
			uint durationInYears = bondType switch
			{
				BondTypes.EDO => 10,
				BondTypes.COI => 4,
				BondTypes.TOS => 3,
				BondTypes.DOR => 2,
				BondTypes.ROR => 1,
				_ => throw new ArgumentException("Unexpected type.")
			};
			string endMonth = startMonth >= 10 ? startMonth.ToString() : $"0{startMonth}";

			uint endYear = startYear % 100 + durationInYears;
			
			string endYearString = endYear >=10 ? endYear.ToString() : $"0{endYear}";
			
			return $"{bondType}{endMonth}{endYearString}";
		}
	}
}