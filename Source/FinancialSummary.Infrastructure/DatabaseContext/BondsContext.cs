namespace FinancialSummary.Infrastructure.DatabaseContext
{
	using System.Diagnostics.CodeAnalysis;
	using Abstract.DatabaseContext;
	using Domain.Abstract.Entities;
	using Microsoft.EntityFrameworkCore;

	[ExcludeFromCodeCoverage]
	public class BondsContext: DbContext, IBondsContext
	{
		public DbSet<BondTypeBase> Bonds { get; set; }
		public BondsContext() : base()
		{
			AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
		}

		public BondsContext(DbContextOptions options): base(options)
		{
			AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<BondTypeBase>().ToTable("bonds");
			base.OnModelCreating(modelBuilder);
		}
	}
}