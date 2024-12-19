namespace FinancialSummary.Infrastructure.Extensions.Expressions;

using System.Linq.Expressions;
using System.Reflection;
using FinancialSummary.Domain.Abstract.Entities;

internal static class EntityExpressionExtensions
{
	internal static PropertyInfo GetProperty<TProperty>(this Expression<Func<IEntity, TProperty>> expression)
	{
		var memberExpression = expression.Body as MemberExpression ?? throw new ArgumentException("Provided expression does not reefers to property");
        var propertyInfo = memberExpression.Member as PropertyInfo ?? throw new ArgumentException("Provided expression does not reefers to property");
        return propertyInfo;
	}
}