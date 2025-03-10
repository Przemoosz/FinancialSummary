namespace FinancialSummary.Tests.Application.Deposit.Handlers;

using System.Net;
using Categories;
using FinancialSummary.Application.Deposit.Handlers;
using FinancialSummary.Application.Deposit.Queries;
using FinancialSummary.Domain.Entities.Deposit;
using FinancialSummary.Application.Contracts.Repository;
using FluentAssertions;
using NSubstitute;
using static TddXt.AnyRoot.Root;
using NSubstitute.ExceptionExtensions;

[ApplicationLayerTests, Parallelizable]
public class GetDepositByIdHandlerTests
{
    private IRepository<Guid, DepositEntity> _repository;
    private GetDepositByIdHandler _handler;

    [SetUp]
    public void SetUp()
    {
        _repository = Substitute.For<IRepository<Guid, DepositEntity>>();
        _handler = new GetDepositByIdHandler(_repository);
    }

    [Test]
    public async Task Handle_WhenEntityExists_ShouldReturnOperationSuccessful()
    {
        // Arrange
        var query = new GetDepositGetByIdQuery(Guid.NewGuid());
        var depositEntity = Any.Instance<DepositEntity>();
        _repository.GetByKeyAsync(query.Id, Arg.Any<CancellationToken>()).Returns(depositEntity);

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        result.IsT1.Should().BeTrue();
        result.AsT1.Context.Should().Be(depositEntity);
    }

    [Test]
    public async Task Handle_WhenEntityDoesNotExist_ShouldReturnOperationFailed()
    {
        // Arrange
        var query = new GetDepositGetByIdQuery(Guid.NewGuid());
        _repository.GetByKeyAsync(query.Id, Arg.Any<CancellationToken>()).Returns((DepositEntity)null);

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        result.IsT0.Should().BeTrue();
        result.AsT0.FailureReason.Should().Be("Entity does not exists");
        result.AsT0.ErrorMessage.Should().Be($"Entity with id: {query.Id} does not exists");
        result.AsT0.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    [Test]
    public async Task Handle_WhenExceptionThrown_ShouldReturnOperationFailed()
    {
        // Arrange
        var query = new GetDepositGetByIdQuery(Guid.NewGuid());
        var errorMessage = "Database error";
        _repository.GetByKeyAsync(query.Id, Arg.Any<CancellationToken>()).Throws(new Exception(errorMessage));

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        result.IsT0.Should().BeTrue();
        result.AsT0.FailureReason.Should().Be("Failed to query entity.");
        result.AsT0.ErrorMessage.Should().Be(errorMessage);
        result.AsT0.StatusCode.Should().Be(HttpStatusCode.InternalServerError);
    }
}