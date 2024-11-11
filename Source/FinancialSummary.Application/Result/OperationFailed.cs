namespace FinancialSummary.Application.Result;

using System.Net;

public sealed class OperationFailed
{
	public string FailureReason { get; init; }
	public string ErrorMessage { get; init; }
	
	public HttpStatusCode StatusCode { get; init; }
	public OperationFailed(string failureReason, string errorMessage, HttpStatusCode statusCode)
	{
		FailureReason = failureReason;
		ErrorMessage = errorMessage;
		StatusCode = statusCode;
	}
}