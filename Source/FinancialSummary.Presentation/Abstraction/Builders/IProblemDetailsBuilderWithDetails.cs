namespace FinancialSummary.Presentation.Abstraction.Builders;

using System.Net;

internal interface IProblemDetailsBuilderWithDetails
{
	IProblemDetailsBuilderWithStatusCode ForStatusCode(HttpStatusCode statusCode);
}