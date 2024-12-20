namespace FinancialSummary.Application.Deposit.Handlers;

using System.Net;
using Abstraction.Deposit.Services;
using MediatR;
using Microsoft.Extensions.Logging;
using Requests;
using Result;
using Services;

internal sealed class UpdateDepositHandler: IRequestHandler<UpdateDepositRequest, OperationResult>
{
	private readonly IDepositUpdateService _depositUpdateService;
	private readonly ILogger<UpdateDepositHandler> _logger;

	public UpdateDepositHandler(IDepositUpdateService depositUpdateService, ILogger<UpdateDepositHandler> logger)
	{
		_depositUpdateService = depositUpdateService;
		_logger = logger;
	}
	
	public async Task<OperationResult> Handle(UpdateDepositRequest request, CancellationToken cancellationToken)
	{
		try
		{
			await _depositUpdateService.UpdateAsync(request.Id, request.ToUpdateEntity(), cancellationToken);
		}
		catch (Exception e)
		{
			_logger.LogError(e, $"Failed to update entity {request.Id}");
			return new OperationFailed("Error when updating deposit entity", e.Message,
				HttpStatusCode.InternalServerError);
		}

		return new OperationSuccessful(request.Id);
	}
}