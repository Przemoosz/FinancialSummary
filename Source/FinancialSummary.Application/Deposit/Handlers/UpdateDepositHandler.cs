namespace FinancialSummary.Application.Deposit.Handlers;

using Contracts.Repository;
using Domain.Entities;
using Domain.Entities.Deposit;
using MediatR;
using Microsoft.Extensions.Logging;
using Requests;
using Result;
using Services;

internal sealed class UpdateDepositHandler: IRequestHandler<UpdateDepositRequest, OperationResult>
{
	private readonly IRepository<DepositEntity> _repository;
	private readonly IDepositUpdateService _depositUpdateService;
	private readonly ILogger<UpdateDepositHandler> _logger;

	public UpdateDepositHandler(IDepositUpdateService depositUpdateService, ILogger<UpdateDepositHandler> logger)
	{
		_depositUpdateService = depositUpdateService;
		_logger = logger;
	}
	
	public async Task<OperationResult> Handle(UpdateDepositRequest request, CancellationToken cancellationToken)
	{
		await _depositUpdateService.UpdateAsync(request.Id, request.ToUpdateEntity(), cancellationToken);
		
		throw new NotImplementedException();
	}
}