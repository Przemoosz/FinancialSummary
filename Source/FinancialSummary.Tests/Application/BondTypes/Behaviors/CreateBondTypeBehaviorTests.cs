namespace FinancialSummary.Tests.Application.BondTypes.Behaviors
{
    using System.Net;
    using System.Threading;
    using System.Threading.Tasks;
    using FinancialSummary.Application.BondTypes.Behaviors;
    using FinancialSummary.Application.BondTypes.Requests;
    using FinancialSummary.Application.Result;
    using Categories;
    using FluentAssertions;
    using FluentValidation;
    using FluentValidation.Results;
    using MediatR;
    using Microsoft.Extensions.Logging;
    using NSubstitute;
    using NUnit.Framework;
    using static TddXt.AnyRoot.Root;
    
    [Parallelizable, ApplicationLayerTests]
    public class CreateBondTypeBehaviorTests
    {
        private IValidator<CreateBondTypeRequest> _validator;
        private ILogger<CreateBondTypeBehavior> _logger;
        private IPipelineBehavior<CreateBondTypeRequest, OperationResult> _sut;
        private RequestHandlerDelegate<OperationResult> _delegate;

        [SetUp]
        public void SetUp()
        {
            _validator = Substitute.For<IValidator<CreateBondTypeRequest>>();
            _logger = Substitute.For<ILogger<CreateBondTypeBehavior>>();
            _delegate = Substitute.For<RequestHandlerDelegate<OperationResult>>();
        
            _sut = new CreateBondTypeBehavior(_validator, _logger);
        }

        [Test]
        public async Task Handle_WhenValidationFails_ShouldReturnOperationFailed()
        {
            // Arrange
            CreateBondTypeRequest request = Any.Instance<CreateBondTypeRequest>();
            var validationResult = new ValidationResult(new[] { new ValidationFailure("Name", "Invalid name") });
            _validator.ValidateAsync(request).Returns(Task.FromResult(validationResult));

            // Act
            var result = await _sut.Handle(request, _delegate, CancellationToken.None);

            // Assert
            result.IsT0.Should().BeTrue();
            result.AsT0.ErrorMessage.Should().NotBeEmpty();
            result.AsT0.FailureReason.Should().Contain("Request Validation Error");
            result.AsT0.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            _logger.Received(1).LogWarning($"Request Validation Error. {validationResult.ToString(" ")}");
        }

        [Test]
        public async Task Handle_WhenValidationSucceeds_ShouldCallNext()
        {
            // Arrange
            CreateBondTypeRequest request = Any.Instance<CreateBondTypeRequest>();
            var validationResult = new ValidationResult();
            _validator.ValidateAsync(request).Returns(Task.FromResult(validationResult));
            _delegate.Invoke().Returns(new OperationSuccessful(null));

            // Act
            var result = await _sut.Handle(request, _delegate, CancellationToken.None);
        
            // Assert
            result.IsT1.Should().BeTrue();
        }
    }
}