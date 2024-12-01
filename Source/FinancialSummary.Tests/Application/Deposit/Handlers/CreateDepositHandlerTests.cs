namespace FinancialSummary.Tests.Application.Deposit.Handlers;

using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Categories;
using FinancialSummary.Application.Deposit.Handlers;
using FinancialSummary.Application.Deposit.Requests;
using FinancialSummary.Application.Contracts.Repository;
using FinancialSummary.Application.Result;
using FinancialSummary.Domain.Abstract.Factories;
using FinancialSummary.Domain.Entities;
using FluentAssertions;
using MediatR;
using Microsoft.Extensions.Logging;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using NUnit.Framework;
using TddXt.AnyRoot.Strings;
using static TddXt.AnyRoot.Root;

[Parallelizable, ApplicationLayerTests]
public class CreateDepositHandlerTests
{
    private IRepository<DepositEntity> _repository;
    private IDepositEntityFactory _depositEntityFactory;
    private ILogger<CreateDepositRequest> _logger;
    private IRequestHandler<CreateDepositRequest, OperationResult> _handler;

    [SetUp]
    public void SetUp()
    {
        _repository = Substitute.For<IRepository<DepositEntity>>();
        _depositEntityFactory = Substitute.For<IDepositEntityFactory>();
        _logger = Substitute.For<ILogger<CreateDepositRequest>>();
        _handler = new CreateDepositHandler(_repository, _depositEntityFactory, _logger);
    }

    [Test]
    public async Task Handle_WhenRequestIsValid_ShouldCreateDeposit()
    {
        // Arrange
        var request = Any.Instance<CreateDepositRequest>();
        var depositEntity = Any.Instance<DepositEntity>();
        _depositEntityFactory.Create(request.Name, request.Cash, request.InterestRate, request.CapitalizationPerYear, request.StartDate, request.FinishDate)
            .Returns(depositEntity);

        // Act
        var result = await _handler.Handle(request, CancellationToken.None);

        // Assert
        await _repository.Received(1).AddAsync(depositEntity, Arg.Any<CancellationToken>());
        _logger.Received(1).LogInformation($"Created entity {depositEntity.Id}");
        result.IsT1.Should().BeTrue();
    }

    [Test]
    public async Task Handle_WhenRepositoryFails_ShouldReturnOperationFailed()
    {
        // Arrange
        var request = Any.Instance<CreateDepositRequest>();
        var depositEntity = Any.Instance<DepositEntity>();
        var errorMessage = Any.String();
        _depositEntityFactory.Create(request.Name, request.Cash, request.InterestRate, request.CapitalizationPerYear, request.StartDate, request.FinishDate)
            .Returns(depositEntity);
        _repository.AddAsync(depositEntity, Arg.Any<CancellationToken>()).ThrowsAsync(new Exception(errorMessage));

        // Act
        var result = await _handler.Handle(request, CancellationToken.None);

        // Assert
        result.IsT0.Should().Be(true);
        result.AsT0.FailureReason.Should().Be("Error when adding deposit to entity");
        result.AsT0.ErrorMessage.Should().Be(errorMessage);
        result.AsT0.StatusCode.Should().Be(HttpStatusCode.InternalServerError);
    }
}