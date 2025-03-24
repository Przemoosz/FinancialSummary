namespace FinancialSummary.Infrastructure.DatabaseContext
{
	using System.Diagnostics.CodeAnalysis;
	using Abstract.DatabaseContext;
	using Domain.Entities;
	using Domain.Entities.Bonds.AntiInflationary;
	using Domain.Entities.Bonds.FixedInterest;
	using Domain.Entities.Bonds.FloatingInterest;
	using Domain.Entities.Deposit;
	using Microsoft.EntityFrameworkCore;

	[ExcludeFromCodeCoverage]
	public class DatabaseContext: DbContext, IDatabaseContext
	{
		public DbSet<ThreeYearsFixedInterestBondType> ThreeYearsFixedInterestBonds { get; set; }
		public DbSet<TwoYearsFloatingInterestBondType> TwoYearsFloatingInterestBonds { get; set; }
		public DbSet<OneYearFloatingInterestBondType> OneYearFloatingInterestBonds { get; set; }
		public DbSet<FourYearsAntiInflationaryBondType> FourYearsAntiInflationaryBonds { get; set; }
		public DbSet<TenYearsAntiInflationaryBondType> TenYearsAntiInflationaryBondBonds { get; set; }
		public DbSet<DepositEntity> Deposits { get; set; }
		public DbSet<CpiEntity> Cpi { get; set; }

		public DatabaseContext() : base()
		{
			AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
		}

		public DatabaseContext(DbContextOptions options): base(options)
		{
			AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<ThreeYearsFixedInterestBondType>().ToTable("three_years_fixed_interest_bond_types");
			modelBuilder.Entity<TwoYearsFloatingInterestBondType>().ToTable("two_years_floating_interest_bond_types");
			modelBuilder.Entity<OneYearFloatingInterestBondType>().ToTable("one_year_floating_interest_bond_types");
			modelBuilder.Entity<FourYearsAntiInflationaryBondType>().ToTable("four_year_anti_inflationary_bond_types");
			modelBuilder.Entity<TenYearsAntiInflationaryBondType>().ToTable("ten_year_anti_inflationary_bond_types");
			modelBuilder.Entity<DepositEntity>().ToTable("deposits");
			modelBuilder.Entity<CpiEntity>().ToTable("cpi");
			base.OnModelCreating(modelBuilder);
		}
	}
}