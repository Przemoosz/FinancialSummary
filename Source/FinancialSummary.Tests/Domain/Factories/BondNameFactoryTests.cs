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
			Assert.Throws<ArgumentException>(() => BondNameFactory.Create<EDO>(StartYear, 13));
		}
		
		[Test]
		public void Create_CreatesNameForTOS()
		{
			// Arrange
			const uint startMonth = 8;
			const uint durationInYear = 3;
			
			// Act
			string actual = BondNameFactory.Create<TOS>(StartYear, startMonth);
			
			// Assert
			actual.Should().Be("TOS0827");
		}
		
		[Test]
		public void Create_CreatesNameForCOI()
		{
			// Arrange
			const uint startMonth = 10;
			const uint durationInYear = 4;
			
			// Act
			string actual = BondNameFactory.Create<COI>(StartYear, startMonth);
			
			// Assert
			actual.Should().Be("COI1028");
		}
		
		[Test]
		public void Create_CreatesNameForEDO()
		{
			// Arrange
			const uint startMonth = 1;
			const uint durationInYear = 10;
			
			// Act
			string actual = BondNameFactory.Create<EDO>(StartYear, startMonth);
			
			// Assert
			actual.Should().Be("EDO0134");
		}
		
		[Test]
		public void Create_CreatesNameForROR()
		{
			// Arrange
			const uint startMonth = 4;
			const uint durationInYear = 1;
			
			// Act
			string actual = BondNameFactory.Create<ROR>(StartYear, startMonth);
			
			// Assert
			actual.Should().Be("ROR0425");
		}
		
		[Test]
		public void Create_CreatesNameForDOR()
		{
			// Arrange
			const uint startMonth = 12;
			const uint durationInYear = 2;
			
			// Act
			string actual = BondNameFactory.Create<DOR>(StartYear, startMonth);
			
			// Assert
			actual.Should().Be("DOR1226");
		}
		
	}
}