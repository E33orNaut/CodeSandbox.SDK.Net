using System;
using System.Threading;
using System.Threading.Tasks;
using CodeSandbox.SDK.Net.Internal;
using CodeSandbox.SDK.Net.Models.New.SandboxFSModels;
using CodeSandbox.SDK.Net.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace CodeSandbox.SDK.Net.Tests.Services
{
    [TestClass]
    public class SandboxFsServiceTests
    {
        private Mock<ApiClient> _mockClient;
        private Mock<LoggerService> _mockLogger;
        private SandboxFsService _service;

        [TestInitialize]
        public void Setup()
        {
            _mockClient = new Mock<ApiClient>("http://localhost", null, null, null);
            _mockLogger = new Mock<LoggerService>(LogLevel.Trace);
            _service = new SandboxFsService(_mockClient.Object, _mockLogger.Object);
        }

        [TestMethod]
        public async Task WriteFileAsync_Success_ReturnsResult()
        {
            var req = new SandboxFSWriteFileRequest();
            var expected = new object();
            _mockClient.Setup(c => c.PostAsync<SandboxFSSuccessResponse<object>>("/fs/writeFile", req, It.IsAny<CancellationToken>()))
                .ReturnsAsync(new SandboxFSSuccessResponse<object> { Result = expected });

            var result = await _service.WriteFileAsync(req);
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public async Task WriteFileAsync_Exception_ThrowsWrapped()
        {
            var req = new SandboxFSWriteFileRequest();
            _mockClient.Setup(c => c.PostAsync<SandboxFSSuccessResponse<object>>("/fs/writeFile", req, It.IsAny<CancellationToken>()))
                .ThrowsAsync(new InvalidOperationException("fail"));

            await Assert.ThrowsExceptionAsync<Exception>(() => _service.WriteFileAsync(req));
        }

        [TestMethod]
        public async Task FsPathSearchAsync_Success_ReturnsResult()
        {
            var req = new SandboxFSPathSearchResult();
            var expected = new SandboxFSPathSearchResult();
            _mockClient.Setup(c => c.PostAsync<SandboxFSSuccessResponse<SandboxFSPathSearchResult>>("/fs/pathSearch", req, It.IsAny<CancellationToken>()))
                .ReturnsAsync(new SandboxFSSuccessResponse<SandboxFSPathSearchResult> { Result = expected });

            var result = await _service.FsPathSearchAsync(req);
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public async Task FsUploadAsync_Success_ReturnsResult()
        {
            var req = new UploadRequest();
            var expected = new UploadResult();
            _mockClient.Setup(c => c.PostAsync<SandboxFSSuccessResponse<UploadResult>>("/fs/upload", req, It.IsAny<CancellationToken>()))
                .ReturnsAsync(new SandboxFSSuccessResponse<UploadResult> { Result = expected });

            var result = await _service.FsUploadAsync(req);
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public async Task FsDownloadAsync_Success_ReturnsResult()
        {
            var req = new DownloadRequest();
            var expected = new DownloadResult();
            _mockClient.Setup(c => c.PostAsync<SandboxFSSuccessResponse<DownloadResult>>("/fs/download", req, It.IsAny<CancellationToken>()))
                .ReturnsAsync(new SandboxFSSuccessResponse<DownloadResult> { Result = expected });

            var result = await _service.FsDownloadAsync(req);
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public async Task FsReadFileAsync_Success_ReturnsResult()
        {
            var req = new FSReadFileParams();
            var expected = new FSReadFileResult();
            _mockClient.Setup(c => c.PostAsync<SandboxFSSuccessResponse<FSReadFileResult>>("/fs/readFile", req, It.IsAny<CancellationToken>()))
                .ReturnsAsync(new SandboxFSSuccessResponse<FSReadFileResult> { Result = expected });

            var result = await _service.FsReadFileAsync(req);
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public async Task ReadDirAsync_Success_ReturnsResult()
        {
            var req = new FSReadDirParams();
            var expected = new FSReadDirResult();
            _mockClient.Setup(c => c.PostAsync<SandboxFSSuccessResponse<FSReadDirResult>>("/fs/readdir", req, It.IsAny<CancellationToken>()))
                .ReturnsAsync(new SandboxFSSuccessResponse<FSReadDirResult> { Result = expected });

            var result = await _service.ReadDirAsync(req);
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public async Task StatAsync_Success_ReturnsResult()
        {
            var req = new FSStatParams();
            var expected = new FSStatResult();
            _mockClient.Setup(c => c.PostAsync<SandboxFSSuccessResponse<FSStatResult>>("/fs/stat", req, It.IsAny<CancellationToken>()))
                .ReturnsAsync(new SandboxFSSuccessResponse<FSStatResult> { Result = expected });

            var result = await _service.StatAsync(req);
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public async Task CopyAsync_Success_ReturnsResult()
        {
            var req = new FSCopyParams();
            var expected = new object();
            _mockClient.Setup(c => c.PostAsync<SandboxFSSuccessResponse<object>>("/fs/copy", req, It.IsAny<CancellationToken>()))
                .ReturnsAsync(new SandboxFSSuccessResponse<object> { Result = expected });

            var result = await _service.CopyAsync(req);
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public async Task RenameAsync_Success_ReturnsResult()
        {
            var req = new FSRenameParams();
            var expected = new object();
            _mockClient.Setup(c => c.PostAsync<SandboxFSSuccessResponse<object>>("/fs/rename", req, It.IsAny<CancellationToken>()))
                .ReturnsAsync(new SandboxFSSuccessResponse<object> { Result = expected });

            var result = await _service.RenameAsync(req);
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public async Task RemoveAsync_Success_ReturnsResult()
        {
            var req = new FSRemoveParams();
            var expected = new object();
            _mockClient.Setup(c => c.PostAsync<SandboxFSSuccessResponse<object>>("/fs/remove", req, It.IsAny<CancellationToken>()))
                .ReturnsAsync(new SandboxFSSuccessResponse<object> { Result = expected });

            var result = await _service.RemoveAsync(req);
            Assert.AreEqual(expected, result);
        }
    }
}