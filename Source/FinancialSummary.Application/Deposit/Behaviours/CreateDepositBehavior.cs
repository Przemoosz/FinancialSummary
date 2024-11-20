namespace FinancialSummary.Application.Deposit.Behaviours;

using System.Net;
using FluentValidation;
using FluentValidation.Results;
using MediatR;
using Microsoft.Extensions.Logging;
using Requests;
using Result;

public class CreateDepositBehavior: IPipelineBehavior<CreateDepositRequest, OperationResult>
{
	private readonly IValidator<CreateDepositRequest> _validator;
	private readonly ILogger<CreateDepositBehavior> _logger;

	public CreateDepositBehavior(IValidator<CreateDepositRequest> validator, ILogger<CreateDepositBehavior> logger)
	{
		_validator = validator;
		_logger = logger;
	}
	
	public async Task<OperationResult> Handle(CreateDepositRequest request, RequestHandlerDelegate<OperationResult> next, CancellationToken cancellationToken)
	{
		ValidationResult validationResult = await _validator.ValidateAsync(request);
		
		if (!validationResult.IsValid)
		{
			_logger.LogWarning($"Request Validation Error. {validationResult.ToString(" ")}");
			return new OperationFailed("Request Validation Error", validationResult.ToString(" "), HttpStatusCode.BadRequest);
		}

		var result = await next();

		return result;
	}
}