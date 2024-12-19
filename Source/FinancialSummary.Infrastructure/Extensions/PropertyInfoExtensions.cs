namespace FinancialSummary.Infrastructure.Extensions;

using System.Linq.Expressions;
using System.Reflection;
using Domain.Abstract.Entities;
using Expressions;

internal static class EntityExtensions
{
	internal static void UpdateProperty<TProperty>(this IEntity entity, Expression<Func<IEntity, TProperty>> expression, TProperty value)
	{
		PropertyInfo propertyToUpdate = expression.GetProperty();
		propertyToUpdate.SetValue(entity, value);
	}
}