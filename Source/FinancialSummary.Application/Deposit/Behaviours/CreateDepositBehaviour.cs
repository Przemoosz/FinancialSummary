namespace FinancialSummary.Application.Deposit.Behaviours;

using System.Net;
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
		Console.WriteLine(request.StartDate);
		Console.WriteLine(request.StartDate.ToLongDateString());
		Console.WriteLine(request.StartDate.ToShortDateString());
		
		if (!validationResult.IsValid)
		{
			// logger
			return new OperationFailed("Request Validation Error", validationResult.ToString(" "), HttpStatusCode.BadRequest);
		}

		var result = await next();

		return result;
	}
}