namespace FinancialSummary.Infrastructure.Abstract.DatabaseContext
{
	using Domain.Entities;
	using Domain.Entities.Bonds.AntiInflationary;
	using Domain.Entities.Bonds.FixedInterest;
	using Domain.Entities.Bonds.FloatingInterest;
	using Domain.Entities.Deposit;
	using Microsoft.EntityFrameworkCore;

	public interface IDatabaseContext: IAsyncDisposable
	{
		DbSet<ThreeYearsFixedInterestBondType> ThreeYearsFixedInterestBonds { get; }
		DbSet<TwoYearsFloatingInterestBondType> TwoYearsFloatingInterestBonds { get; set; }
		DbSet<OneYearFloatingInterestBondType> OneYearFloatingInterestBonds { get; set; }
		DbSet<FourYearsAntiInflationaryBondType> FourYearsAntiInflationaryBonds { get; set; }
		DbSet<TenYearsAntiInflationaryBondType> TenYearsAntiInflationaryBondBonds { get; set; }
		DbSet<DepositEntity> Deposits { get; }
		DbSet<CpiEntity> Cpi { get; }

		Task<int> SaveChangesAsync(CancellationToken cancellationToken);
		DbSet<TEntity> Set<TEntity>() where TEntity : class;
	}
}