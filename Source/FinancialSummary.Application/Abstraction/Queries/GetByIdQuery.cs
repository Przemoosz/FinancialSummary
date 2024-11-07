namespace FinancialSummary.Application.Abstraction.Queries;

using Domain.Abstract.Entities;
using MediatR;

public abstract record GetByIdQuery<TEntity>(Guid Id) :IRequest<TEntity> where TEntity: IEntity;