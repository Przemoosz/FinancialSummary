namespace FinancialSummary.Tests.Application.Extensions.Entity.Expressions;

using System.Linq.Expressions;
using System.Reflection;
using Categories;
using FinancialSummary.Application.Extensions.Entity.Expressions;
using FinancialSummary.Domain.Entities.Deposit;
using FluentAssertions;

[TestFixture, ApplicationLayerTests, Parallelizable]
public class EntityExpressionExtensionsTests
{
	[Test]
	public void GetProperty_ValidExpression_ReturnsPropertyInfo()
	{
		// Arrange
		Expression<Func<DepositEntity, string>> expression = x => x.Name;

		// Act
		PropertyInfo propertyInfo = expression.GetProperty();

		// Assert
		propertyInfo.Should().NotBeNull();
		propertyInfo.Name.Should().Be("Name");
	}

	[Test]
	public void GetProperty_InvalidExpression_ThrowsArgumentException()
	{
		// Arrange
		Expression<Func<DepositEntity, string>> expression = x => x.Name + "test";
		Action act = () => expression.GetProperty();
		
		// Act & Assert
		act.Should().Throw<ArgumentException>().WithMessage("Provided expression does not reefers to property");
	}
}