namespace FinancialSummary.Application.BondTypes.Requests
{
	using Domain.Abstract.Entities;
	using Domain.Enums.BondTypes;
	using MediatR;
	using Result;

	public sealed record CreateBondTypeRequest(
		Guid OperationId,
		BondTypes BondType,
		uint StartYear,
		uint StartMonth,
		decimal FirstYearInterestRate,
		decimal Profit) : IRequest<OperationResult>;
}