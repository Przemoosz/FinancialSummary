namespace FinancialSummary.Application.Deposit.Behaviours;

using System.Net;
using FluentValidation;
using FluentValidation.Results;
using MediatR;
using Microsoft.Extensions.Logging;
using Requests;
using Result;

internal sealed class DeleteDepositBehavior: IPipelineBehavior<DeleteDepositRequest, OperationResult>
{
	private readonly IValidator<DeleteDepositRequest> _validator;
	private readonly ILogger<DeleteDepositBehavior> _logger;

	public DeleteDepositBehavior(IValidator<DeleteDepositRequest> validator, ILogger<DeleteDepositBehavior> logger)
	{
		_validator = validator;
		_logger = logger;
	}
	
	public async Task<OperationResult> Handle(DeleteDepositRequest request, RequestHandlerDelegate<OperationResult> next, CancellationToken cancellationToken)
	{
		ValidationResult validationResult = await _validator.ValidateAsync(request, cancellationToken);
		if (!validationResult.IsValid)
		{
			_logger.LogWarning(validationResult.ToString());
			return new OperationFailed("Deposit does not exists", validationResult.ToString(), HttpStatusCode.NotFound);
		}

		var result = await next();
		return result;
	}
}