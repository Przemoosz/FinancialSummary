namespace FinancialSummary.Application.Extensions;

using System.Linq.Expressions;
using System.Reflection;
using Expressions;
using Domain.Abstract.Entities;

internal static class EntityExtensions
{
	internal static TEntity UpdateProperty<TEntity, TProperty>(this TEntity entity,
		Expression<Func<TEntity, TProperty>> expression, TProperty value)
		where TEntity : IEntity 
		where TProperty : class
	{
		if (value is null)
		{
			return entity;
		}
		PropertyInfo propertyToUpdate = expression.GetProperty();
		propertyToUpdate.SetValue(entity, value);
		return entity;
	}
	
	internal static TEntity UpdateProperty<TEntity, TProperty>(this TEntity entity,
		Expression<Func<TEntity, TProperty>> expression, TProperty? value)
		where TEntity : IEntity 
		where TProperty : struct
	{
		if (value.HasValue)
		{
			PropertyInfo propertyToUpdate = expression.GetProperty();
			propertyToUpdate.SetValue(entity, value.Value);
			return entity;
		}

		return entity;
	}
}