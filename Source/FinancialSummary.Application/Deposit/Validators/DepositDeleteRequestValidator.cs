namespace FinancialSummary.Application.Deposit.Validators;

using Contracts.Repository;
using Domain.Entities;
using Domain.Entities.Deposit;
using FluentValidation;
using Requests;

internal sealed class DepositDeleteRequestValidator: AbstractValidator<DeleteDepositRequest>
{
	public DepositDeleteRequestValidator(IRepository<Guid, DepositEntity> repository)
	{
		RuleFor(x => x.Id).MustAsync(async (id, ct) => await repository.ExistsAsync(id, ct))
			.WithMessage(x => $"Deposit with id {x.Id} does not exists.").WithErrorCode(ValidationErrorCodes.EntityNotFound);
	}
}