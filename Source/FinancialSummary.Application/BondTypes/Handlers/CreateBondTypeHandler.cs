namespace FinancialSummary.Application.BondTypes.Handlers
{
	using Contracts.Repository;
	using Domain.Abstract.Entities;
	using Domain.Entities.Bonds.AntiInflationary;
	using Domain.Entities.Bonds.FixedInterest;
	using Domain.Entities.Bonds.FloatingInterest;
	using Domain.Enums.BondTypes;
	using MediatR;
	using Microsoft.Extensions.DependencyInjection;
	using Microsoft.Extensions.Logging;
	using Requests;
	using Result;

	internal class CreateBondTypeHandler: IRequestHandler<CreateBondTypeRequest, OperationResult>
	{
		private readonly IServiceProvider _serviceProvider;
		private readonly ILogger<CreateBondTypeHandler> _logger;

		public CreateBondTypeHandler(IServiceProvider serviceProvider, ILogger<CreateBondTypeHandler> logger)
		{
			_serviceProvider = serviceProvider;
			_logger = logger;
		}
		
		public async Task<OperationResult> Handle(CreateBondTypeRequest request, CancellationToken cancellationToken)
		{
			await using var scope = _serviceProvider.CreateAsyncScope();

			switch (request.BondType)
			{
				case BondTypes.ROR:
					var rorEntity = new OneYearFloatingInterestBondType(request.StartYear, request.StartMonth,
						request.FirstYearInterestRate, request.Profit);
					await CreateAsync(rorEntity, scope, cancellationToken);
					return new OperationSuccessful(new { EntityId = rorEntity.Id });
				
				case BondTypes.DOR:
					var dorEntity = new TwoYearsFloatingInterestBondType(request.StartYear, request.StartMonth,
						request.FirstYearInterestRate, request.Profit);
					await CreateAsync(dorEntity, scope, cancellationToken);
					return new OperationSuccessful(new { EntityId = dorEntity.Id });
				
				case BondTypes.TOS:
					var tosEntity = new ThreeYearsFixedInterestBondType(request.StartYear, request.StartMonth, request.FirstYearInterestRate);
					await CreateAsync(tosEntity, scope, cancellationToken);
					return new OperationSuccessful(new { EntityId = tosEntity.Id });
				
				case BondTypes.COI:
					var coiEntity = new FourYearsAntiInflationaryBondType(request.StartYear, request.StartMonth, request.FirstYearInterestRate, request.Profit);
					await CreateAsync(coiEntity, scope, cancellationToken);
					return new OperationSuccessful(new { EntityId = coiEntity.Id });
				
				case BondTypes.EDO:
					var edoEntity = new TenYearsAntiInflationaryBondType(request.StartYear, request.StartMonth, request.FirstYearInterestRate, request.Profit);
					await CreateAsync(edoEntity, scope, cancellationToken);
					return new OperationSuccessful(new { EntityId = edoEntity.Id });
				default:
					throw new ArgumentException($"BondType: {request.BondType} does not exists.");
			}
		}


		private async Task CreateAsync<TBond>(TBond bondType, IServiceScope scope, CancellationToken cancellationToken) where TBond: BondTypeBase
		{
			var repository = scope.ServiceProvider.GetService<IRepository<string, TBond>>();
			await repository.AddAsync(bondType, cancellationToken);
		}
		
	}
}