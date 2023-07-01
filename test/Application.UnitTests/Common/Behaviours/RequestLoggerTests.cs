﻿using OnlineStoreApi.Application.Common.Behaviours;
using OnlineStoreApi.Application.Common.Interfaces;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using OnlineStoreApi.Application.Products.Commands.AddEdit;

namespace OnlineStoreApi.Application.UnitTests.Common.Behaviours;

public class RequestLoggerTests
{
    private Mock<ILogger<AddEditProductCommand>> _logger = null!;
    private Mock<ICurrentUserService> _currentUserService = null!;
    private Mock<IIdentityService> _identityService = null!;

    [SetUp]
    public void Setup()
    {
        _logger = new Mock<ILogger<AddEditProductCommand>>();
        _currentUserService = new Mock<ICurrentUserService>();
        _identityService = new Mock<IIdentityService>();
    }

    [Test]
    public async Task ShouldCallGetUserNameAsyncOnceIfAuthenticated()
    {
        _currentUserService.Setup(x => x.UserId).Returns(Guid.NewGuid().ToString());

        var requestLogger = new LoggingBehaviour<AddEditProductCommand>(_logger.Object, _currentUserService.Object, _identityService.Object);

        await requestLogger.Process(new AddEditProductCommand { SizeId = 1,ColorId=1, Name = "title" }, new CancellationToken());

        _identityService.Verify(i => i.GetUserNameAsync(It.IsAny<string>()), Times.Once);
    }

    [Test]
    public async Task ShouldNotCallGetUserNameAsyncOnceIfUnauthenticated()
    {
        var requestLogger = new LoggingBehaviour<AddEditProductCommand>(_logger.Object, _currentUserService.Object, _identityService.Object);

        await requestLogger.Process(new AddEditProductCommand { SizeId = 1, ColorId = 1, Name = "title" }, new CancellationToken());

        _identityService.Verify(i => i.GetUserNameAsync(It.IsAny<string>()), Times.Never);
    }
}
