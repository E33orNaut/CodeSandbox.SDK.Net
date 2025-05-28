using System;
using System.Threading;
using System.Threading.Tasks;
using CodeSandbox.SDK.Net.Interfaces;
using CodeSandbox.SDK.Net.Internal;
using CodeSandbox.SDK.Net.Models.New.SandboxContainerModels;
using CodeSandbox.SDK.Net.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace CodeSandbox.SDK.Net.Tests.Services
{
    [TestClass]
    public class ContainerServiceTests
    {
        private Mock<IApiClient> _mockClient;
        private Mock<LoggerService> _mockLogger;
        private ContainerService _service;

        [TestInitialize]
        public void Setup()
        {
            _mockClient = new Mock<IApiClient>(MockBehavior.Strict);
            _mockLogger = new Mock<LoggerService>(LogLevel.Trace);
            _service = new ContainerService(_mockClient.Object, _mockLogger.Object);
        }

        [TestMethod]
        public async Task SetupContainerAsync_Success_ReturnsResponse()
        {
            // Arrange
            ContainerSetupRequest request = new ContainerSetupRequest();
            ContainerSetupSuccessResponse expectedResponse = new ContainerSetupSuccessResponse();
            _ = _mockClient
                .Setup(c => c.PostAsync<ContainerSetupSuccessResponse>("/container/setup", request, It.IsAny<CancellationToken>()))
                .ReturnsAsync(expectedResponse);

            // Act
            ContainerSetupSuccessResponse result = await _service.SetupContainerAsync(request);

            // Assert
            Assert.AreEqual(expectedResponse, result);
            _mockClient.VerifyAll();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task SetupContainerAsync_NullRequest_ThrowsArgumentNullException()
        {
            // Act
            _ = await _service.SetupContainerAsync(null);
        }

        [TestMethod]
        public async Task SetupContainerAsync_ApiException_ThrowsApiException()
        {
            // Arrange
            ContainerSetupRequest request = new ContainerSetupRequest();
            ApiException apiEx = new ApiException("API error", 400, "Some response content");
            _ = _mockClient
                .Setup(c => c.PostAsync<ContainerSetupSuccessResponse>("/container/setup", request, It.IsAny<CancellationToken>()))
                .ThrowsAsync(apiEx);

            // Act & Assert
            ApiException ex = await Assert.ThrowsExceptionAsync<ApiException>(() => _service.SetupContainerAsync(request));
            StringAssert.Contains(ex.Message, "API error");
            Assert.AreEqual(apiEx.StatusCode, ex.StatusCode);
            Assert.AreEqual(apiEx.ResponseContent, ex.ResponseContent);
        }

        [TestMethod]
        public async Task SetupContainerAsync_UnexpectedException_ThrowsWrappedException()
        {
            // Arrange
            ContainerSetupRequest request = new ContainerSetupRequest();
            InvalidOperationException unexpectedEx = new InvalidOperationException("Unexpected");
            _ = _mockClient
                .Setup(c => c.PostAsync<ContainerSetupSuccessResponse>("/container/setup", request, It.IsAny<CancellationToken>()))
                .ThrowsAsync(unexpectedEx);

            // Act & Assert
            Exception ex = await Assert.ThrowsExceptionAsync<Exception>(() => _service.SetupContainerAsync(request));
            StringAssert.Contains(ex.Message, "Unexpected error during container setup");
            Assert.AreEqual(unexpectedEx, ex.InnerException);
        }
    }
}
