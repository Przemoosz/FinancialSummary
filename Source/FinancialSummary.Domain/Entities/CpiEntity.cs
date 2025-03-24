namespace FinancialSummary.Domain.Entities
{
	using Abstract.Entities;

	public sealed record CpiEntity: IEntity<string>
	{
		public DateTime ModifyDate { get; init; }
		public string Id { get; init; }
		public double Value { get; init; }

		public CpiEntity()
		{ }
		internal CpiEntity(string name, double value)
		{
			Id = name;
			Value = value;
		}
	}
}