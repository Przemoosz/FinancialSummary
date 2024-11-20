namespace FinancialSummary.Presentation.Abstraction.Factories;

using FinancialSummary.Application.Result;
using Microsoft.AspNetCore.Mvc;

public interface IProblemDetailsFactory
{
	ProblemDetails Create(OperationFailed operationFailed, Guid? operationId);
}