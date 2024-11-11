namespace FinancialSummary.Presentation.Builders;

using System.Net;
using FinancialSummary.Presentation.Abstraction.Builders;
using Microsoft.AspNetCore.Mvc;

internal sealed class ProblemDetailsBuilder: IProblemDetailsBuilderBase, 
	IProblemDetailsBuilderWithTitle, 
	IProblemDetailsBuilderWithDetails,
	IProblemDetailsBuilderWithStatusCode
{
	private ProblemDetails _problemDetails = new ProblemDetails();
	
	public static IProblemDetailsBuilderBase Create()
	{
		return new ProblemDetailsBuilder();
	}

	public IProblemDetailsBuilderWithTitle WithTitle(string title)
	{
		_problemDetails.Title = title;
		return this;
	}
	
	public IProblemDetailsBuilderWithDetails WithDetails(string details)
	{
		_problemDetails.Detail = details;
		return this;
	}

	public IProblemDetailsBuilderWithStatusCode WithExtension(string key, object value)
	{
		_problemDetails.Extensions.Add(key, value);
		return this;
	}
	
	public IProblemDetailsBuilderWithStatusCode ForStatusCode(HttpStatusCode statusCode)
	{
		_problemDetails.Status = (int) statusCode;
		switch (statusCode)
		{
			case HttpStatusCode.BadRequest:
				_problemDetails.Type = "https://datatracker.ietf.org/doc/html/rfc9110#name-400-bad-request";
				break;
			default:
				_problemDetails.Type = "about:blank";
				break;
		}
		return this;
	}

	public ProblemDetails Build()
	{
		return _problemDetails;
	}
}