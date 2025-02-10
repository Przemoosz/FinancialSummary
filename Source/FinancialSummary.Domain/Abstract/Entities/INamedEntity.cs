namespace FinancialSummary.Domain.Abstract.Entities
{
	public interface INamedEntity
	{
		string Name { get; }
		DateTime ModifyDate { get; }
	}
}