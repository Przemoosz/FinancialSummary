namespace FinancialSummary.Application.Deposit.Behaviors;

using System.Net;
using FinancialSummary.Application.Deposit.Requests;
using FinancialSummary.Application.Result;
using FluentValidation;
using FluentValidation.Results;
using MediatR;
using Microsoft.Extensions.Logging;

internal class CreateDepositBehavior: IPipelineBehavior<CreateDepositRequest, OperationResult>
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
		ValidationResult validationResult = await _validator.ValidateAsync(request, cancellationToken);
		
		if (!validationResult.IsValid)
		{
			_logger.LogWarning($"Request Validation Error. {validationResult.ToString(" ")}");
			return new OperationFailed("Request Validation Error", validationResult.ToString(" "), HttpStatusCode.BadRequest);
		}

		var result = await next();

		return result;
	}
}