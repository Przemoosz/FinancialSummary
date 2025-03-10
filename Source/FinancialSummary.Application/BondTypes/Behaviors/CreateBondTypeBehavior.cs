namespace FinancialSummary.Application.BondTypes.Behaviors
{
	using System.Net;
	using FluentValidation;
	using FluentValidation.Results;
	using MediatR;
	using Microsoft.Extensions.Logging;
	using Requests;
	using Result;

	internal sealed class CreateBondTypeBehavior: IPipelineBehavior<CreateBondTypeRequest, OperationResult>
	{
		private readonly IValidator<CreateBondTypeRequest> _validator;
		private readonly ILogger<CreateBondTypeBehavior> _logger;

		public CreateBondTypeBehavior(IValidator<CreateBondTypeRequest> validator, ILogger<CreateBondTypeBehavior> logger)
		{
			_validator = validator;
			_logger = logger;
		}
		public async Task<OperationResult> Handle(CreateBondTypeRequest request, RequestHandlerDelegate<OperationResult> next, CancellationToken cancellationToken)
		{
			ValidationResult validationResult = await _validator.ValidateAsync(request, cancellationToken);
		
			if (!validationResult.IsValid)
			{
				_logger.LogWarning($"Request Validation Error. {validationResult.ToString(" ")}");
				return new OperationFailed("Request Validation Error", validationResult.ToString(" "), HttpStatusCode.BadRequest);
			}

			var result = await next();

			return result;
		}
	}
}