using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using CodeSandbox.SDK.Net.Interfaces;
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
        private Mock<IApiClient> _mockClient;
        private Mock<LoggerService> _mockLogger;
        private PortService _service;

        [TestInitialize]
        public void Setup()
        {
            _mockClient = new Mock<IApiClient>();
            _mockLogger = new Mock<LoggerService>(LogLevel.Trace);
            _service = new PortService(_mockClient.Object, _mockLogger.Object);
        }

        [TestMethod]
        public async Task GetPortListAsync_Success_ReturnsResult()
        {
            List<PortModel> expectedList = new List<PortModel>
            {
                new PortModel { /* set properties as needed for your test */ }
            };
            PortSuccessResponse expectedResponse = new PortSuccessResponse
            {
                Result = new PortListResult { List = expectedList }
            };

            _ = _mockClient
                .Setup(c => c.PostAsync<PortSuccessResponse>("/port/list", It.IsAny<object>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(expectedResponse);

            PortListResult result = await _service.GetPortListAsync();

            Assert.IsNotNull(result);
            Assert.AreEqual(expectedList, result.List);
        }

        [TestMethod]
        public async Task GetPortListAsync_ApiException_ThrowsWrapped()
        {
            _ = _mockClient
                .Setup(c => c.PostAsync<PortSuccessResponse>("/port/list", It.IsAny<object>(), It.IsAny<CancellationToken>()))
                .ThrowsAsync(new ApiException("fail", 400, "err"));

            _ = await Assert.ThrowsExceptionAsync<System.Exception>(() => _service.GetPortListAsync());
        }
    }
}