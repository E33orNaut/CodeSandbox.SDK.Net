using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using CodeSandbox.SDK.Net.Internal;
using CodeSandbox.SDK.Net.Models.New.PortModels;
using CodeSandbox.SDK.Net.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace CodeSandbox.SDK.Net.Tests.Services
{
    [TestClass]
    public class PortServiceTests
    {
        private Mock<ApiClient> _mockClient;
        private Mock<LoggerService> _mockLogger;
        private PortService _service;

        [TestInitialize]
        public void Setup()
        {
            _mockClient = new Mock<ApiClient>("http://localhost", null, null, null);
            _mockLogger = new Mock<LoggerService>(LogLevel.Trace);
            _service = new PortService(_mockClient.Object, _mockLogger.Object);
        }

        [TestMethod]
        public async Task GetPortListAsync_Success_ReturnsResult()
        {
            var expectedList = new List<PortModel>
            {
                new PortModel(), // Add property initializations as needed
                new PortModel()
            };
            var expectedResult = new PortListResult { List = expectedList };
            var response = new PortSuccessResponse { Result = expectedResult };

            _mockClient
                .Setup(c => c.PostAsync<PortSuccessResponse>("/port/list", It.IsAny<object>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(response);

            var result = await _service.GetPortListAsync();

            Assert.IsNotNull(result);
            Assert.AreEqual(expectedResult, result);
            Assert.AreEqual(expectedList, result.List);
        }

        [TestMethod]
        public async Task GetPortListAsync_ApiException_ThrowsWrapped()
        {
            _mockClient
                .Setup(c => c.PostAsync<PortSuccessResponse>("/port/list", It.IsAny<object>(), It.IsAny<CancellationToken>()))
                .ThrowsAsync(new ApiException("fail", 400, "err"));

            await Assert.ThrowsExceptionAsync<Exception>(() => _service.GetPortListAsync());
        }

        [TestMethod]
        public async Task GetPortListAsync_UnexpectedException_ThrowsWrapped()
        {
            _mockClient
                .Setup(c => c.PostAsync<PortSuccessResponse>("/port/list", It.IsAny<object>(), It.IsAny<CancellationToken>()))
                .ThrowsAsync(new InvalidOperationException("unexpected"));

            await Assert.ThrowsExceptionAsync<Exception>(() => _service.GetPortListAsync());
        }
    }
}