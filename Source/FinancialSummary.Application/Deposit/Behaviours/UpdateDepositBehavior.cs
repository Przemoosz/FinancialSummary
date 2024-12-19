namespace FinancialSummary.Application.Deposit.Behaviours;

using System.Net;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;
using Requests;
using Result;
using Shared.Extensions;

public class UpdateDepositBehavior: IPipelineBehavior<UpdateDepositRequest, OperationResult>
{
	private readonly IValidator<UpdateDepositRequest> _validator;
	private readonly ILogger<UpdateDepositBehavior> _logger;

	public UpdateDepositBehavior(IValidator<UpdateDepositRequest> validator,
		ILogger<UpdateDepositBehavior> logger)
	{
		_validator = validator;
		_logger = logger;
	}
	
	public async Task<OperationResult> Handle(UpdateDepositRequest request, RequestHandlerDelegate<OperationResult> next, CancellationToken cancellationToken)
	{
		var validationResult = await _validator.ValidateAsync(request, cancellationToken);
		if (validationResult.ContainsErrorCode(ValidationErrorCodes.EntityNotFound))
		{
			_logger.LogWarning($"Deposit {request.Id} does not exists");
			return new OperationFailed("Deposit does not exists", $"Deposit {request.Id} does not exists.", HttpStatusCode.NotFound);
		}

		if (!validationResult.IsValid)
		{
			_logger.LogWarning($"Request Validation Error. {validationResult.ToString(" ")}");
			return new OperationFailed("Request Validation Error", validationResult.ToString(" "), HttpStatusCode.BadRequest);
		}

		var response = await next();

		return response;
	}
}