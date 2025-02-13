namespace FinancialSummary.Presentation.Api.V1.Deposit.Controllers;

using System.Diagnostics.CodeAnalysis;
using System.Net;
using FinancialSummary.Application.Deposit.Queries;
using FinancialSummary.Application.Deposit.Requests;
using FinancialSummary.Application.Result;
using FinancialSummary.Domain.Entities.Deposit;
using FinancialSummary.Presentation.Abstraction.Factories;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Logging;
using Requests;

[ExcludeFromCodeCoverage]
[Route("/[controller]/V1")]
public sealed class DepositController: ControllerBase
{
	private readonly IMediator _mediator;
	private readonly ILogger<DepositController> _logger;
	private readonly IProblemDetailsFactory _problemDetailsFactory;

	public DepositController(IMediator mediator, ILogger<DepositController> logger, IProblemDetailsFactory problemDetailsFactory)
	{
		_mediator = mediator;
		_logger = logger;
		_problemDetailsFactory = problemDetailsFactory;
	}
	
	[HttpGet("{Id:guid}")]
	[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(DepositEntity))]
	[ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ProblemDetails))]
	[ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
	public async Task<IActionResult> GetDeposit([FromRoute(Name = "Id")] Guid id)
	{
		Guid operationId = Guid.NewGuid();
		GetDepositGetByIdQuery getByIdQuery = new GetDepositGetByIdQuery(id);
		
		var result = await _mediator.Send(getByIdQuery);
		
		return result.Match<IActionResult>(failed =>
			{
				ProblemDetails problemDetails =
					_problemDetailsFactory.Create(failed, operationId);
				return StatusCode((int)failed.StatusCode, problemDetails);
			},
			success => StatusCode((int) HttpStatusCode.Created, success.Context));
	}	
	
	[HttpDelete("{Id:guid}")]
	[ProducesResponseType(StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ProblemDetails))]
	[ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
	public async Task<IActionResult> DeleteDeposit([FromRoute(Name = "Id")] Guid id)
	{
		DeleteDepositRequest getByIdQuery = new DeleteDepositRequest(id);
		
		var result = await _mediator.Send(getByIdQuery);
		
		return result.Match<IActionResult>(failed =>
			{
				ProblemDetails problemDetails =
					_problemDetailsFactory.Create(failed, Guid.NewGuid());
				return StatusCode((int)failed.StatusCode, problemDetails);
			},
			_ => Ok());
	}
	
	[HttpPost]
	[ProducesResponseType(StatusCodes.Status201Created, Type = typeof(DepositEntity))]
	[ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
	[ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
	public async Task<IActionResult> AddDeposit([FromBody(EmptyBodyBehavior = EmptyBodyBehavior.Disallow)] CreateDepositRequestBody createDepositRequestBody)
	{
		using (_logger.BeginScope(new Dictionary<string, object>() {{"OperationId", createDepositRequestBody.OperationId.GetValueOrDefault()}}))
		{
			CreateDepositRequest createDepositRequest = new CreateDepositRequest(createDepositRequestBody.Name,
				createDepositRequestBody.Cash,
				createDepositRequestBody.InterestRate,
				createDepositRequestBody.CapitalizationPerYear,
				createDepositRequestBody.StartDate,
				createDepositRequestBody.FinishDate);

			OperationResult result = await _mediator.Send(createDepositRequest);

			return result.Match<IActionResult>(failed =>
				{
					ProblemDetails problemDetails =
						_problemDetailsFactory.Create(failed, createDepositRequestBody.OperationId);
					return StatusCode((int)failed.StatusCode, problemDetails);
				},
				success => StatusCode((int) HttpStatusCode.Created, success.Context));
		}
	}
	
	[HttpPatch("{Id:guid}")]
	[ProducesResponseType(StatusCodes.Status201Created, Type = typeof(DepositEntity))]
	[ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
	[ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
	public async Task<IActionResult> UpdateDeposit([FromBody(EmptyBodyBehavior = EmptyBodyBehavior.Allow)] UpdateDepositRequestBody updateDepositRequestBody, [FromRoute(Name = "Id")] Guid id)
	{
		using (_logger.BeginScope(new Dictionary<string, object>() {{"OperationId", updateDepositRequestBody.OperationId.GetValueOrDefault()}}))
		{
			UpdateDepositRequest createDepositRequest = new UpdateDepositRequest(
				id,
				updateDepositRequestBody.Name,
				updateDepositRequestBody.Cash,
				updateDepositRequestBody.InterestRate,
				updateDepositRequestBody.CapitalizationPerYear);

			OperationResult result = await _mediator.Send(createDepositRequest);

			return result.Match<IActionResult>(failed =>
				{
					ProblemDetails problemDetails =
						_problemDetailsFactory.Create(failed, updateDepositRequestBody.OperationId);
					return StatusCode((int)failed.StatusCode, problemDetails);
				},
				success => StatusCode((int) HttpStatusCode.OK, success.Context));
		}
	}
}