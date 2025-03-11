namespace FinancialSummary.Infrastructure.Cpi
{
	using DTOs;

	internal interface ICpiClient
	{
		Task<CpiDto[]> GetAsync(int year);
	}
}