namespace FinancialSummary.Infrastructure.DatabaseContext;

using System.Diagnostics.CodeAnalysis;
using Abstract.DatabaseContext;
using Domain.Entities;
using Domain.Entities.Deposit;
using Microsoft.EntityFrameworkCore;

[ExcludeFromCodeCoverage]
public class DepositContext: DbContext, IDepositContext
{
	public DbSet<DepositEntity> Deposits { get; set; }
	public DepositContext() : base()
	{
		AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
	}

	public DepositContext(DbContextOptions<DepositContext> options): base(options)
	{
		AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
	}

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		modelBuilder.Entity<DepositEntity>().ToTable("deposits");
		base.OnModelCreating(modelBuilder);
	}
}