namespace FinancialSummary.Application.Result;

using System.Diagnostics.CodeAnalysis;

[ExcludeFromCodeCoverage]
public sealed class OperationSuccessful
{
	public object Context { get; }

	public OperationSuccessful(object context)
	{
		Context = context;
	}
}