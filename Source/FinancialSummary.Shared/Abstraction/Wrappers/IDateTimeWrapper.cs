namespace FinancialSummary.Shared.Abstraction.Wrappers;

public interface IDateTimeWrapper
{
	public DateTime UtcNow { get; }
}