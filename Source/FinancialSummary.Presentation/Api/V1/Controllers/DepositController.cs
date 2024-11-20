namespace FinancialSummary.Presentation.Api.V1.Controllers;

using System.Diagnostics.CodeAnalysis;
using System.Net;
using Abstraction.Factories;
using Application.Deposit.Queries;
using Application.Deposit.Requests;
using Application.Result;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Logging;
using Requests;

[ExcludeFromCodeCoverage]
[Route("/V1/[controller]")]
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
	public async Task<IActionResult> GetDeposit([FromRoute(Name = "Id")] Guid id)
	{
		GetDepositGetByIdQuery getByIdQuery = new GetDepositGetByIdQuery(id);
		
		DepositEntity entity = await _mediator.Send(getByIdQuery);
		
		return Ok(entity);
	}
	
	[HttpDelete("{Id:guid}")]
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
	
	[HttpPut]
	public async Task<IActionResult> GetDeposit([FromBody(EmptyBodyBehavior = EmptyBodyBehavior.Disallow)] CreateDepositRequestBody createDepositRequestBody)
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
}