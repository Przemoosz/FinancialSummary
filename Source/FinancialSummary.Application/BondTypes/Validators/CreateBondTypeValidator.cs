namespace FinancialSummary.Application.BondTypes.Validators
{
	using FluentValidation;
	using Requests;

	public class CreateBondTypeValidator: AbstractValidator<CreateBondTypeRequest>
	{
		public CreateBondTypeValidator()
		{
			RuleFor(s => s.Profit).ExclusiveBetween(0, 100)
				.WithMessage("Profit can not be value higher or equal 100 or less than 0.");
			
			RuleFor(s => s.FirstYearInterestRate).ExclusiveBetween(0, 100)
				.WithMessage("FirstYearInterestRate can not be value higher or equal 100 or less than 0.");


			RuleFor(s => s.StartMonth).LessThan((uint)12)
				.WithMessage("StartMonth can not be higher than 12");
			
			RuleFor(s => s.StartYear).LessThan((uint) DateTime.Now.Year)
				.WithMessage("StartYear can not be higher than actual year.");
			
			RuleFor(s => s.StartYear).GreaterThanOrEqualTo((uint) 2000)
				.WithMessage("StartYear can not be lower than year 2000.");
			
			RuleFor(s => s.StartMonth).Must((x, y) =>
				{
					if (x.StartYear == DateTime.Now.Year)
					{
						if (x.StartMonth <= DateTime.Now.Month)
						{
							return true;
						}

						return false;
					}

					return true;
				})
				.WithMessage("StartMonth can not be higher than actual month in year.");
		}
	}
}