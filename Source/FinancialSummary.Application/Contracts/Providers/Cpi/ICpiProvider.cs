namespace FinancialSummary.Application.Contracts.Providers.Cpi;

public interface ICpiProvider
{
	Task<decimal> GetCpiAsync(int month, int year);
}