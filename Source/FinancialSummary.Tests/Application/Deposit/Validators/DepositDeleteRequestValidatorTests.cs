namespace FinancialSummary.Tests.Application.Deposit.Validators;

using FinancialSummary.Application.Contracts.Repository;
using FinancialSummary.Application.Deposit.Requests;
using FinancialSummary.Application.Deposit.Validators;
using FinancialSummary.Domain.Entities;
using FinancialSummary.Domain.Entities.Deposit;
using FluentValidation.TestHelper;
using NSubstitute;

public class DepositDeleteRequestValidatorTests
{
	private IRepository<Guid, DepositEntity> _repository;
	private DepositDeleteRequestValidator _validator;

	[SetUp]
	public void SetUp()
	{
		_repository = Substitute.For<IRepository<Guid, DepositEntity>>();
		_validator = new DepositDeleteRequestValidator(_repository);
	}

	[Test]
	public async Task Validate_WhenDepositExists_ShouldNotHaveValidationError()
	{
		// Arrange
		var depositId = Guid.NewGuid();
		var request = new DeleteDepositRequest(depositId);
		_repository.ExistsAsync(depositId, Arg.Any<CancellationToken>()).Returns(true);

		// Act
		var result = await _validator.TestValidateAsync(request);

		// Assert
		result.ShouldNotHaveValidationErrorFor(x => x.Id);
	}

	[Test]
	public async Task Validate_WhenDepositDoesNotExist_ShouldHaveValidationError()
	{
		// Arrange
		var depositId = Guid.NewGuid();
		var request = new DeleteDepositRequest(depositId);
		_repository.ExistsAsync(depositId, Arg.Any<CancellationToken>()).Returns(false);

		// Act
		var result = await _validator.TestValidateAsync(request);

		// Assert
		result.ShouldHaveValidationErrorFor(x => x.Id)
			.WithErrorMessage($"Deposit with id {depositId} does not exists.");
	}
}