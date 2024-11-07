namespace FinancialSummary.Presentation.Api.V1.Controllers;

using Application.Deposit.Commands.Handlers;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;

[Route("/V1/[controller]")]
public sealed class DepositController: ControllerBase
{
	private readonly IMediator _mediator;

	public DepositController(IMediator mediator)
	{
		_mediator = mediator;
	}

	[HttpGet]
	public async Task<IActionResult> GetDeposit()
	{
		QueryById<DepositEntity> queryById = new QueryById<DepositEntity>(Guid.Empty);
		var entity = await _mediator.Send(queryById);
		
		return Ok(entity);
	}
}