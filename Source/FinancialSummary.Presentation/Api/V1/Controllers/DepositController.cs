namespace FinancialSummary.Presentation.Api.V1.Controllers;

using Application.Deposit.Queries;
using Application.Deposit.Requests;
using Application.Result;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Logging;
using Requests;

[Route("/V1/[controller]")]
public sealed class DepositController: ControllerBase
{
	private readonly IMediator _mediator;
	private readonly ILogger<DepositController> _logger;

	public DepositController(IMediator mediator, ILogger<DepositController> logger)
	{
		_mediator = mediator;
		_logger = logger;
	}
	
	[HttpGet("{Id:guid}")]
	public async Task<IActionResult> GetDeposit([FromRoute(Name = "Id")] Guid id)
	{
		GetDepositGetByIdQuery getByIdQuery = new GetDepositGetByIdQuery(id);
		
		var entity = await _mediator.Send(getByIdQuery);
		
		return Ok(entity);
	}
	
	[HttpPut]
	public async Task<IActionResult> GetDeposit([FromBody(EmptyBodyBehavior = EmptyBodyBehavior.Disallow)] CreateDepositRequestBody createDepositRequestBody)
	{
		using (_logger.BeginScope(new Dictionary<string, object>() {{"OperationId", createDepositRequestBody.OperationId.GetValueOrDefault()}}))
		{
			_logger.LogInformation("aaaaaaaaaaaa");
			CreateDepositRequest createDepositRequest = new CreateDepositRequest(createDepositRequestBody.Name,
				createDepositRequestBody.Cash,
				createDepositRequestBody.InterestRate,
				createDepositRequestBody.CapitalizationPerYear,
				createDepositRequestBody.StartDate,
				createDepositRequestBody.FinishDate);

			OperationResult result = await _mediator.Send(createDepositRequest);
			return Ok(result.AsT1);
		}
	}
}