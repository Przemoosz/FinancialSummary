namespace FinancialSummary.Presentation.Abstraction.Builders;

internal interface IProblemDetailsBuilderWithTitle
{
	IProblemDetailsBuilderWithDetails WithDetails(string details);
}