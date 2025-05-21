using System;
using System.Threading;
using System.Threading.Tasks;
using CodeSandbox.SDK.Net.Internal;
using CodeSandbox.SDK.Net.Models;
using CodeSandbox.SDK.Net.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace CodeSandbox.SDK.Net.Tests.Services
{
    [TestClass]
    public class ContainerServiceTests
    {
        private Mock<ApiClient> _mockApiClient;
        private Mock<LoggerService> _mockLogger;
        private ContainerService _service;

        [TestInitialize]
        public void Setup()
        {
            _mockApiClient = new Mock<ApiClient>(null, null);
            _mockLogger = new Mock<LoggerService>(LogLevel.Trace);
            _service = new ContainerService(_mockApiClient.Object, _mockLogger.Object);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task SetupContainerAsync_NullRequest_ThrowsArgumentNullException()
        { 
            _ = await _service.SetupContainerAsync(null);
        }

        [TestMethod]
        public async Task SetupContainerAsync_ValidRequest_ReturnsExpectedResponse()
        {
            
            ContainerSetupRequest request = new ContainerSetupRequest { ContainerId = "abc123" };
            ContainerSetupResponse expectedResponse = new ContainerSetupResponse { Status = 200 };

            _ = _mockApiClient
                .Setup(c => c.PostAsync<ContainerSetupResponse>(
                    "/container/setup", request, It.IsAny<CancellationToken>()))
                .ReturnsAsync(expectedResponse);
             
            ContainerSetupResponse response = await _service.SetupContainerAsync(request);

            
            Assert.IsNotNull(response);
            Assert.AreEqual("success", response.Status);
            _mockLogger.Verify(l => l.LogSuccess("Container setup completed successfully."), Times.Once);
        }

        [TestMethod]
        public async Task SetupContainerAsync_ApiThrowsApiException_LogsErrorAndThrows()
        {
            
            ContainerSetupRequest request = new ContainerSetupRequest { ContainerId = "abc123" };
            Net.Services.ApiException apiEx = new Net.Services.ApiException("API failed", "500");

            _ = _mockApiClient
                .Setup(c => c.PostAsync<ContainerSetupResponse>(
                    "/container/setup", request, It.IsAny<CancellationToken>()))
                .ThrowsAsync(apiEx);

             
            try
            {
                _ = await _service.SetupContainerAsync(request);
                Assert.Fail("Expected exception was not thrown.");
            }
            catch (Exception ex)
            {
                Assert.IsTrue(ex.Message.Contains("API error during container setup"));
                _mockLogger.Verify(l => l.LogError(It.Is<string>(s => s.Contains("API error")), apiEx), Times.Once);
            }
        }

        [TestMethod]
        public async Task SetupContainerAsync_GeneralException_LogsErrorAndThrows()
        {
            
            ContainerSetupRequest request = new ContainerSetupRequest { ContainerId = "abc123" };
            InvalidOperationException exception = new InvalidOperationException("Something went wrong");

            _ = _mockApiClient
                .Setup(c => c.PostAsync<ContainerSetupResponse>(
                    "/container/setup", request, It.IsAny<CancellationToken>()))
                .ThrowsAsync(exception);

             
            try
            {
                _ = await _service.SetupContainerAsync(request);
                Assert.Fail("Expected exception was not thrown.");
            }
            catch (Exception ex)
            {
                Assert.IsTrue(ex.Message.Contains("Unexpected error during container setup"));
                _mockLogger.Verify(l => l.LogError("Unexpected error during container setup.", exception), Times.Once);
            }
        }
    }
}
