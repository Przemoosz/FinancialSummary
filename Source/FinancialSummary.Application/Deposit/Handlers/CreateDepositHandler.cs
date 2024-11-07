namespace FinancialSummary.Application.Deposit.Handlers;

using Contracts.Repository;
using Domain.Entities;
using FluentValidation;
using MediatR;
using Requests;
using Result;

internal sealed class CreateDepositHandler: IRequestHandler<CreateDepositRequest, OperationResult>
{
	private readonly IRepository<DepositEntity> _repository;
	private readonly IValidator<CreateDepositRequest> _validator;

	public CreateDepositHandler(IRepository<DepositEntity> repository, IValidator<CreateDepositRequest> validator)
	{
		_repository = repository;
		_validator = validator;
	}
	
	public async Task<OperationResult> Handle(CreateDepositRequest request, CancellationToken cancellationToken)
	{
		// Validate
		
		// Create Entity
		
		// Add Entity
		
		throw new NotImplementedException();
	}
}