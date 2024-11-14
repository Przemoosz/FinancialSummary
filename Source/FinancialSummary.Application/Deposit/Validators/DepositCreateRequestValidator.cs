namespace FinancialSummary.Application.Deposit.Validators;

using FluentValidation;
using Requests;

internal class DepositCreateRequestValidator: AbstractValidator<CreateDepositRequest>
{
	public DepositCreateRequestValidator()
	{
		RuleFor(x => x.Cash)
			.GreaterThan(0)
			.WithMessage("Deposit cash can not be less or equal to zero.");
		
		RuleFor(x => x.Name)
			.NotEmpty()
			.NotNull()
			.WithMessage("Name can not be null or empty.")
			.MaximumLength(50)
			.WithMessage("Name maximum length can not be greater than 50 characters.");
		
		RuleFor(x => x.InterestRate)
			.LessThan(100)
			.GreaterThan(0)
			.WithMessage("Interest Rate value must be greater than 0 and less than 100.");
		
		RuleFor(x => x.CapitalizationPerYear)
			.LessThan(12)
			.GreaterThan(0)
			.WithMessage("Capitalization Per Year value must be greater than 0 and less than 12.");

		RuleFor(x => x.StartDate)
			.GreaterThan(new DateTime(2000, 1, 1))
			.WithMessage("Start Date Year can not be earlier than 2000.")
			.LessThan(DateTime.Now + TimeSpan.FromDays(1))
			.WithMessage("Start Date can not be in future.");
		
		RuleFor(x => x.FinishDate)
			.GreaterThan(new DateTime(2000, 1, 1))
			.WithMessage("Finish Date Year can not be earlier than 2000.")
			.GreaterThan(x => x.StartDate)
			.WithMessage("Finish Date can not be before start Date.");

	}
}