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
		private readonly ICpiProvider _cpiProvider;

		public BondTypeController(IMediator mediator, IProblemDetailsFactory problemDetailsFactory, ICpiProvider cpiProvider)
		{
			_mediator = mediator;
			_problemDetailsFactory = problemDetailsFactory;
			_cpiProvider = cpiProvider;
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

		[HttpGet("/cpi")]
		public async Task<IActionResult> GetCPI()
		{
			var result = await _cpiProvider.GetCpiAsync(1, 2024);
			return Ok(result);
		}
	}
}