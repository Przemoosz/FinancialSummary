namespace FinancialSummary.Application.Deposit.Behaviours;

using System.Net;
using Contracts.Repository;
using Domain.Entities;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;
using Requests;
using Result;

public class UpdateDepositBehaviour: IPipelineBehavior<UpdateDepositRequest, OperationResult>
{
	private readonly IRepository<DepositEntity> _repository;
	private readonly IValidator<UpdateDepositRequest> _validator;
	private readonly ILogger<UpdateDepositBehaviour> _logger;

	public UpdateDepositBehaviour(IRepository<DepositEntity> repository, IValidator<UpdateDepositRequest> validator,
		ILogger<UpdateDepositBehaviour> logger)
	{
		_repository = repository;
		_validator = validator;
		_logger = logger;
	}
	
	public async Task<OperationResult> Handle(UpdateDepositRequest request, RequestHandlerDelegate<OperationResult> next, CancellationToken cancellationToken)
	{
		if (!await _repository.ExistsAsync(request.Id, cancellationToken))
		{
			_logger.LogWarning($"Deposit {request.Id} does not exists");
			return new OperationFailed("Deposit does not exists", $"Deposit {request.Id} does not exists.", HttpStatusCode.NotFound);
		}

		var validationResult = await _validator.ValidateAsync(request, cancellationToken);
		if (!validationResult.IsValid)
		{
			_logger.LogWarning($"Request Validation Error. {validationResult.ToString(" ")}");
			return new OperationFailed("Request Validation Error", validationResult.ToString(" "), HttpStatusCode.BadRequest);
		}

		var response = await next();

		return response;
	}
}