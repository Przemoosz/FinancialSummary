namespace FinancialSummary.Domain.Factories
{
	using Abstract.Factories;
	using Entities;

	internal sealed class CpiEntityFactory: ICpiEntityFactory
	{
		public CpiEntity Create(uint month, uint year, double value)
		{
			return new CpiEntity(CreateName(month, year), value);
		}

		public CpiEntity[] CreateForYear(uint year, double[] values)
		{
			if (values.Length != 12)
			{
				throw new ArgumentException("Provided quantity of CPI values for year is not equals to 12.");
			}

			CpiEntity[] entities = new CpiEntity[12];

			for (uint i = 0; i < 12; i++)
			{
				entities[i] = Create(i, year, values[i]);
			}

			return entities;
		}

		private string CreateName(uint month, uint year)
		{
			string endMonth = month >= 10 ? month.ToString() : $"0{month}";

			uint endYear = year % 100;
			
			string endYearString = endYear >=10 ? endYear.ToString() : $"0{endYear}";
			
			return $"{endMonth}_{endYearString}";
		}
	}
}