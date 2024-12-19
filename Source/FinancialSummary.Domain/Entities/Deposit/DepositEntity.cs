namespace FinancialSummary.Domain.Entities.Deposit;

using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using FinancialSummary.Domain.Abstract.Entities;

[ExcludeFromCodeCoverage]
public sealed record DepositEntity: IEntity
{
	public Guid Id { get; init; }
	
	[Required]
	[MaxLength(50)]
	public string Name { get; init; }
	
	[Required]
	public decimal Cash { get; init; }
	
	[Required]
	public decimal InterestRate { get; set; }
	
	[Required]
	public int CapitalizationPerYear { get; set; }
	
	[Required]
	public DateTime ModifyDate { get; set; }
	
	[Required]
	public DateTime StartDate { get; set; }
	
	[Required]
	public DateTime FinishDate { get; set; }
	
	public DepositEntity(string name, decimal cash, decimal interestRate, int capitalizationPerYear, DateTime startDate, DateTime finishDate)
	{
		Id =  Guid.NewGuid();
		Name = name;
		Cash = cash;
		InterestRate = interestRate;
		CapitalizationPerYear = capitalizationPerYear;
		ModifyDate = DateTime.UtcNow;
		StartDate = startDate;
		FinishDate = finishDate;
	}
}