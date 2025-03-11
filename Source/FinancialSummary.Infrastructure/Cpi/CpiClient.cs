namespace FinancialSummary.Infrastructure.Cpi
{
	using System.Net;
	using System.Text.Json;
	using DTOs;
	using Shared.Extensions;

	internal sealed class CpiClient: ICpiClient
	{
		private readonly HttpClient _httpClient;
		public CpiClient(HttpClient httpClient)
		{
			_httpClient = httpClient;
		}

		public async Task<CpiDto[]> GetAsync(int year)
		{
			var response = await _httpClient.GetAsync(CreateCpiUri(year));
			if (response.StatusCode == HttpStatusCode.NotFound)
			{
				return [];
			}

			response.EnsureSuccessStatusCode();
			
			CpiDto[] inflationThroughYear = await JsonSerializer.DeserializeAsync<CpiDto[]>(await response.Content.ReadAsStreamAsync() );
			return inflationThroughYear.Slice(12);
		}
		
		private static Uri CreateCpiUri(int year)
		{
			return new Uri($"https://api-sdp.stat.gov.pl/api/indicators/indicator-data-indicator?id-wskaznik=339&id-rok={year}&lang=pl");
		}
	}
}