namespace FinancialSummary.Presentation.Factories;

using Application.Result;
using Builders;
using FinancialSummary.Presentation.Abstraction.Factories;
using Microsoft.AspNetCore.Mvc;

internal sealed class ProblemDetailsFactory: IProblemDetailsFactory
{
	public ProblemDetails Create(OperationFailed operationFailed, Guid? operationId)
	{
		ProblemDetails problemDetails = ProblemDetailsBuilder.Create()
			.WithTitle(operationFailed.FailureReason)
			.WithDetails(operationFailed.ErrorMessage)
			.ForStatusCode(operationFailed.StatusCode)
			.WithExtension("operationId", operationId.GetValueOrDefault())
			.Build();
		return problemDetails;
	}
}