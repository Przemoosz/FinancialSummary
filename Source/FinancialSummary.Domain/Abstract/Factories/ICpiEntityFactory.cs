namespace FinancialSummary.Domain.Abstract.Factories
{
	using FinancialSummary.Domain.Entities;

	public interface ICpiEntityFactory
	{
		CpiEntity Create(uint month, uint year, double value);
		CpiEntity[] CreateForYear(uint year, double[] values);
	}
}