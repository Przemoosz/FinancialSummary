namespace FinancialSummary.Application.BondTypes.Behaviors
{
	using MediatR;
	using Result;

	internal sealed class CreateBondTypeBehavior: IPipelineBehavior<CreateBondTypeBehavior, OperationResult>
	{
		public Task<OperationResult> Handle(CreateBondTypeBehavior request, RequestHandlerDelegate<OperationResult> next, CancellationToken cancellationToken)
		{
			throw new NotImplementedException();
		}
	}
}