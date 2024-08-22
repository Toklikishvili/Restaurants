using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Moq;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;
using Xunit;

namespace Restaurants.API.Middlewares.Tests;

public class ErrorHandlingMiddlewareTests
{
    [Fact()]
    public async Task InvokeAsync_WhenNoExeptionThrown_ShouldCallNextDelegate()
    {
        // arrange

        var loggerMock = new Mock<ILogger<ErrorHandlingMiddleware>>();
        var middleware = new ErrorHandlingMiddleware(loggerMock.Object);
        var context = new DefaultHttpContext();
        var nextDelegateMock = new Mock<RequestDelegate>();

        // act

        await middleware.InvokeAsync(context , nextDelegateMock.Object);

        // assert

        nextDelegateMock.Verify(next => next.Invoke(context) , Times.Once);
    }

    [Fact()]
    public async Task InvokeAsync_WhenNoExeptionThrown_ShoulSetStatusCodeTo404AndWriteExeptionMessage()
    {
        // arrange

        var loggerMock = new Mock<ILogger<ErrorHandlingMiddleware>>();
        var middleware = new ErrorHandlingMiddleware(loggerMock.Object);
        var context = new DefaultHttpContext();
        var notFoundException = new NotFoundException(nameof(Restaurant) , "1");

        // act

        await middleware.InvokeAsync(context , _ => throw notFoundException);

        // assert

        context.Response.StatusCode.Should().Be(404);
    }

    [Fact()]
    public async Task InvokeAsync_WhenForbidExeptionThrown_ShoulSetStatusCodeTo403()
    {
        // arrange

        var loggerMock = new Mock<ILogger<ErrorHandlingMiddleware>>();
        var middleware = new ErrorHandlingMiddleware(loggerMock.Object);
        var context = new DefaultHttpContext();
        var exception = new ForbidException();

        // act

        await middleware.InvokeAsync(context , _ => throw exception);

        // assert

        context.Response.StatusCode.Should().Be(403);
    }

    [Fact()]
    public async Task InvokeAsync_WhenForbidExeptionThrown_ShoulSetStatusCodeTo500()
    {
        // arrange

        var loggerMock = new Mock<ILogger<ErrorHandlingMiddleware>>();
        var middleware = new ErrorHandlingMiddleware(loggerMock.Object);
        var context = new DefaultHttpContext();
        var exception = new Exception();

        // act

        await middleware.InvokeAsync(context , _ => throw exception);

        // assert

        context.Response.StatusCode.Should().Be(500);
    }
}