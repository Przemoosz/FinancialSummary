namespace FinancialSummary.Application.Result;

using System.Diagnostics.CodeAnalysis;
using OneOf;

[ExcludeFromCodeCoverage]
public sealed class OperationResult: OneOfBase<OperationFailed, OperationSuccessful>
{
	public OperationResult(OneOf<OperationFailed, OperationSuccessful> input) : base(input)
	{
	}

	public static implicit operator OperationResult(OperationFailed operationFailed) => new(operationFailed);
	public static implicit operator OperationResult(OperationSuccessful operationSuccessful) => new(operationSuccessful);
}