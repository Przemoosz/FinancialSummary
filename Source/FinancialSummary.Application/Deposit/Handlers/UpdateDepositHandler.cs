namespace FinancialSummary.Application.Deposit.Handlers;

using Contracts.Repository;
using Domain.Entities;
using Domain.Entities.Deposit;
using MediatR;
using Microsoft.Extensions.Logging;
using Requests;
using Result;

internal sealed class UpdateDepositHandler: IRequestHandler<CreateDepositRequest, OperationResult>
{
	private readonly IRepository<DepositEntity> _repository;
	private readonly ILogger<UpdateDepositHandler> _logger;

	public UpdateDepositHandler(IRepository<DepositEntity> repository, ILogger<UpdateDepositHandler> logger)
	{
		_repository = repository;
		_logger = logger;
	}
	
	public async Task<OperationResult> Handle(CreateDepositRequest request, CancellationToken cancellationToken)
	{
		
		
		throw new NotImplementedException();
	}
}