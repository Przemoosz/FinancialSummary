namespace FinancialSummary.Domain.Abstract.Entities
{
	using System.ComponentModel.DataAnnotations;
	using Domain.Entities.Bonds;

	public abstract class BondBase: IEntity
	{
		public Guid Id { get; init; }
		
		[Required]
		public DateTime ModifyDate { get; init; }
		
		[Required]
		[Length(7,7)]
		public string Name { get; protected init; }
		
		[Required]
		public uint StartYear { get; protected init; }
		
		[Required]
		public uint StartMonth { get; protected init; }
		
		[Required]
		public uint DurationInYears { get; protected init; }
		
		[Required]
		public BondType Type { get; protected init; }
		
		[Required]
		public decimal InterestRate { get; protected init; }		
		
		[Required]
		public decimal Profit { get; protected init; }

		protected BondBase()
		{
			Id = Guid.NewGuid();
			ModifyDate = DateTime.UtcNow;
		}
	}
}