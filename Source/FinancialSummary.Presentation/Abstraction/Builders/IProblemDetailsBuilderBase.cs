namespace FinancialSummary.Presentation.Abstraction.Builders;

internal interface IProblemDetailsBuilderBase
{
	IProblemDetailsBuilderWithTitle WithTitle(string title);
}