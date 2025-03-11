namespace FinancialSummary.Shared.Extensions
{
	public static class ArrayExtension
	{
		public static T[] Slice<T>(this T[] array, int length)
		{
			if (length > array.Length)
			{
				throw new ArgumentException("Array bonudary exceeded");
			}
			ReadOnlySpan<T> span = array;
			return span.Slice(0, length).ToArray();
		}
	}
}