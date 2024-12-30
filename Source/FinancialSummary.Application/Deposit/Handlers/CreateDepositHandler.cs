namespace FinancialSummary.Application.Deposit.Handlers;

using System.Net;
using Contracts.Repository;
using Domain.Abstract.Factories;
using Domain.Entities.Deposit;
using MediatR;
using Microsoft.Extensions.Logging;
using Requests;
using Result;

internal sealed class CreateDepositHandler: IRequestHandler<CreateDepositRequest, OperationResult>
{
	private readonly IRepository<DepositEntity> _repository;
	private readonly IDepositEntityFactory _depositEntityFactory;
	private readonly ILogger<CreateDepositRequest> _logger;

	public CreateDepositHandler(IRepository<DepositEntity> repository, IDepositEntityFactory depositEntityFactory,
		ILogger<CreateDepositRequest> logger)
	{
		_repository = repository;
		_depositEntityFactory = depositEntityFactory;
		_logger = logger;
	}
	
	public async Task<OperationResult> Handle(CreateDepositRequest request, CancellationToken cancellationToken)
	{
		DepositEntity depositEntity = _depositEntityFactory.Create(request.Name, request.Cash, request.InterestRate,
			request.CapitalizationPerYear, request.StartDate, request.FinishDate);

		try
		{
			await _repository.AddAsync(depositEntity, cancellationToken);
		}
		catch (Exception e)
		{
			_logger.LogError(e, $"Failed to create entity {depositEntity.Id}");
			return new OperationFailed("Error when adding deposit to entity", e.Message,
				HttpStatusCode.InternalServerError);
		}
		_logger.LogInformation($"Created entity {depositEntity.Id}");
		return new OperationSuccessful(new {EntityId = depositEntity.Id});
	}
}