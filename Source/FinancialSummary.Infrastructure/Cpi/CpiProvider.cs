namespace FinancialSummary.Infrastructure.Cpi
{
	using Application.Contracts.Providers.Cpi;
	using DTOs;
	using Microsoft.Extensions.Caching.Memory;

	internal sealed class CpiProvider : ICpiProvider
	{
		private readonly IMemoryCache _memoryCache;
		private readonly ICpiClient _cpiClient;

		public CpiProvider(IMemoryCache memoryCache, ICpiClient cpiClient)
		{
			_memoryCache = memoryCache;
			_cpiClient = cpiClient;
		}
	
		public async Task<decimal> GetCpiAsync(int month, int year)
		{
			if (_memoryCache.TryGetValue<CpiDto[]>(year, out CpiDto[] cpi))
			{
				return cpi[month - 1].Value;
			}

			var values = await _cpiClient.GetAsync(year);
		
			_memoryCache.Set(year, values);
		
			return values[month - 1].Value;
		}
	}
}