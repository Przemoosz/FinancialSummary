namespace FinancialSummary.Presentation.Api.V1.Requests;

public sealed record CreateDepositRequestBody(
	Guid? OperationId,
	string Name,
	double Cash,
	double InterestRate,
	int CapitalizationPerYear,
	DateTime StartDate,
	DateTime FinishDate);