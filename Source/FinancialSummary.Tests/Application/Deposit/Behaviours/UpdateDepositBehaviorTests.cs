namespace FinancialSummary.Tests.Application.Deposit.Behaviours
{
	using System.Net;
	using Categories;
	using FinancialSummary.Application;
	using FinancialSummary.Application.Deposit.Behaviors;
	using FinancialSummary.Application.Deposit.Requests;
	using FinancialSummary.Application.Result;
	using FluentAssertions;
	using FluentValidation;
	using FluentValidation.Results;
	using MediatR;
	using Microsoft.Extensions.Logging;
	using NSubstitute;

	[Parallelizable, ApplicationLayerTests]
	public class UpdateDepositBehaviorTests
	{
		private IValidator<UpdateDepositRequest> _validator;
		private RequestHandlerDelegate<OperationResult> _delegate;
		private ILogger<UpdateDepositBehavior> _logger;
		private UpdateDepositBehavior _sut;

		[SetUp]
		public void SetUp()
		{
			_validator = Substitute.For<IValidator<UpdateDepositRequest>>();
			_logger = Substitute.For<ILogger<UpdateDepositBehavior>>();
			_delegate = Substitute.For<RequestHandlerDelegate<OperationResult>>();
			_sut = new UpdateDepositBehavior(_validator, _logger);
		}

		[Test]
		public async Task Handle_WhenValidationSucceeds_ShouldCallNext()
		{
			// Arrange
			var request = new UpdateDepositRequest( Guid.NewGuid(),"ValidName",100, 5, 5);
			var validationResult = new ValidationResult();
			_validator.ValidateAsync(request).Returns(Task.FromResult(validationResult));
			_delegate.Invoke().Returns(new OperationSuccessful(null));

			// Act
			var result = await _sut.Handle(request, _delegate, CancellationToken.None);
        
			// Assert
			result.IsT1.Should().BeTrue();
		}
		
		[Test]
		public async Task Handle_WhenValidationFails_ShouldReturnOperationFailed()
		{
			// Arrange
			var request = new UpdateDepositRequest( Guid.NewGuid(),"ValidName",100, 5, 5);
			var validationResult = new ValidationResult(new[] { new ValidationFailure("Name", "Invalid name"){ErrorCode = "None"} });
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
		public async Task Handle_WhenDepositDoesNotExists_ShouldReturnOperationFailed()
		{
			// Arrange
			var request = new UpdateDepositRequest( Guid.NewGuid(),"ValidName",100, 5, 5);
			var validationResult = new ValidationResult(new[] { new ValidationFailure("Name", "Invalid name"){ErrorCode = ValidationErrorCodes.EntityNotFound} });
			_validator.ValidateAsync(request).Returns(Task.FromResult(validationResult));

			// Act
			var result = await _sut.Handle(request, _delegate, CancellationToken.None);

			// Assert
			result.IsT0.Should().BeTrue();
			result.AsT0.ErrorMessage.Should().NotBeEmpty();
			result.AsT0.FailureReason.Should().Contain("Deposit does not exists");
			result.AsT0.StatusCode.Should().Be(HttpStatusCode.NotFound);
		}
	}
}