namespace FinancialSummary.Application.Abstraction.Queries;

using Domain.Abstract.Entities;
using MediatR;

public abstract record GetByKeyQuery<TEntity, TKey>(TKey Id) :IRequest<TEntity> where TEntity: IEntity<TKey>;