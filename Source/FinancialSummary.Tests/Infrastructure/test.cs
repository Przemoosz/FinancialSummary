namespace FinancialSummary.Tests.Infrastructure;

using FinancialSummary.Infrastructure.Abstract.DatabaseContext;
using FinancialSummary.Infrastructure.Repository;
using NSubstitute;

public class test
{
	private DepositRepository _sut;

	[SetUp]
	public void Setup()
	{
		_sut = new DepositRepository(Substitute.For<IDepositContext>());
	}

	[Test]
	public async Task A()
	{
		await _sut.UpdateManyEntityPropertiesAsync(Guid.Empty, x => x.Name, "tak");
	}
}