namespace FinancialSummary.Domain.Factories
{
	using Abstract.Entities;

	internal static class BondNameFactory
	{
		public static string Create<TBond>(uint startYear, uint startMonth) where TBond: BondBase
		{
			uint durationInYears = typeof(TBond).Name switch
			{
				"EDO" => 10,
				"COI" => 4,
				"TOS" => 3,
				"DOR" => 2,
				"ROR" => 1,
				_ => throw new ArgumentException("Unexpected type.")
			};

			if (startMonth > 12)
			{
				throw new ArgumentException("Start Month can not be greater that 12");
			}
			
			string endMonth = startMonth >= 10 ? startMonth.ToString() : $"0{startMonth}";

			string endYear = (startYear % 100 + durationInYears).ToString();
			
			return $"{typeof(TBond).Name}{endMonth}{endYear}";
		}
	}
}