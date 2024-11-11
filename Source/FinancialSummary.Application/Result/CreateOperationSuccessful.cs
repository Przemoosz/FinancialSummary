namespace FinancialSummary.Application.Result;

internal sealed class CreateOperationSuccessful: OperationSuccessful
{
	public Guid EntityId { get; init; }

	public CreateOperationSuccessful(Guid entityId)
	{
		EntityId = entityId;
	}
}