namespace FinancialSummary.Shared.Extensions;

public static class DateTimeExtensions
{
	public static DateTime ToShortDate(this DateTime dateTime)
	{
		return new DateTime(dateTime.Year, dateTime.Month, dateTime.Day);
	}
}