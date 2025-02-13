namespace FinancialSummary.Presentation.Api.V1.BondTypes.Controllers
{
	using System.Diagnostics.CodeAnalysis;
	using Application.BondTypes.Requests;
	using Domain.Enums.BondTypes;
	using MediatR;
	using Microsoft.AspNetCore.Mvc;
	using Requests;

	[ExcludeFromCodeCoverage]
	[Route("/[controller]/V1")]
	public sealed class BondTypeController: ControllerBase
	{
		private readonly IMediator _mediator;

		public BondTypeController(IMediator mediator)
		{
			_mediator = mediator;
		}
		
		[HttpPut]
		public async Task<IActionResult> CreateBondType([FromBody] CreateBondTypeRequestBody requestBody)
		{
			CreateBondTypeRequest request = new CreateBondTypeRequest(requestBody.OperationId ?? Guid.NewGuid(), requestBody.BondType, requestBody.StartYear,
				requestBody.StartMonth, requestBody.FirstYearInterestRate, requestBody.Profit);
			await _mediator.Send(request);
			return null;
		}
		
	}
}