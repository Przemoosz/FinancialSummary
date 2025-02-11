namespace FinancialSummary.Infrastructure.Abstract.DatabaseContext
{
	using Domain.Entities.Bonds.AntiInflationary;
	using Domain.Entities.Bonds.FixedInterest;
	using Domain.Entities.Bonds.FloatingInterest;
	using Microsoft.EntityFrameworkCore;

	public interface IBondTypesContext: IAsyncDisposable
	{
		DbSet<ThreeYearsFixedInterestBondType> ThreeYearsFixedInterestBonds { get; }
		DbSet<TwoYearsFloatingInterestBondType> TwoYearsFloatingInterestBonds { get; set; }
		DbSet<OneYearFloatingInterestBondType> OneYearFloatingInterestBonds { get; set; }
		DbSet<FourYearsAntiInflationaryBondType> FourYearsAntiInflationaryBonds { get; set; }
		DbSet<TenYearsAntiInflationaryBondType> TenYearsAntiInflationaryBondBonds { get; set; }
		Task<int> SaveChangesAsync(CancellationToken cancellationToken);
		DbSet<TEntity> Set<TEntity>() where TEntity : class;
	}
}