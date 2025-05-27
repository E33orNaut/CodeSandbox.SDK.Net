using System;
using System.Threading;
using System.Threading.Tasks;
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
        private Mock<ApiClient> _mockClient;
        private Mock<LoggerService> _mockLogger;
        private ContainerService _service;

        [TestInitialize]
        public void Setup()
        {
            _mockClient = new Mock<ApiClient>(MockBehavior.Strict, null);
            _mockLogger = new Mock<LoggerService>(LogLevel.Trace);
            _service = new ContainerService(_mockClient.Object, _mockLogger.Object);
        }

        [TestMethod]
        public async Task SetupContainerAsync_Success_ReturnsResponse()
        {
            // Arrange
            var request = new ContainerSetupRequest();
            var expectedResponse = new ContainerSetupSuccessResponse();
            _mockClient
                .Setup(c => c.PostAsync<ContainerSetupSuccessResponse>("/container/setup", request, It.IsAny<CancellationToken>()))
                .ReturnsAsync(expectedResponse);

            // Act
            var result = await _service.SetupContainerAsync(request);

            // Assert
            Assert.AreEqual(expectedResponse, result);
            _mockClient.VerifyAll();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task SetupContainerAsync_NullRequest_ThrowsArgumentNullException()
        {
            // Act
            await _service.SetupContainerAsync(null);
        }

        [TestMethod]
        public async Task SetupContainerAsync_ApiException_ThrowsWrappedException()
        {
            // Arrange
            var request = new ContainerSetupRequest();
            var apiEx = new ApiException("API error", 0, "Some response content");
            _mockClient
                .Setup(c => c.PostAsync<ContainerSetupSuccessResponse>("/container/setup", request, It.IsAny<CancellationToken>()))
                .ThrowsAsync(apiEx);

            // Act & Assert
            var ex = await Assert.ThrowsExceptionAsync<Exception>(() => _service.SetupContainerAsync(request));
            StringAssert.Contains(ex.Message, "API error during container setup");
            Assert.AreEqual(apiEx, ex.InnerException);
        }

        [TestMethod]
        public async Task SetupContainerAsync_UnexpectedException_ThrowsWrappedException()
        {
            // Arrange
            var request = new ContainerSetupRequest();
            var unexpectedEx = new InvalidOperationException("Unexpected");
            _mockClient
                .Setup(c => c.PostAsync<ContainerSetupSuccessResponse>("/container/setup", request, It.IsAny<CancellationToken>()))
                .ThrowsAsync(unexpectedEx);

            // Act & Assert
            var ex = await Assert.ThrowsExceptionAsync<Exception>(() => _service.SetupContainerAsync(request));
            StringAssert.Contains(ex.Message, "Unexpected error during container setup");
            Assert.AreEqual(unexpectedEx, ex.InnerException);
        }
    }
}
