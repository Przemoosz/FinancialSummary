namespace FinancialSummary.Domain.Entities.Deposit;

public record UpdateDepositEntity(string Name, decimal? Cash, decimal? InterestRate, int? CapitalizationPerYear);