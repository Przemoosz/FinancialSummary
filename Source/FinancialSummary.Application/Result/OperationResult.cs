namespace FinancialSummary.Application.Result;

using OneOf;

public sealed class OperationResult: OneOfBase<OperationFailed, OperationSuccessful>
{
	public OperationResult(OneOf<OperationFailed, OperationSuccessful> input) : base(input)
	{
	}

	public static implicit operator OperationResult(OperationFailed operationFailed) => new(operationFailed);
	public static implicit operator OperationResult(OperationSuccessful operationSuccessful) => new(operationSuccessful);
}