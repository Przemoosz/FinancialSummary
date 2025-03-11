namespace FinancialSummary.Presentation.Api.V1.BondTypes.Controllers
{
	using System.Diagnostics.CodeAnalysis;
	using System.Net;
	using Abstraction.Factories;
	using Application.BondTypes.Requests;
	using Application.Contracts.Providers.Cpi;
	using Domain.Enums.BondTypes;
	using MediatR;
	using Microsoft.AspNetCore.Mvc;
	using Requests;

	[ExcludeFromCodeCoverage]
	[Route("/[controller]/V1")]
	public sealed class BondTypeController: ControllerBase
	{
		private readonly IMediator _mediator;
		private readonly IProblemDetailsFactory _problemDetailsFactory;

		public BondTypeController(IMediator mediator, IProblemDetailsFactory problemDetailsFactory)
		{
			_mediator = mediator;
			_problemDetailsFactory = problemDetailsFactory;
		}
		
		[HttpPut]
		public async Task<IActionResult> CreateBondType([FromBody]CreateBondTypeRequestBody requestBody)
		{
			CreateBondTypeRequest request = new CreateBondTypeRequest(requestBody.OperationId ?? Guid.NewGuid(), requestBody.BondType, requestBody.StartYear,
				requestBody.StartMonth, requestBody.FirstYearInterestRate, requestBody.Profit);
			var result = await _mediator.Send(request);
			return result.Match<IActionResult>(failed =>
				{
					ProblemDetails problemDetails =
						_problemDetailsFactory.Create(failed, requestBody.OperationId);
					return StatusCode((int)failed.StatusCode, problemDetails);
				},
				success => StatusCode((int) HttpStatusCode.Created, success.Context));
		}
	}
}