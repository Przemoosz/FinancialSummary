namespace FinancialSummary.Application.Result;

public sealed class OperationSuccessful
{
	public object Context { get; }

	public OperationSuccessful(object context)
	{
		Context = context;
	}
}