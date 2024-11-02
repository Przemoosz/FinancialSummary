namespace FinancialSummary.Domain.Entities;

using System.ComponentModel.DataAnnotations;

public sealed class DepositEntity
{
	public Guid Id { get; set; }
	
	[Required]
	[MaxLength(50)]
	public string Name { get; set; }
	
	[Required]
	public decimal Cash { get; set; }
	
	[Required]
	public decimal InterestRate { get; set; }
	
	[Required]
	public int CapitalizationPerYear { get; set; }
	
	[Required]
	public DateTime CreationDate { get; set; }
	
	[Required]
	public DateTime StartDate { get; set; }
	
	[Required]
	public DateTime FinishDate { get; set; }
}