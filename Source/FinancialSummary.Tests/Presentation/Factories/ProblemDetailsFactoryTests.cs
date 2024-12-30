namespace FinancialSummary.Tests.Presentation.Factories;

using System.Net;
using Categories;
using FinancialSummary.Application.Result;
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
	public void Create_ForBadRequest_ReturnsProblemDetailsForBadRequests()
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
	public void Create_ForInternalServerError_ReturnProblemDetailsForInternalServerError()
	{
		// Arrange
		string failureReason = Any.String();
		string errorMessage = Any.String();
		Guid operationId = Guid.NewGuid();
		OperationFailed operationFailed = new OperationFailed(failureReason, errorMessage, HttpStatusCode.InternalServerError);
		
		// Act
		var actual = _sut.Create(operationFailed, operationId);

		// Assert
		actual.Extensions.TryGetValue("operationId", out var id);
		id.Should().Be(operationId);
		actual.Detail.Should().Be(errorMessage);
		actual.Title.Should().Be(failureReason);
		actual.Status.Should().Be(500);
		actual.Type.Should().Be("https://datatracker.ietf.org/doc/html/rfc9110#name-500-internal-server-error");
	}
	
	[Test]
	public void Create_ForNotFound_ReturnsProblemDetailsForNotFound()
	{
		// Arrange
		string failureReason = Any.String();
		string errorMessage = Any.String();
		Guid operationId = Guid.NewGuid();
		OperationFailed operationFailed = new OperationFailed(failureReason, errorMessage, HttpStatusCode.NotFound);
		
		// Act
		var actual = _sut.Create(operationFailed, operationId);

		// Assert
		actual.Extensions.TryGetValue("operationId", out var id);
		id.Should().Be(operationId);
		actual.Detail.Should().Be(errorMessage);
		actual.Title.Should().Be(failureReason);
		actual.Status.Should().Be(404);
		actual.Type.Should().Be("https://datatracker.ietf.org/doc/html/rfc9110#name-404-not-found");
	}
	
	[Test]
	public void Create_WhenNoOperationId_ReturnsProblemDetails_WithEmptyGuid()
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
	public void Create_WhenNoMatchingStatusCode_ReturnsProblemDetailsWithAboutBlank()
	{
		// Arrange
		OperationFailed operationFailed = new OperationFailed(Any.String(), Any.String(), HttpStatusCode.IMUsed);
		
		// Act
		var actual = _sut.Create(operationFailed, null);

		// Assert
		actual.Type.Should().Be("about:blank");
	}
	
}