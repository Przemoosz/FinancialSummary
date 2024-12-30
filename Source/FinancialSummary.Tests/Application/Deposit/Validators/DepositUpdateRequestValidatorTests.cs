namespace FinancialSummary.Tests.Application.Deposit.Validators;

using FinancialSummary.Application.Deposit.Requests;
using FinancialSummary.Application.Deposit.Validators;
using Categories;
using FinancialSummary.Application;
using FinancialSummary.Application.Contracts.Repository;
using FinancialSummary.Domain.Entities.Deposit;
using FluentValidation.TestHelper;
using NSubstitute;

[Parallelizable, ApplicationLayerTests]
public class DepositUpdateRequestValidatorTests
{
    private DepositUpdateRequestValidator _sut;
    private IRepository<DepositEntity> _repository;

    [SetUp]
    public void SetUp()
    {
        _repository = Substitute.For<IRepository<DepositEntity>>();
        _sut = new DepositUpdateRequestValidator(_repository);
    }

    [Test]
    public async Task Validate_WhenCashIsLessThanOrEqualToZero_ShouldHaveValidationError()
    {
        // Arrange
        var request = new UpdateDepositRequest(Guid.Empty, "ValidName", -10, 5, 5);

        // Act
        var result = await _sut.TestValidateAsync(request);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Cash)
            .WithErrorMessage("Deposit cash can not be less or equal to zero.");
    }
    
    [Test]
    public async Task Validate_WhenNameEmpty_ShouldHaveValidationError()
    {
        // Arrange
        var request = new UpdateDepositRequest(Guid.Empty, "", 10, 5, 5);

        // Act
        var result = await _sut.TestValidateAsync(request);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Name)
            .WithErrorMessage("Name can not be empty.");
    }

    [Test]
    public async Task Validate_WhenNameExceedsMaxLength_ShouldHaveValidationError()
    {
        // Arrange
        var request = new UpdateDepositRequest(Guid.Empty, new string('a', 51), 10, 5, 5);

        // Act
        var result = await _sut.TestValidateAsync(request);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Name)
            .WithErrorMessage("Name maximum length can not be greater than 50 characters.");
    }

    [Test]
    public async Task Validate_WhenInterestRateIsOutOfRange_ShouldHaveValidationError()
    {
        // Arrange
        var request = new UpdateDepositRequest(Guid.Empty, "ValidName", 10, 101, 5);

        // Act
        var result = await _sut.TestValidateAsync(request);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.InterestRate)
            .WithErrorMessage("Interest Rate value must be greater than 0 and less than 100.");
    }

    [Test]
    public async Task Validate_WhenCapitalizationPerYearIsOutOfRange_ShouldHaveValidationError()
    {
        // Arrange
        var request = new UpdateDepositRequest(Guid.Empty, "ValidName", 10, 5, 13);

        // Act
        var result = await _sut.TestValidateAsync(request);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.CapitalizationPerYear)
            .WithErrorMessage("Capitalization Per Year value must be greater than 0 and less than 12.");
    }
    
    [Test]
    public async Task Validate_WhenDepositDoesNotExists_ShouldHaveValidationError()
    {
        // Arrange
        var request = new UpdateDepositRequest(Guid.Empty, "ValidName", 100, 5, 1);
        _repository.ExistsAsync(Guid.Empty, Arg.Any<CancellationToken>()).Returns(false);
        
        // Act
        var result = await _sut.TestValidateAsync(request);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Id)
            .WithErrorCode(ValidationErrorCodes.EntityNotFound);
    }
    
    [Test]
    public async Task Validate_ShouldReturnValid()
    {
        // Arrange
        var request = new UpdateDepositRequest(Guid.Empty, "ValidName", 100, 5, 1);
        _repository.ExistsAsync(Guid.Empty, Arg.Any<CancellationToken>()).Returns(true);
        
        // Act
        var result = await _sut.TestValidateAsync(request);

        // Assert
        result.ShouldNotHaveAnyValidationErrors();
    }
}