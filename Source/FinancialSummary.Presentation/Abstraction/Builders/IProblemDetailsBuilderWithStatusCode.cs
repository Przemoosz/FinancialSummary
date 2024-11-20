namespace FinancialSummary.Presentation.Abstraction.Builders;

using Microsoft.AspNetCore.Mvc;

internal interface IProblemDetailsBuilderWithStatusCode
{
	IProblemDetailsBuilderWithStatusCode WithExtension(string key, object value);
	ProblemDetails Build();
}