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
    public class PortServiceTests
    {
        private Mock<ApiClient> _mockApiClient;
        private Mock<LoggerService> _mockLogger;
        private PortService _service;

        [TestInitialize]
        public void Setup()
        {
            _mockApiClient = new Mock<ApiClient>();
            _mockLogger = new Mock<LoggerService>(LogLevel.Trace);
            _service = new PortService(_mockApiClient.Object, _mockLogger.Object);
        }

        [TestMethod]
        public async Task GetPortListAsync_ReturnsPortListResponse_WhenApiClientSucceeds()
        { 
            PortListResponse expectedResponse = new PortListResponse();
            _ = _mockApiClient
                .Setup(c => c.PostAsync<PortListResponse>("/port/list", It.IsAny<object>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(expectedResponse);
             
            PortListResponse result = await _service.GetPortListAsync();
             
            Assert.IsNotNull(result);
            Assert.AreEqual(expectedResponse, result);

            _mockLogger.Verify(l => l.LogInfo("Starting GetPortListAsync..."), Times.Once);
            _mockLogger.Verify(l => l.LogSuccess("GetPortListAsync completed successfully."), Times.Once);
        }

        [TestMethod]
        public async Task GetPortListAsync_ThrowsExceptionAndLogs_WhenApiExceptionOccurs()
        {
            Net.Services.ApiException apiException = new Net.Services.ApiException("API failure", "500");
            _mockApiClient
                .Setup(c => c.PostAsync<PortListResponse>("/port/list", It.IsAny<object>(), It.IsAny<CancellationToken>()))
                .ThrowsAsync(apiException);

            Exception ex = await Assert.ThrowsExceptionAsync<Exception>(() => _service.GetPortListAsync());

            StringAssert.Contains(ex.Message, "API error while fetching port list");
            Assert.AreSame(apiException, ex.InnerException);

            _mockLogger.Verify(l => l.LogError($"API error in GetPortListAsync: {apiException.Message} (Status: {apiException.ErrorCode})"), Times.Once);
#if DEBUG
            _mockLogger.Verify(l => l.LogTrace(It.IsAny<string>()), Times.AtLeastOnce);
#endif
        }

        [TestMethod]
        public async Task GetPortListAsync_ThrowsExceptionAndLogs_WhenUnexpectedExceptionOccurs()
        { 
            InvalidOperationException unexpectedException = new InvalidOperationException("Unexpected failure");
            _mockApiClient
                .Setup(c => c.PostAsync<PortListResponse>("/port/list", It.IsAny<object>(), It.IsAny<CancellationToken>()))
                .ThrowsAsync(unexpectedException);
             
            Exception ex = await Assert.ThrowsExceptionAsync<Exception>(() => _service.GetPortListAsync());

            StringAssert.Contains(ex.Message, "Unexpected error while fetching port list");
            Assert.AreSame(unexpectedException, ex.InnerException);

            _mockLogger.Verify(l => l.LogError("Unexpected error in GetPortListAsync.", unexpectedException), Times.Once);
#if DEBUG
            _mockLogger.Verify(l => l.LogTrace(It.IsAny<string>()), Times.AtLeastOnce);
#endif
        }
    }
}
