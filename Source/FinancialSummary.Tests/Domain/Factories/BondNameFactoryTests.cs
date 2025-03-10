namespace FinancialSummary.Tests.Domain.Factories
{
	using Categories;
	using FinancialSummary.Domain.Entities.Bonds.AntiInflationary;
	using FinancialSummary.Domain.Entities.Bonds.FixedInterest;
	using FinancialSummary.Domain.Entities.Bonds.FloatingInterest;
	using FinancialSummary.Domain.Factories;
	using FluentAssertions;

	[TestFixture, DomainLayerTests, Parallelizable]
	public sealed class BondNameFactoryTests
	{
		private const uint StartYear = 2024;

		[Test]
		public void Create_IfMonthIsGreaterThan12_ThrowsArgumentException()
		{
			// Act && Assert
			Assert.Throws<ArgumentException>(() => BondNameFactory.Create<TenYearsAntiInflationaryBondType>(StartYear, 13));
		}
		
		[Test]
		public void Create_CreatesNameForTOS()
		{
			// Arrange
			const uint startMonth = 8;
			
			// Act
			string actual = BondNameFactory.Create<ThreeYearsFixedInterestBondType>(StartYear, startMonth);
			
			// Assert
			actual.Should().Be("TOS0827");
		}
		
		[Test]
		public void Create_CreatesNameForCOI()
		{
			// Arrange
			const uint startMonth = 10;
			
			// Act
			string actual = BondNameFactory.Create<FourYearsAntiInflationaryBondType>(StartYear, startMonth);
			
			// Assert
			actual.Should().Be("COI1028");
		}
		
		[Test]
		public void Create_CreatesNameForEDO()
		{
			// Arrange
			const uint startMonth = 1;
			
			// Act
			string actual = BondNameFactory.Create<TenYearsAntiInflationaryBondType>(StartYear, startMonth);
			
			// Assert
			actual.Should().Be("EDO0134");
		}
		
		[Test]
		public void Create_CreatesNameForROR()
		{
			// Arrange
			const uint startMonth = 4;
			
			// Act
			string actual = BondNameFactory.Create<OneYearFloatingInterestBondType>(2008, startMonth);
			
			// Assert
			actual.Should().Be("ROR0409");
		}
		
		[Test]
		public void Create_CreatesNameForDOR()
		{
			// Arrange
			const uint startMonth = 12;
			
			// Act
			string actual = BondNameFactory.Create<TwoYearsFloatingInterestBondType>(StartYear, startMonth);
			
			// Assert
			actual.Should().Be("DOR1226");
		}
	}
}