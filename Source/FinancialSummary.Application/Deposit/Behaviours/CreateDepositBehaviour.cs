namespace FinancialSummary.Application.Deposit.Behaviours;

using FluentValidation;
using FluentValidation.Results;
using MediatR;
using Requests;
using Result;

public class CreateDepositBehaviour: IPipelineBehavior<CreateDepositRequest, OperationResult>
{
	private readonly IValidator<CreateDepositRequest> _validator;

	public CreateDepositBehaviour(IValidator<CreateDepositRequest> validator)
	{
		_validator = validator;
	}
	
	public async Task<OperationResult> Handle(CreateDepositRequest request, RequestHandlerDelegate<OperationResult> next, CancellationToken cancellationToken)
	{
		ValidationResult validationResult = await _validator.ValidateAsync(request);
		Console.WriteLine(a.ToString());
		throw new NotImplementedException();
	}
}