namespace FinancialSummary.Application.Deposit.Handlers;

using Contracts.Repository;
using Domain.Abstract.Factories;
using Domain.Entities;
using FluentValidation;
using MediatR;
using Requests;
using Result;

internal sealed class CreateDepositHandler: IRequestHandler<CreateDepositRequest, OperationResult>
{
	private readonly IRepository<DepositEntity> _repository;
	private readonly IDepositEntityFactory _depositEntityFactory;

	public CreateDepositHandler(IRepository<DepositEntity> repository, IDepositEntityFactory depositEntityFactory)
	{
		_repository = repository;
		_depositEntityFactory = depositEntityFactory;
	}
	
	public async Task<OperationResult> Handle(CreateDepositRequest request, CancellationToken cancellationToken)
	{
		// Create Entity
		DepositEntity depositEntity = _depositEntityFactory.Create(request.Name, request.Cash, request.InterestRate,
			request.CapitalizationPerYear, request.StartDate, request.FinishDate); 
		
		// Add Entity
		await _repository.AddAsync(depositEntity, cancellationToken);
		return new CreateOperationSuccessful(depositEntity.Id);
	}
}