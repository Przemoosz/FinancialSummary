namespace FinancialSummary.Domain.Abstract.Entities
{
	using System.ComponentModel.DataAnnotations;

	public class BondTypeBase: INamedEntity
	{
		[Key]
		[Required]
		[Length(7,7)]
		public string Name { get; protected init; }
		
		[Required]
		public DateTime ModifyDate { get; init; }
		
		[Required]
		public uint StartYear { get; protected init; }
		
		[Required]
		public uint StartMonth { get; protected init; }
		
		[Required]
		public uint DurationInYears { get; protected init; }
		
		[Required]
		public decimal FirstYearInterestRate { get; protected init; }

		protected BondTypeBase(string name)
		{
			Name = name;
			ModifyDate = DateTime.UtcNow;
		}
	}
}