namespace FinancialSummary.Application.BondTypes.Validators
{
	using FluentValidation;
	using Requests;

	public class CreateBondTypeValidator: AbstractValidator<CreateBondTypeRequest>
	{
		public CreateBondTypeValidator()
		{
			RuleFor(s => s.Profit).GreaterThanOrEqualTo(100)
				.WithMessage("Profit can not be value higher or equal 100.")
				.LessThan(0)
				.WithMessage("Profit can not be value lower than 0.");
			
			RuleFor(s => s.FirstYearInterestRate).GreaterThanOrEqualTo(100)
				.WithMessage("FirstYearInterestRate can not be value higher or equal 100.")
				.LessThan(0)
				.WithMessage("FirstYearInterestRate can not be value lower than 0.");

			RuleFor(s => s.StartMonth).GreaterThan((uint) 12)
				.WithMessage("StartMonth can not be higher than 12");
			
			RuleFor(s => s.StartYear).GreaterThan((uint) DateTime.Now.Year)
				.WithMessage("StartYear can not be higher than actual year.");
			
			RuleFor(s => s.StartYear).LessThan((uint) 2000)
				.WithMessage("StartYear can not be lower than year 2000.");
			
			RuleFor(s => s.StartMonth).Must((x, y) => x.StartYear == DateTime.Now.Year && x.StartMonth <= DateTime.Now.Month)
				.WithMessage("StartMonth can not be higher than actual month in year.");
		}
	}
}