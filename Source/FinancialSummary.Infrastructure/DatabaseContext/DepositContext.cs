namespace FinancialSummary.Infrastructure.DatabaseContext;

using Abstract.DatabaseContext;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

public class DepositContext: DbContext, IDepositContext
{
	public DbSet<DepositEntity> Deposits { get; set; }
	public DepositContext() : base()
	{
		AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
	}

	public DepositContext(DbContextOptions options): base(options)
	{
		AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
	}

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		modelBuilder.Entity<DepositEntity>().ToTable("deposits");
		base.OnModelCreating(modelBuilder);
	}
	
	
}