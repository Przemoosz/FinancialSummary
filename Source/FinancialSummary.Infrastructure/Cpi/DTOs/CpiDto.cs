namespace FinancialSummary.Infrastructure.Cpi.DTOs;

using System.Text.Json.Serialization;

internal sealed class CpiDto
{
	[JsonPropertyName("wartosc")]
	public decimal Value { get; set; }
}