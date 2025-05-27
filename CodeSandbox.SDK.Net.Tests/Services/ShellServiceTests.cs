using System;
using System.Threading;
using System.Threading.Tasks;
using CodeSandbox.SDK.Net.Internal;
using CodeSandbox.SDK.Net.Models.New.SandboxShellModels;
using CodeSandbox.SDK.Net.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace CodeSandbox.SDK.Net.Tests.Services
{
    [TestClass]
    public class ShellServiceTests
    {
        private Mock<ApiClient> _mockClient;
        private Mock<LoggerService> _mockLogger;
        private ShellService _service;

        [TestInitialize]
        public void Setup()
        {
            _mockClient = new Mock<ApiClient>("http://localhost", null, null, null);
            _mockLogger = new Mock<LoggerService>(LogLevel.Trace);
            _service = new ShellService(_mockClient.Object, _mockLogger.Object);
        }

        [TestMethod]
        public async Task CreateShellAsync_Success_ReturnsResponse()
        {
            var req = new SandboxShellCreateRequest();
            var expected = new SandboxShellSuccessResponse<SandboxOpenShellDTO>();
            _mockClient.Setup(c => c.PostAsync<SandboxShellSuccessResponse<SandboxOpenShellDTO>>("/shell/create", req, It.IsAny<CancellationToken>()))
                .ReturnsAsync(expected);

            var result = await _service.CreateShellAsync(req);
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public async Task SendInputAsync_Success_ReturnsResponse()
        {
            var req = new SandboxShellInRequest();
            var expected = new SandboxShellSuccessResponse<object>();
            _mockClient.Setup(c => c.PostAsync<SandboxShellSuccessResponse<object>>("/shell/in", req, It.IsAny<CancellationToken>()))
                .ReturnsAsync(expected);

            var result = await _service.SendInputAsync(req);
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public async Task ListShellsAsync_Success_ReturnsResponse()
        {
            var expected = new SandboxShellSuccessResponse<SandboxShellListResult>();
            _mockClient.Setup(c => c.PostAsync<SandboxShellSuccessResponse<SandboxShellListResult>>("/shell/list", It.IsAny<object>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(expected);

            var result = await _service.ListShellsAsync();
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public async Task OpenShellAsync_Success_ReturnsResponse()
        {
            var req = new SandboxShellOpenRequest();
            var expected = new SandboxShellSuccessResponse<SandboxOpenShellDTO>();
            _mockClient.Setup(c => c.PostAsync<SandboxShellSuccessResponse<SandboxOpenShellDTO>>("/shell/open", req, It.IsAny<CancellationToken>()))
                .ReturnsAsync(expected);

            var result = await _service.OpenShellAsync(req);
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public async Task CloseShellAsync_Success_ReturnsResponse()
        {
            var req = new SandboxShellIdRequest();
            var expected = new SandboxShellSuccessResponse<object>();
            _mockClient.Setup(c => c.PostAsync<SandboxShellSuccessResponse<object>>("/shell/close", req, It.IsAny<CancellationToken>()))
                .ReturnsAsync(expected);

            var result = await _service.CloseShellAsync(req);
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public async Task RestartShellAsync_Success_ReturnsResponse()
        {
            var req = new SandboxShellIdRequest();
            var expected = new SandboxShellSuccessResponse<object>();
            _mockClient.Setup(c => c.PostAsync<SandboxShellSuccessResponse<object>>("/shell/restart", req, It.IsAny<CancellationToken>()))
                .ReturnsAsync(expected);

            var result = await _service.RestartShellAsync(req);
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public async Task TerminateShellAsync_Success_ReturnsResponse()
        {
            var req = new SandboxShellIdRequest();
            var expected = new SandboxShellSuccessResponse<SandboxShellDTO>();
            _mockClient.Setup(c => c.PostAsync<SandboxShellSuccessResponse<SandboxShellDTO>>("/shell/terminate", req, It.IsAny<CancellationToken>()))
                .ReturnsAsync(expected);

            var result = await _service.TerminateShellAsync(req);
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public async Task ResizeShellAsync_Success_ReturnsResponse()
        {
            var req = new SandboxShellResizeRequest();
            var expected = new SandboxShellSuccessResponse<object>();
            _mockClient.Setup(c => c.PostAsync<SandboxShellSuccessResponse<object>>("/shell/resize", req, It.IsAny<CancellationToken>()))
                .ReturnsAsync(expected);

            var result = await _service.ResizeShellAsync(req);
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public async Task RenameShellAsync_Success_ReturnsResponse()
        {
            var req = new SandboxShellRenameRequest();
            var expected = new SandboxShellSuccessResponse<object>();
            _mockClient.Setup(c => c.PostAsync<SandboxShellSuccessResponse<object>>("/shell/rename", req, It.IsAny<CancellationToken>()))
                .ReturnsAsync(expected);

            var result = await _service.RenameShellAsync(req);
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public async Task CreateShellAsync_ApiException_ThrowsWrapped()
        {
            var req = new SandboxShellCreateRequest();
            _mockClient.Setup(c => c.PostAsync<SandboxShellSuccessResponse<SandboxOpenShellDTO>>("/shell/create", req, It.IsAny<CancellationToken>()))
                .ThrowsAsync(new ApiException("fail", 400, "err"));

            await Assert.ThrowsExceptionAsync<Exception>(() => _service.CreateShellAsync(req));
        }

        [TestMethod]
        public async Task ListShellsAsync_UnexpectedException_ThrowsWrapped()
        {
            _mockClient.Setup(c => c.PostAsync<SandboxShellSuccessResponse<SandboxShellListResult>>("/shell/list", It.IsAny<object>(), It.IsAny<CancellationToken>()))
                .ThrowsAsync(new InvalidOperationException("fail"));

            await Assert.ThrowsExceptionAsync<Exception>(() => _service.ListShellsAsync());
        }
    }
}