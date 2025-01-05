namespace FinancialSummary.Tests.Domain.Factories;

using Categories;
using FinancialSummary.Domain.Factories;
using FinancialSummary.Shared.Extensions;
using FluentAssertions;

[TestFixture, DomainLayerTests, Parallelizable]
public class DepositEntityFactoryTests
{
	private DepositEntityFactory _factory;

	[SetUp]
	public void SetUp()
	{
		_factory = new DepositEntityFactory();
	}

	[Test]
	public void Create_WhenCalled_ShouldReturnDepositEntity()
	{
		// Arrange
		var name = "Test Deposit";
		var cash = 1000m;
		var interestRate = 5m;
		var capitalizationPerYear = 4;
		var startDate = DateTime.Now;
		var finishDate = DateTime.Now.AddYears(1);

		// Act
		var result = _factory.Create(name, cash, interestRate, capitalizationPerYear, startDate, finishDate);

		// Assert
		result.Should().NotBeNull();
		result.Name.Should().Be(name);
		result.Cash.Should().Be(cash);
		result.InterestRate.Should().Be(interestRate);
		result.CapitalizationPerYear.Should().Be(capitalizationPerYear);
		result.StartDate.Should().Be(startDate.ToShortDate());
		result.FinishDate.Should().Be(finishDate.ToShortDate());
	}
}