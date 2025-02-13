namespace FinancialSummary.Tests.Application.BondTypes.Validators
{
    using FinancialSummary.Application.BondTypes.Requests;
    using FinancialSummary.Application.BondTypes.Validators;
    using FluentValidation.TestHelper;
    using Categories;
    using FinancialSummary.Domain.Enums.BondTypes;

    [TestFixture, Parallelizable, ApplicationLayerTests]
    public class CreateBondTypeValidatorTests
    {
        private CreateBondTypeValidator _validator;
        
        [SetUp]
        public void SetUp()
        {
            _validator = new CreateBondTypeValidator();
        }

        [Test]
        public void Validate_ShouldReturnValid()
        {
            // Arrange
            var request = new CreateBondTypeRequest(
                Guid.NewGuid(),
                BondTypes.ROR,
                2021,
                2,
                1.5m,
                5
            );

            // Act
            var result = _validator.TestValidate(request);

            // Assert
            result.ShouldNotHaveAnyValidationErrors();
        }
        
        [Test]
        public void Validate_WhenProfitIsLessThan0_ShouldHaveValidationError()
        {
            // Arrange
            var request = new CreateBondTypeRequest(
                Guid.NewGuid(),
                BondTypes.ROR,
                2021,
                2,
                1.5m,
                -0
            );

            // Act
            var result = _validator.TestValidate(request);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.Profit)
                .WithErrorMessage("Profit can not be value higher or equal 100 or less than 0.");
        }

        [Test]
        public void Validate_WhenProfitIsGreaterThanOrEqualTo100_ShouldHaveValidationError()
        {
            // Arrange
            var request = new CreateBondTypeRequest(
                Guid.NewGuid(),
                BondTypes.ROR,
                2021,
                2,
                1.5m,
                100
            );

            // Act
            var result = _validator.TestValidate(request);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.Profit)
                .WithErrorMessage("Profit can not be value higher or equal 100 or less than 0.");
        }

        [Test]
        public void Validate_WhenFirstYearInterestRateIsLessThan0_ShouldHaveValidationError()
        {
            // Arrange
            var request = new CreateBondTypeRequest(
                Guid.NewGuid(),
                BondTypes.ROR,
                2021,
                2,
                -1,
                50
            );

            // Act
            var result = _validator.TestValidate(request);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.FirstYearInterestRate)
                .WithErrorMessage("FirstYearInterestRate can not be value higher or equal 100 or less than 0.");
        }

        [Test]
        public void Validate_WhenFirstYearInterestRateIsGreaterThanOrEqualTo100_ShouldHaveValidationError()
        {
            // Arrange
            var request = new CreateBondTypeRequest(
                Guid.NewGuid(),
                BondTypes.ROR,
                2021,
                2,
                100,
                50
            );

            // Act
            var result = _validator.TestValidate(request);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.FirstYearInterestRate)
                .WithErrorMessage("FirstYearInterestRate can not be value higher or equal 100 or less than 0.");
        }

        [Test]
        public void Validate_WhenStartMonthIsGreaterThan12_ShouldHaveValidationError()
        {
            // Arrange
            var request = new CreateBondTypeRequest(
                Guid.NewGuid(),
                BondTypes.ROR,
                2021,
                13,
                1.5m,
                50
            );

            // Act
            var result = _validator.TestValidate(request);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.StartMonth)
                .WithErrorMessage("StartMonth can not be higher than 12");
        }

        [Test]
        public void Validate_WhenStartYearIsGreaterThanCurrentYear_ShouldHaveValidationError()
        {
            // Arrange
            var request = new CreateBondTypeRequest(
                Guid.NewGuid(),
                BondTypes.ROR,
                (uint)DateTime.Now.Year + 1,
                2,
                1.5m,
                50
            );

            // Act
            var result = _validator.TestValidate(request);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.StartYear)
                .WithErrorMessage("StartYear can not be higher than actual year.");
        }

        [Test]
        public void Validate_WhenStartYearIsLessThan2000_ShouldHaveValidationError()
        {
            // Arrange
            var request = new CreateBondTypeRequest(
                Guid.NewGuid(),
                BondTypes.ROR,
                1999,
                2,
                1.5m,
                50
            );

            // Act
            var result = _validator.TestValidate(request);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.StartYear)
                .WithErrorMessage("StartYear can not be lower than year 2000.");
        }

        [Test]
        public void Validate_WhenStartMonthIsHigherThanCurrentMonthInCurrentYear_ShouldHaveValidationError()
        {
            // Arrange
            var request = new CreateBondTypeRequest(
                Guid.NewGuid(),
                BondTypes.ROR,
                (uint)DateTime.Now.Year,
                (uint)DateTime.Now.Month + 1,
                1.5m,
                50
            );

            // Act
            var result = _validator.TestValidate(request);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.StartMonth)
                .WithErrorMessage("StartMonth can not be higher than actual month in year.");
        }
    }
}