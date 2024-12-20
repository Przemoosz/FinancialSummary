namespace FinancialSummary.Tests.Application.Deposit.Behaviours;

using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Categories;
using FinancialSummary.Application.Deposit.Behaviours;
using FinancialSummary.Application.Deposit.Requests;
using FinancialSummary.Application.Result;
using FluentAssertions;
using FluentValidation;
using FluentValidation.Results;
using MediatR;
using Microsoft.Extensions.Logging;
using NSubstitute;
using NUnit.Framework;

[Parallelizable, ApplicationLayerTests]
public class DeleteDepositBehaviorTests
{
    private IValidator<DeleteDepositRequest> _validator;
    private ILogger<DeleteDepositBehavior> _logger;
    private IPipelineBehavior<DeleteDepositRequest, OperationResult> _sut;
    private RequestHandlerDelegate<OperationResult> _delegate;

    [SetUp]
    public void SetUp()
    {
        _validator = Substitute.For<IValidator<DeleteDepositRequest>>();
        _logger = Substitute.For<ILogger<DeleteDepositBehavior>>();
        _delegate = Substitute.For<RequestHandlerDelegate<OperationResult>>();
        
        _sut = new DeleteDepositBehavior(_validator, _logger);
    }

    [Test]
    public async Task Handle_WhenValidationFails_ShouldReturnOperationFailed()
    {
        // Arrange
        var request = new DeleteDepositRequest(Guid.NewGuid());
        var validationResult = new ValidationResult(new[] { new ValidationFailure("Id", "Invalid Id") });
        _validator.ValidateAsync(request).Returns(Task.FromResult(validationResult));

        // Act
        var result = await _sut.Handle(request, _delegate, CancellationToken.None);

        // Assert
        result.IsT0.Should().BeTrue();
        result.AsT0.FailureReason.Should().Be("Deposit does not exists");
        result.AsT0.ErrorMessage.Should().NotBeEmpty();
        result.AsT0.StatusCode.Should().Be(HttpStatusCode.NotFound);
        _logger.Received(1).LogWarning(validationResult.ToString());
    }

    [Test]
    public async Task Handle_WhenValidationSucceeds_ShouldCallNext()
    {
        // Arrange
        var request = new DeleteDepositRequest(Guid.NewGuid());
        var validationResult = new ValidationResult();
        _validator.ValidateAsync(request).Returns(Task.FromResult(validationResult));
        _delegate.Invoke().Returns(new OperationSuccessful(null));
        
        // Act
        var result = await _sut.Handle(request, _delegate, CancellationToken.None);

        // Assert
        result.IsT1.Should().BeTrue();
    }
}