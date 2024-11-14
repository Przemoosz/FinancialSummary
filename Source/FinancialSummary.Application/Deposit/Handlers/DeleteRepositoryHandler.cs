namespace FinancialSummary.Application.Deposit.Handlers;

using Contracts.Repository;
using Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;
using Requests;
using Result;

public class DeleteRepositoryHandler: IRequestHandler<DeleteDepositRequest, OperationResult>
{
	private readonly IRepository<DepositEntity> _repository;
	private readonly ILogger<DeleteRepositoryHandler> _logger;

	public DeleteRepositoryHandler(IRepository<DepositEntity> repository, ILogger<DeleteRepositoryHandler> logger)
	{
		_repository = repository;
		_logger = logger;
	}
	
	public async Task<OperationResult> Handle(DeleteDepositRequest request, CancellationToken cancellationToken)
	{
		await _repository.DeleteAsync(request.Id, cancellationToken);
		
		_logger.LogInformation($"Deposit with id {request.Id} deleted.");

		return new DeleteOperationSuccessful(request.Id);
	}
}