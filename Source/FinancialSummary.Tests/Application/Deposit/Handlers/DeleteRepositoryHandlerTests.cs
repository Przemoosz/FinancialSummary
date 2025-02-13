namespace FinancialSummary.Tests.Application.Deposit.Handlers;

using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using FinancialSummary.Application.Deposit.Handlers;
using FinancialSummary.Application.Deposit.Requests;
using FinancialSummary.Domain.Entities;
using FinancialSummary.Application.Contracts.Repository;
using FinancialSummary.Application.Result;
using FinancialSummary.Domain.Entities.Deposit;
using FluentAssertions;
using MediatR;
using Microsoft.Extensions.Logging;
using NSubstitute;
using NUnit.Framework;

[TestFixture]
public class DeleteRepositoryHandlerTests
{
    private IRepository<Guid, DepositEntity> _repository;
    private ILogger<DeleteRepositoryHandler> _logger;
    private IRequestHandler<DeleteDepositRequest, OperationResult> _sut;

    [SetUp]
    public void SetUp()
    {
        _repository = Substitute.For<IRepository<Guid, DepositEntity>>();
        _logger = Substitute.For<ILogger<DeleteRepositoryHandler>>();
        _sut = new DeleteRepositoryHandler(_repository, _logger);
    }

    [Test]
    public async Task Handle_WhenDeleteIsSuccessful_ShouldReturnOperationSuccessful()
    {
        // Arrange
        var depositId = Guid.NewGuid();
        var request = new DeleteDepositRequest(depositId);

        // Act
        var result = await _sut.Handle(request, CancellationToken.None);

        // Assert
        await _repository.Received(1).DeleteAsync(depositId, Arg.Any<CancellationToken>());
        _logger.Received(1).LogInformation($"Deposit with id {depositId} deleted.");
        result.IsT1.Should().BeTrue();
    }

    [Test]
    public async Task Handle_WhenDeleteFails_ShouldReturnOperationFailed()
    {
        // Arrange
        var depositId = Guid.NewGuid();
        var request = new DeleteDepositRequest(depositId);
        var exception = new Exception("Database error");
        _repository.When(x => x.DeleteAsync(depositId, Arg.Any<CancellationToken>())).Do(x => { throw exception; });

        // Act
        var result = await _sut.Handle(request, CancellationToken.None);

        // Assert
        await _repository.Received(1).DeleteAsync(depositId, Arg.Any<CancellationToken>());
        _logger.Received(1).LogError(exception, $"Failed to delete entity {depositId}");
        result.IsT0.Should().BeTrue();
        result.AsT0.StatusCode.Should().Be(HttpStatusCode.InternalServerError);
        result.AsT0.ErrorMessage.Should().Be(exception.Message);
        result.AsT0.FailureReason.Should().Be("Error when deleting deposit entity");
    }
}