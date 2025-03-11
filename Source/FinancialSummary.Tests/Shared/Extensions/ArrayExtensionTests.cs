namespace FinancialSummary.Tests.Shared.Extensions
{
	using Categories;
	using FinancialSummary.Shared.Extensions;
	using FluentAssertions;

	[Parallelizable, SharedLayerTests]
	public class ArrayExtensionTests
	{
		[Test]
		public void Slice_ShouldReturnSliceOfArray()
		{
			// Arrange
			int[] ints = new int[] { 1, 2, 3, 4, 5 };
			
			// Act
			var subArray = ints.Slice(3);
			
			// Assert
			subArray.Length.Should().Be(3);
			subArray[0].Should().Be(1);
			subArray[1].Should().Be(2);
			subArray[2].Should().Be(3);
		}
	}
}