namespace FinancialSummary.Shared.Extensions;

using FluentValidation.Results;

public static class ValidationResultExtensions
{
	public static bool ContainsErrorCode(this ValidationResult validationResult, string errorCode)
	{
		if (validationResult.IsValid)
		{
			return false;
		}

		return validationResult.Errors.Any(s => s.ErrorCode.Equals(errorCode));
	}
}