namespace FinancialSummary.Application.BondTypes.Validators
{
	using FluentValidation;
	using Requests;

	public class CreateBondTypeValidator: AbstractValidator<CreateBondTypeRequest>
	{
		public CreateBondTypeValidator()
		{
			
		}
	}
}