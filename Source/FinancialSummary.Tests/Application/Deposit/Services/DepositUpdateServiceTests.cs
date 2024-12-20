namespace FinancialSummary.Tests.Application.Deposit.Services;

using Categories;
using FinancialSummary.Application.Contracts.Repository;
using FinancialSummary.Application.Deposit.Services;
using FinancialSummary.Domain.Entities.Deposit;
using FinancialSummary.Shared.Abstraction.Wrappers;
using FluentAssertions;
using NSubstitute;
using static TddXt.AnyRoot.Root;

[ApplicationLayerTests, Parallelizable]
public class DepositUpdateServiceTests
{
	private IRepository<DepositEntity> _repository;
	private DepositUpdateService _sut;
	private IDateTimeWrapper _dateTimeWrapper;

	[SetUp]
	public void Setup()
	{
		_repository = Substitute.For<IRepository<DepositEntity>>();
		_dateTimeWrapper = Substitute.For<IDateTimeWrapper>();
		_sut = new DepositUpdateService(_repository, _dateTimeWrapper);
	}

	[Test]
	public async Task UpdateAsync_UpdatesAndSavesEntity()
	{
		// Arrange
		DepositEntity depositEntity = Any.Instance<DepositEntity>();
		const string newName = "NewName";
		const int newCash = -90;
		const int newCapitalization = -9;
		const decimal newInterestRate = -8;
		DateTime newModifyDate = new DateTime(1939, 9, 1);
		UpdateDepositEntity updateDepositEntity = new UpdateDepositEntity(newName, newCash, newInterestRate, newCapitalization);
		_repository.GetByIdAsync(depositEntity.Id, CancellationToken.None).Returns(depositEntity);
		_dateTimeWrapper.UtcNow.Returns(newModifyDate);
		
		// Act
		await _sut.UpdateAsync(depositEntity.Id, updateDepositEntity, CancellationToken.None);
		
		// Assert
		depositEntity.Name.Should().Be(newName);
		depositEntity.CapitalizationPerYear.Should().Be(newCapitalization);
		depositEntity.Cash.Should().Be(newCash);
		depositEntity.InterestRate.Should().Be(newInterestRate);
		depositEntity.ModifyDate.Should().Be(newModifyDate);
		_repository.Received(1).UpdateAsync(CancellationToken.None);

	}
	
}