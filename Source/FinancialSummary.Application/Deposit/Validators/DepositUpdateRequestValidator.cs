namespace FinancialSummary.Application.Deposit.Validators;

using Contracts.Repository;
using Domain.Entities.Deposit;
using FluentValidation;
using Requests;

internal class DepositUpdateRequestValidator: AbstractValidator<UpdateDepositRequest>
{
	public DepositUpdateRequestValidator(IRepository<DepositEntity> repository)
	{
		RuleFor(x => x.Id).MustAsync(async (id, ct) => await repository.ExistsAsync(id, ct))
			.WithMessage(x => $"Deposit with id {x.Id} does not exists.").WithErrorCode(ValidationErrorCodes.EntityNotFound);
		
		RuleFor(x => x.Cash)
			.GreaterThan(0)
			.Unless(x => x.Cash is null)
			.WithMessage("Deposit cash can not be less or equal to zero.");
		
		RuleFor(x => x.Name)
			.MaximumLength(50)
			.Unless(x => x.Name is null)
			.WithMessage("Name maximum length can not be greater than 50 characters.");

		RuleFor(x => x.Name)
			.NotEmpty()
			.Unless(x => x.Name is null)
			.Configure(c => c.MessageBuilder = _ => "Name can not be empty.");
		
		RuleFor(x => x.InterestRate)
			.LessThan(100)
			.GreaterThan(0)
			.Unless(x => x.InterestRate is null)
			.Configure(c => c.MessageBuilder = _ => "Interest Rate value must be greater than 0 and less than 100.");
		
		RuleFor(x => x.CapitalizationPerYear)
			.LessThan(12)
			.GreaterThan(0)
			.Unless(x => x.CapitalizationPerYear is null)
			.Configure(c => c.MessageBuilder = _ => "Capitalization Per Year value must be greater than 0 and less than 12.");
	}
}