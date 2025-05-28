using System;
using System.Threading;
using System.Threading.Tasks;
using CodeSandbox.SDK.Net.Interfaces;
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

        private Mock<IApiClient> _mockClient;
        private Mock<LoggerService> _mockLogger;
        private SandboxFsService _service;

        [TestInitialize]
        public void Setup()
        {
            _mockClient = new Mock<IApiClient>();
            _mockLogger = new Mock<LoggerService>(LogLevel.Trace);
            _service = new SandboxFsService(_mockClient.Object, _mockLogger.Object);
        }


        [TestMethod]
        public async Task WriteFileAsync_Success_ReturnsResult()
        {
            SandboxFSWriteFileRequest req = new SandboxFSWriteFileRequest();
            object expected = new object();
            _ = _mockClient.Setup(c => c.PostAsync<SandboxFSSuccessResponse<object>>("/fs/writeFile", req, It.IsAny<CancellationToken>()))
                .ReturnsAsync(new SandboxFSSuccessResponse<object> { Result = expected });

            object result = await _service.WriteFileAsync(req);
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public async Task WriteFileAsync_Exception_ThrowsWrapped()
        {
            SandboxFSWriteFileRequest req = new SandboxFSWriteFileRequest();
            _ = _mockClient.Setup(c => c.PostAsync<SandboxFSSuccessResponse<object>>("/fs/writeFile", req, It.IsAny<CancellationToken>()))
                .ThrowsAsync(new InvalidOperationException("fail"));

            _ = await Assert.ThrowsExceptionAsync<Exception>(() => _service.WriteFileAsync(req));
        }

        [TestMethod]
        public async Task FsPathSearchAsync_Success_ReturnsResult()
        {
            SandboxFSPathSearchResult req = new SandboxFSPathSearchResult();
            SandboxFSPathSearchResult expected = new SandboxFSPathSearchResult();
            _ = _mockClient.Setup(c => c.PostAsync<SandboxFSSuccessResponse<SandboxFSPathSearchResult>>("/fs/pathSearch", req, It.IsAny<CancellationToken>()))
                .ReturnsAsync(new SandboxFSSuccessResponse<SandboxFSPathSearchResult> { Result = expected });

            SandboxFSPathSearchResult result = await _service.FsPathSearchAsync(req);
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public async Task FsUploadAsync_Success_ReturnsResult()
        {
            UploadRequest req = new UploadRequest();
            UploadResult expected = new UploadResult();
            _ = _mockClient.Setup(c => c.PostAsync<SandboxFSSuccessResponse<UploadResult>>("/fs/upload", req, It.IsAny<CancellationToken>()))
                .ReturnsAsync(new SandboxFSSuccessResponse<UploadResult> { Result = expected });

            UploadResult result = await _service.FsUploadAsync(req);
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public async Task FsDownloadAsync_Success_ReturnsResult()
        {
            DownloadRequest req = new DownloadRequest();
            DownloadResult expected = new DownloadResult();
            _ = _mockClient.Setup(c => c.PostAsync<SandboxFSSuccessResponse<DownloadResult>>("/fs/download", req, It.IsAny<CancellationToken>()))
                .ReturnsAsync(new SandboxFSSuccessResponse<DownloadResult> { Result = expected });

            DownloadResult result = await _service.FsDownloadAsync(req);
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public async Task FsReadFileAsync_Success_ReturnsResult()
        {
            FSReadFileParams req = new FSReadFileParams();
            FSReadFileResult expected = new FSReadFileResult();
            _ = _mockClient.Setup(c => c.PostAsync<SandboxFSSuccessResponse<FSReadFileResult>>("/fs/readFile", req, It.IsAny<CancellationToken>()))
                .ReturnsAsync(new SandboxFSSuccessResponse<FSReadFileResult> { Result = expected });

            FSReadFileResult result = await _service.FsReadFileAsync(req);
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public async Task ReadDirAsync_Success_ReturnsResult()
        {
            FSReadDirParams req = new FSReadDirParams();
            FSReadDirResult expected = new FSReadDirResult();
            _ = _mockClient.Setup(c => c.PostAsync<SandboxFSSuccessResponse<FSReadDirResult>>("/fs/readdir", req, It.IsAny<CancellationToken>()))
                .ReturnsAsync(new SandboxFSSuccessResponse<FSReadDirResult> { Result = expected });

            FSReadDirResult result = await _service.ReadDirAsync(req);
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public async Task StatAsync_Success_ReturnsResult()
        {
            FSStatParams req = new FSStatParams();
            FSStatResult expected = new FSStatResult();
            _ = _mockClient.Setup(c => c.PostAsync<SandboxFSSuccessResponse<FSStatResult>>("/fs/stat", req, It.IsAny<CancellationToken>()))
                .ReturnsAsync(new SandboxFSSuccessResponse<FSStatResult> { Result = expected });

            FSStatResult result = await _service.StatAsync(req);
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public async Task CopyAsync_Success_ReturnsResult()
        {
            FSCopyParams req = new FSCopyParams();
            object expected = new object();
            _ = _mockClient.Setup(c => c.PostAsync<SandboxFSSuccessResponse<object>>("/fs/copy", req, It.IsAny<CancellationToken>()))
                .ReturnsAsync(new SandboxFSSuccessResponse<object> { Result = expected });

            object result = await _service.CopyAsync(req);
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public async Task RenameAsync_Success_ReturnsResult()
        {
            FSRenameParams req = new FSRenameParams();
            object expected = new object();
            _ = _mockClient.Setup(c => c.PostAsync<SandboxFSSuccessResponse<object>>("/fs/rename", req, It.IsAny<CancellationToken>()))
                .ReturnsAsync(new SandboxFSSuccessResponse<object> { Result = expected });

            object result = await _service.RenameAsync(req);
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public async Task RemoveAsync_Success_ReturnsResult()
        {
            FSRemoveParams req = new FSRemoveParams();
            object expected = new object();
            _ = _mockClient.Setup(c => c.PostAsync<SandboxFSSuccessResponse<object>>("/fs/remove", req, It.IsAny<CancellationToken>()))
                .ReturnsAsync(new SandboxFSSuccessResponse<object> { Result = expected });

            object result = await _service.RemoveAsync(req);
            Assert.AreEqual(expected, result);
        }
    }
}