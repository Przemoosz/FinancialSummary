namespace FinancialSummary.Application.Deposit.Commands.Handlers;

using Contracts.Repository;
using Domain.Abstract.Entities;
using Domain.Entities;
using MediatR;

public class GetByIdHandler: IRequestHandler<QueryById<DepositEntity>, DepositEntity> 
{
	private readonly IRepository<DepositEntity> _repository;

	public GetByIdHandler(IRepository<DepositEntity> repository)
	{
		_repository = repository;
	}
	
	public Task<DepositEntity> Handle(QueryById<DepositEntity> request, CancellationToken cancellationToken)
	{
		Console.WriteLine(request.Id);
		return null;
	}
}
public sealed record QueryById<TEntity>(Guid Id) :IRequest<TEntity> where TEntity: IEntity;