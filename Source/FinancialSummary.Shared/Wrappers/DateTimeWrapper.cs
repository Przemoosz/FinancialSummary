namespace FinancialSummary.Shared.Wrappers;

using Abstraction.Wrappers;

internal class DateTimeWrapper: IDateTimeWrapper
{
	public DateTime UtcNow { get; } = DateTime.UtcNow;
}