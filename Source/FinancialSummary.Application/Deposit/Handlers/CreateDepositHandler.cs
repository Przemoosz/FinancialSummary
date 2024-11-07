namespace FinancialSummary.Application.Deposit.Handlers;

using Commands.Requests;
using Contracts.Repository;
using Domain.Entities;
using MediatR;
using Result;

internal sealed class CreateDepositHandler: IRequestHandler<CreateDepositRequest, OperationResult>
{
	private readonly IRepository<DepositEntity> _repository;

	public CreateDepositHandler(IRepository<DepositEntity> repository)
	{
		_repository = repository;
	}
	
	public async Task<OperationResult> Handle(CreateDepositRequest request, CancellationToken cancellationToken)
	{
		// Validate
		
		// Create Entity
		
		// Add Entity
		
		throw new NotImplementedException();
	}
}