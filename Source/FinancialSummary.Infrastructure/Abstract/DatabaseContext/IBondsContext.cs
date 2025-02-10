namespace FinancialSummary.Infrastructure.Abstract.DatabaseContext
{
	using Domain.Abstract.Entities;
	using Microsoft.EntityFrameworkCore;

	public interface IBondsContext: IAsyncDisposable
	{
		DbSet<BondTypeBase> Bonds { get; }
		Task<int> SaveChangesAsync(CancellationToken cancellationToken);
	}
}