namespace FinancialSummary.Tests.Application.Deposit.Validators;

using FinancialSummary.Application.Deposit.Requests;
using FinancialSummary.Application.Deposit.Validators;
using Categories;
using FluentValidation.TestHelper;

[Parallelizable, ApplicationLayerTests]
public class DepositCreateRequestValidatorTests
{
    private DepositCreateRequestValidator _sut;

    [SetUp]
    public void SetUp()
    {
        _sut = new DepositCreateRequestValidator();
    }

    [Test]
    public void Validate_WhenCashIsLessThanOrEqualToZero_ShouldHaveValidationError()
    {
        // Arrange
        var request = new CreateDepositRequest( "ValidName", 0, 5, 5, DateTime.Now, DateTime.Now.AddYears(1));

        // Act
        var result = _sut.TestValidate(request);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Cash)
            .WithErrorMessage("Deposit cash can not be less or equal to zero.");
    }

    [TestCase("")]
    [TestCase(null)]
    public void Validate_WhenNameIsNullOrEmpty_ShouldHaveValidationError(string name)
    {
        // Arrange
        var request = new CreateDepositRequest(name, 100,5, 5, DateTime.Now, DateTime.Now.AddYears(1));

        // Act
        var result = _sut.TestValidate(request);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Name)
            .WithErrorMessage("Name can not be null or empty.");
    }

    [Test]
    public void Validate_WhenNameExceedsMaxLength_ShouldHaveValidationError()
    {
        // Arrange
        var request = new CreateDepositRequest( new string('a', 51),100, 5, 5, DateTime.Now, DateTime.Now.AddYears(1));

        // Act
        var result = _sut.TestValidate(request);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Name)
            .WithErrorMessage("Name maximum length can not be greater than 50 characters.");
    }

    [Test]
    public void Validate_WhenInterestRateIsOutOfRange_ShouldHaveValidationError()
    {
        // Arrange
        var request = new CreateDepositRequest( "ValidName", 100,101, 5, DateTime.Now, DateTime.Now.AddYears(1));

        // Act
        var result = _sut.TestValidate(request);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.InterestRate)
            .WithErrorMessage("Interest Rate value must be greater than 0 and less than 100.");
    }

    [Test]
    public void Validate_WhenCapitalizationPerYearIsOutOfRange_ShouldHaveValidationError()
    {
        // Arrange
        var request = new CreateDepositRequest("ValidName", 100, 5, 13, DateTime.Now, DateTime.Now.AddYears(1));

        // Act
        var result = _sut.TestValidate(request);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.CapitalizationPerYear)
            .WithErrorMessage("Capitalization Per Year value must be greater than 0 and less than 12.");
    }

    [Test]
    public void Validate_WhenStartDateIsOutOfRange_ShouldHaveValidationError()
    {
        // Arrange
        var request = new CreateDepositRequest("ValidName",100,  5, 5, new DateTime(1999, 12, 31), DateTime.Now.AddYears(1));

        // Act
        var result = _sut.TestValidate(request);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.StartDate)
            .WithErrorMessage("Start Date Year can not be earlier than 2000.");
    }
    
    [Test]
    public void Validate_WhenStartDateIsInFuture_ShouldHaveValidationError()
    {
        // Arrange
        var request = new CreateDepositRequest("ValidName",100,  5, 5, DateTime.Now.AddYears(1), DateTime.Now.AddYears(1));

        // Act
        var result = _sut.TestValidate(request);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.StartDate)
            .WithErrorMessage("Start Date can not be in future.");
    }
    
    [Test]
    public void Validate_WhenFinishDateIsBeforeStartDate_ShouldHaveValidationError()
    {
        // Arrange
        var request = new CreateDepositRequest( "ValidName", 100,5, 5, DateTime.Now, DateTime.Now.AddDays(-1));

        // Act
        var result = _sut.TestValidate(request);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.FinishDate)
            .WithErrorMessage("Finish Date can not be before start Date.");
    }
}