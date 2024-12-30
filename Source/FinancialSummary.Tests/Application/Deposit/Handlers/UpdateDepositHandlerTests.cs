namespace FinancialSummary.Tests.Application.Deposit.Handlers;

using System.Net;
using Categories;
using FinancialSummary.Application.Abstraction.Deposit.Services;
using FinancialSummary.Application.Deposit.Handlers;
using FinancialSummary.Application.Deposit.Requests;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using static TddXt.AnyRoot.Root;

[ApplicationLayerTests, Parallelizable]
public class UpdateDepositHandlerTests
{
    private IDepositUpdateService _depositUpdateService;
    private ILogger<UpdateDepositHandler> _logger;
    private UpdateDepositHandler _handler;

    [SetUp]
    public void SetUp()
    {
        _depositUpdateService = Substitute.For<IDepositUpdateService>();
        _logger = Substitute.For<ILogger<UpdateDepositHandler>>();
        _handler = new UpdateDepositHandler(_depositUpdateService, _logger);
    }

    [Test]
    public async Task Handle_WhenUpdateIsSuccessful_ShouldReturnOperationSuccessful()
    {
        // Arrange
        var request = Any.Instance<UpdateDepositRequest>();
        _depositUpdateService.UpdateAsync(request.Id, request.ToUpdateEntity(), Arg.Any<CancellationToken>())
            .Returns(Task.CompletedTask);

        // Act
        var result = await _handler.Handle(request, CancellationToken.None);

        // Assert
        result.IsT1.Should().BeTrue();
        result.AsT1.Context.Should().Be(request.Id);
    }

    [Test]
    public async Task Handle_WhenExceptionThrown_ShouldReturnOperationFailed()
    {
        // Arrange
        var request = Any.Instance<UpdateDepositRequest>();
        var errorMessage = "Database error";
        _depositUpdateService.UpdateAsync(request.Id, request.ToUpdateEntity(), Arg.Any<CancellationToken>())
            .Throws(new Exception(errorMessage));

        // Act
        var result = await _handler.Handle(request, CancellationToken.None);

        // Assert
        result.IsT0.Should().BeTrue();
        result.AsT0.FailureReason.Should().Be("Error when updating deposit entity");
        result.AsT0.ErrorMessage.Should().Be(errorMessage);
        result.AsT0.StatusCode.Should().Be(HttpStatusCode.InternalServerError);
    }
}