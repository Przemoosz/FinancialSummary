namespace FinancialSummary.Application.Deposit.Handlers;

using System.Net;
using Contracts.Repository;
using Domain.Entities;
using Domain.Entities.Deposit;
using MediatR;
using Microsoft.Extensions.Logging;
using Requests;
using Result;

internal class DeleteRepositoryHandler: IRequestHandler<DeleteDepositRequest, OperationResult>
{
	private readonly IRepository<Guid, DepositEntity> _repository;
	private readonly ILogger<DeleteRepositoryHandler> _logger;

	public DeleteRepositoryHandler(IRepository<Guid, DepositEntity> repository, ILogger<DeleteRepositoryHandler> logger)
	{
		_repository = repository;
		_logger = logger;
	}
	
	public async Task<OperationResult> Handle(DeleteDepositRequest request, CancellationToken cancellationToken)
	{
		try
		{
			await _repository.DeleteAsync(request.Id, cancellationToken);
		}
		catch (Exception e)
		{
			_logger.LogError(e, $"Failed to delete entity {request.Id}");
			return new OperationFailed("Error when deleting deposit entity", e.Message,
				HttpStatusCode.InternalServerError);
		}
		
		_logger.LogInformation($"Deposit with id {request.Id} deleted.");

		return new OperationSuccessful(request.Id);
	}
}