namespace FinancialSummary.Application.Deposit.Handlers;

using Commands.Queries;
using Contracts.Repository;
using Domain.Entities;
using MediatR;

internal class GetDepositByIdHandler: IRequestHandler<GetDepositGetByIdQuery, DepositEntity> 
{
	private readonly IRepository<DepositEntity> _repository;

	public GetDepositByIdHandler(IRepository<DepositEntity> repository)
	{
		_repository = repository;
	}
	
	public async Task<DepositEntity> Handle(GetDepositGetByIdQuery query, CancellationToken cancellationToken)
	{
		var entity = await _repository.GetByIdAsync(query.Id, cancellationToken);
		return entity;
	}
}