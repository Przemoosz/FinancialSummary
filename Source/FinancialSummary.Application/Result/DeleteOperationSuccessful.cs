namespace FinancialSummary.Application.Result;

internal sealed class DeleteOperationSuccessful: OperationSuccessful
{
	public Guid EntityId { get; init; }

	public DeleteOperationSuccessful(Guid entityId)
	{
		EntityId = entityId;
	}
}