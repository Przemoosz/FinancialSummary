namespace FinancialSummary.Domain.Abstract.Entities;

public interface IEntity<out TKey> : IEntityBase
{
	TKey Id { get; }
}