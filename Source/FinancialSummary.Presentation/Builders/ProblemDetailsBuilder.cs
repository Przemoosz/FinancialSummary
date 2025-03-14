namespace FinancialSummary.Presentation.Builders;

using System.Diagnostics.CodeAnalysis;
using System.Net;
using FinancialSummary.Presentation.Abstraction.Builders;
using Microsoft.AspNetCore.Mvc;

[ExcludeFromCodeCoverage]
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
			case HttpStatusCode.InternalServerError:
				_problemDetails.Type = "https://datatracker.ietf.org/doc/html/rfc9110#name-500-internal-server-error";
				break;
			case HttpStatusCode.NotFound:
				_problemDetails.Type = "https://datatracker.ietf.org/doc/html/rfc9110#name-404-not-found";
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