namespace FinancialSummary.Application.Deposit.Handlers;

using System.Net;
using Contracts.Repository;
using Domain.Entities;
using Domain.Entities.Deposit;
using MediatR;
using Queries;
using Result;

internal class GetDepositByIdHandler: IRequestHandler<GetDepositGetByIdQuery, OperationResult> 
{
	private readonly IRepository<DepositEntity> _repository;

	public GetDepositByIdHandler(IRepository<DepositEntity> repository)
	{
		_repository = repository;
	}
	
	public async Task<OperationResult> Handle(GetDepositGetByIdQuery query, CancellationToken cancellationToken)
	{
		try
		{
			var entity = await _repository.GetByIdAsync(query.Id, cancellationToken);
			if (entity is null)
			{
				return new OperationFailed("Entity does not exists", $"Entity with id: {query.Id} does not exists",
					HttpStatusCode.NotFound);
			}
			return new OperationSuccessful(entity);
		}
		catch (Exception e)
		{
			return new OperationFailed("Failed to query entity.", e.Message, HttpStatusCode.InternalServerError);
		}
	}
}