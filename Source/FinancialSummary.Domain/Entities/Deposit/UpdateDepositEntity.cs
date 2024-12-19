namespace FinancialSummary.Domain.Entities.Deposit;

public class UpdateDepositEntity
{
	public string Name { get; set; }
	
	public decimal? Cash { get; set; }
	
	public decimal? InterestRate { get; set; }
	
	public int? CapitalizationPerYear { get; set; }
}