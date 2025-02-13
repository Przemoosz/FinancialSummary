namespace FinancialSummary.Presentation.Api.V1.BondTypes.Requests
{
	using System.Diagnostics.CodeAnalysis;
	using FinancialSummary.Domain.Enums.BondTypes;

	[ExcludeFromCodeCoverage]
	public sealed record CreateBondTypeRequestBody(
		Guid? OperationId,
		BondTypes BondType,
		uint StartYear,
		uint StartMonth,
		uint DurationInYears,
		decimal FirstYearInterestRate);
}