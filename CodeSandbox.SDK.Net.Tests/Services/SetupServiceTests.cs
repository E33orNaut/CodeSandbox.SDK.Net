using System;
using System.Threading;
using System.Threading.Tasks;
using CodeSandbox.SDK.Net.Interfaces;
using CodeSandbox.SDK.Net.Internal;
using CodeSandbox.SDK.Net.Models.New.SandboxSetupModels;
using CodeSandbox.SDK.Net.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace CodeSandbox.SDK.Net.Tests.Services
{
    [TestClass]
    public class SetupServiceTests
    {
        private Mock<IApiClient> _mockClient;
        private Mock<LoggerService> _mockLogger;
        private SetupService _service;

        [TestInitialize]
        public void Setup()
        {
            _mockClient = new Mock<IApiClient>();
            _mockLogger = new Mock<LoggerService>(LogLevel.Trace);
            _service = new SetupService(_mockClient.Object, _mockLogger.Object);
        }

        [TestMethod]
        public async Task GetSetupProgressAsync_Success_ReturnsResponse()
        {
            var expected = new SandboxSetupSuccessResponse();
            _mockClient.Setup(c => c.PostAsync<SandboxSetupSuccessResponse>("/setup/get", It.IsAny<object>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(expected);

            var result = await _service.GetSetupProgressAsync();
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public async Task SkipStepAsync_Success_ReturnsResponse()
        {
            var expected = new SandboxSetupSuccessResponse();
            _mockClient.Setup(c => c.PostAsync<SandboxSetupSuccessResponse>("/setup/skip", It.IsAny<object>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(expected);

            var result = await _service.SkipStepAsync(1);
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public async Task SkipAllStepsAsync_Success_ReturnsResponse()
        {
            var expected = new SandboxSetupSuccessResponse();
            _mockClient.Setup(c => c.PostAsync<SandboxSetupSuccessResponse>("/setup/skipAll", null, It.IsAny<CancellationToken>()))
                .ReturnsAsync(expected);

            var result = await _service.SkipAllStepsAsync();
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public async Task DisableSetupAsync_Success_ReturnsResponse()
        {
            var expected = new SandboxSetupSuccessResponse();
            _mockClient.Setup(c => c.PostAsync<SandboxSetupSuccessResponse>("/setup/disable", null, It.IsAny<CancellationToken>()))
                .ReturnsAsync(expected);

            var result = await _service.DisableSetupAsync();
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public async Task EnableSetupAsync_Success_ReturnsResponse()
        {
            var expected = new SandboxSetupSuccessResponse();
            _mockClient.Setup(c => c.PostAsync<SandboxSetupSuccessResponse>("/setup/enable", null, It.IsAny<CancellationToken>()))
                .ReturnsAsync(expected);

            var result = await _service.EnableSetupAsync();
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public async Task InitializeSetupAsync_Success_ReturnsResponse()
        {
            var expected = new SandboxSetupSuccessResponse();
            _mockClient.Setup(c => c.PostAsync<SandboxSetupSuccessResponse>("/setup/init", null, It.IsAny<CancellationToken>()))
                .ReturnsAsync(expected);

            var result = await _service.InitializeSetupAsync();
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public async Task SetStepAsync_Success_ReturnsResponse()
        {
            var expected = new SandboxSetupSuccessResponse();
            _mockClient.Setup(c => c.PostAsync<SandboxSetupSuccessResponse>("/setup/setStep", It.IsAny<object>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(expected);

            var result = await _service.SetStepAsync(2);
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public async Task GetSetupProgressAsync_ApiException_ThrowsWrapped()
        {
            _mockClient.Setup(c => c.PostAsync<SandboxSetupSuccessResponse>("/setup/get", It.IsAny<object>(), It.IsAny<CancellationToken>()))
                .ThrowsAsync(new ApiException("fail", 400, "err"));

            await Assert.ThrowsExceptionAsync<Exception>(() => _service.GetSetupProgressAsync());
        }

        [TestMethod]
        public async Task SkipStepAsync_UnexpectedException_ThrowsWrapped()
        {
            _mockClient.Setup(c => c.PostAsync<SandboxSetupSuccessResponse>("/setup/skip", It.IsAny<object>(), It.IsAny<CancellationToken>()))
                .ThrowsAsync(new InvalidOperationException("fail"));

            await Assert.ThrowsExceptionAsync<Exception>(() => _service.SkipStepAsync(1));
        }
    }
}