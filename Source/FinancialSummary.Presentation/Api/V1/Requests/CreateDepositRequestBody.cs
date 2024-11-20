namespace FinancialSummary.Presentation.Api.V1.Requests;

using System.Diagnostics.CodeAnalysis;

[ExcludeFromCodeCoverage]
public sealed record CreateDepositRequestBody(
	Guid? OperationId,
	string Name,
	decimal Cash,
	decimal InterestRate,
	int CapitalizationPerYear,
	DateTime StartDate,
	DateTime FinishDate);