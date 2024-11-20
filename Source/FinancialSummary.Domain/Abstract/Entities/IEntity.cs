namespace FinancialSummary.Domain.Abstract.Entities;

public interface IEntity
{
	Guid Id { get; }
	DateTime ModifyDate { get; }
}