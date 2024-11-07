namespace FinancialSummary.Domain.Entities;

using System.ComponentModel.DataAnnotations;
using Abstract.Entities;

public sealed record DepositEntity: IEntity
{
	public Guid Id { get; init; }
	
	[Required]
	[MaxLength(50)]
	public string Name { get; init; }
	
	[Required]
	public decimal Cash { get; init; }
	
	[Required]
	public decimal InterestRate { get; init; }
	
	[Required]
	public int CapitalizationPerYear { get; init; }
	
	[Required]
	public DateTime ModifyDate { get; init; }
	
	[Required]
	public DateTime StartDate { get; init; }
	
	[Required]
	public DateTime FinishDate { get; init; }
	
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