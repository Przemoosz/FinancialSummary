namespace FinancialSummary.Tests.Application.BondTypes.Handlers
{
    using FinancialSummary.Application.Contracts.Repository;
    using FluentAssertions;
    using BondTypes = FinancialSummary.Domain.Enums.BondTypes.BondTypes;
    using FinancialSummary.Application.BondTypes.Handlers;
    using FinancialSummary.Application.BondTypes.Requests;
    using FinancialSummary.Domain.Entities.Bonds.AntiInflationary;
    using FinancialSummary.Domain.Entities.Bonds.FixedInterest;
    using FinancialSummary.Domain.Entities.Bonds.FloatingInterest;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Logging;
    using NSubstitute;
    [TestFixture]
    public class CreateBondTypeHandlerTests
    {
        private IServiceProvider _serviceProvider;
        private ILogger<CreateBondTypeHandler> _logger;
        private CreateBondTypeHandler _handler;
#pragma warning disable NUnit1032
        private IServiceScope _scope;
#pragma warning restore NUnit1032
        private IServiceScopeFactory _scopeFactory;

        [SetUp]
        public void SetUp()
        {
            _serviceProvider = Substitute.For<IServiceProvider>();
            _logger = Substitute.For<ILogger<CreateBondTypeHandler>>();
            _handler = new CreateBondTypeHandler(_serviceProvider, _logger);

            _scope = Substitute.For<IServiceScope>();
            _scopeFactory = Substitute.For<IServiceScopeFactory>();

            _serviceProvider.GetService(typeof(IServiceScopeFactory)).Returns(_scopeFactory);
            _scopeFactory.CreateScope().Returns(_scope);
            _scope.ServiceProvider.Returns(_serviceProvider);
        }

        [Test]
        public async Task Handle_WhenBondTypeIsROR_ShouldCreateOneYearFloatingInterestBondType()
        {
            // Arrange
            var request = new CreateBondTypeRequest(Guid.NewGuid(), BondTypes.ROR, 2021, 2, 1.5m, 10m);
           
            var repository = Substitute.For<IRepository<string, OneYearFloatingInterestBondType>>();
            _serviceProvider.GetService<IRepository<string, OneYearFloatingInterestBondType>>().Returns(repository);

            // Act
            var result = await _handler.Handle(request, CancellationToken.None);

            // Assert
            await repository.Received(1).AddAsync(Arg.Any<OneYearFloatingInterestBondType>(), Arg.Any<CancellationToken>());
            result.IsT1.Should().BeTrue();
        }

        [Test]
        public async Task Handle_WhenBondTypeIsDOR_ShouldCreateTwoYearsFloatingInterestBondType()
        {
            // Arrange
            var request = new CreateBondTypeRequest(Guid.NewGuid(), BondTypes.DOR, 2021, 2, 1.5m, 10m);
            
            var repository = Substitute.For<IRepository<string, TwoYearsFloatingInterestBondType>>();
            _serviceProvider.GetService<IRepository<string, TwoYearsFloatingInterestBondType>>().Returns(repository);
            // Act
            var result = await _handler.Handle(request, CancellationToken.None);

            // Assert
            await repository.Received(1).AddAsync(Arg.Any<TwoYearsFloatingInterestBondType>(), Arg.Any<CancellationToken>());
            result.IsT1.Should().BeTrue();
        }

        [Test]
        public async Task Handle_WhenBondTypeIsTOS_ShouldCreateThreeYearsFixedInterestBondType()
        {
            // Arrange
            var request = new CreateBondTypeRequest(Guid.NewGuid(), BondTypes.TOS, 2021, 2, 1.5m, 10m);

            var repository = Substitute.For<IRepository<string, ThreeYearsFixedInterestBondType>>();
            _serviceProvider.GetService<IRepository<string, ThreeYearsFixedInterestBondType>>().Returns(repository);
            // Act
            var result = await _handler.Handle(request, CancellationToken.None);

            // Assert
            await repository.Received(1).AddAsync(Arg.Any<ThreeYearsFixedInterestBondType>(), Arg.Any<CancellationToken>());
            result.IsT1.Should().BeTrue();
        }

        [Test]
        public async Task Handle_WhenBondTypeIsCOI_ShouldCreateFourYearsAntiInflationaryBondType()
        {
            // Arrange
            var request = new CreateBondTypeRequest(Guid.NewGuid(), BondTypes.COI, 2021, 2, 1.5m, 10m);

            var repository = Substitute.For<IRepository<string, FourYearsAntiInflationaryBondType>>();
            _serviceProvider.GetService<IRepository<string, FourYearsAntiInflationaryBondType>>().Returns(repository);
            // Act
            var result = await _handler.Handle(request, CancellationToken.None);

            // Assert
            await repository.Received(1).AddAsync(Arg.Any<FourYearsAntiInflationaryBondType>(), Arg.Any<CancellationToken>());
            result.IsT1.Should().BeTrue();
        }

        [Test]
        public async Task Handle_WhenBondTypeIsEDO_ShouldCreateTenYearsAntiInflationaryBondType()
        {
            // Arrange
            var request = new CreateBondTypeRequest(Guid.NewGuid(), BondTypes.EDO, 2021, 2, 1.5m, 10m);

            var repository = Substitute.For<IRepository<string, TenYearsAntiInflationaryBondType>>();
            _serviceProvider.GetService<IRepository<string, TenYearsAntiInflationaryBondType>>().Returns(repository);
            // Act
            var result = await _handler.Handle(request, CancellationToken.None);

            // Assert
            await repository.Received(1).AddAsync(Arg.Any<TenYearsAntiInflationaryBondType>(), Arg.Any<CancellationToken>());
            result.IsT1.Should().BeTrue();
        }

        [Test]
        public async Task Handle_WhenEntityExists_ShouldThrowEntityExistsException()
        {
            // Arrange
            var request = new CreateBondTypeRequest(Guid.NewGuid(), BondTypes.ROR, 2021, 2, 1.5m, 10m);
            var repository = Substitute.For<IRepository<string, OneYearFloatingInterestBondType>>();
            _serviceProvider.GetService<IRepository<string, OneYearFloatingInterestBondType>>().Returns(repository);
            
            repository.ExistsAsync(Arg.Any<string>(), Arg.Any<CancellationToken>()).Returns(true);

            // Act
            var result = await _handler.Handle(request, CancellationToken.None);

            // Assert
            result.IsT0.Should().BeTrue();
            result.AsT0.FailureReason.Should().Be("Entity already exists");
        }
    }
}