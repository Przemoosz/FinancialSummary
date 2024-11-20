namespace FinancialSummary.Tests.Presentation.Factories;

using System.Net;
using Application.Result;
using Categories;
using FinancialSummary.Presentation.Factories;
using FluentAssertions;
using TddXt.AnyRoot.Strings;
using static TddXt.AnyRoot.Root;

[Parallelizable, PresentationLayerTests]
public sealed class ProblemDetailsFactoryTests
{
	private ProblemDetailsFactory _sut;

	[SetUp]
	public void Setup()
	{
		_sut = new ProblemDetailsFactory();
	}
	
	[Test]
	public void Create_ForBadRequest_ShouldReturnProblemDetails_ForBadRequests()
	{
		// Arrange
		string failureReason = Any.String();
		string errorMessage = Any.String();
		Guid operationId = Guid.NewGuid();
		OperationFailed operationFailed = new OperationFailed(failureReason, errorMessage, HttpStatusCode.BadRequest);
		
		// Act
		var actual = _sut.Create(operationFailed, operationId);

		// Assert
		actual.Extensions.TryGetValue("operationId", out var id);
		id.Should().Be(operationId);
		actual.Detail.Should().Be(errorMessage);
		actual.Title.Should().Be(failureReason);
		actual.Status.Should().Be(400);
		actual.Type.Should().Be("https://datatracker.ietf.org/doc/html/rfc9110#name-400-bad-request");
	}
	
	[Test]
	public void Create_WhenNoOperationId_ShouldReturnProblemDetails_WithEmptyGuid()
	{
		// Arrange
		OperationFailed operationFailed = new OperationFailed(Any.String(), Any.String(), HttpStatusCode.BadRequest);
		
		// Act
		var actual = _sut.Create(operationFailed, null);

		// Assert
		actual.Extensions.TryGetValue("operationId", out var id);
		id.Should().Be(Guid.Empty);
	}
	
	[Test]
	public void Create_WhenNoMatchingStatusCodeShouldReturnProblemDetails_WithAboutBlank()
	{
		// Arrange
		OperationFailed operationFailed = new OperationFailed(Any.String(), Any.String(), HttpStatusCode.IMUsed);
		
		// Act
		var actual = _sut.Create(operationFailed, null);

		// Assert
		actual.Type.Should().Be("about:blank");
	}
	
}