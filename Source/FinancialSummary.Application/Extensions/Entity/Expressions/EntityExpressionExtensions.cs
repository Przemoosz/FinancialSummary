namespace FinancialSummary.Application.Extensions.Entity.Expressions;

using System.Linq.Expressions;
using System.Reflection;
using Domain.Abstract.Entities;

internal static class EntityExpressionExtensions
{
	internal static PropertyInfo GetProperty<TEntity, TProperty>(this Expression<Func<TEntity, TProperty>> expression) where TEntity: IEntityBase
	{
		var memberExpression = expression.Body as MemberExpression ?? throw new ArgumentException("Provided expression does not reefers to property");
        var propertyInfo = memberExpression.Member as PropertyInfo ?? throw new ArgumentException("Provided expression does not reefers to property");
        return propertyInfo;
	}
}