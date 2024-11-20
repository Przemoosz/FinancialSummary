namespace FinancialSummary.Tests.Shared.Extensions;

using Categories;
using FinancialSummary.Shared.Extensions;
using FluentAssertions;

[Parallelizable, SharedLayerTests]
public sealed class DateTimeExtensionsTests
{
	[Test]
	public void ToShortDate_ShouldReturnShortDate()
	{
		// Arrange
		DateTime dateTime = new DateTime(1945, 7, 16, 5, 29, 30);
		DateTime expectedShortDate = new DateTime(1945, 7, 16);

		// Act
		DateTime actual = dateTime.ToShortDate();

		// Assert
		actual.Should().Be(expectedShortDate);
	}
}