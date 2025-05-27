using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using CodeSandbox.SDK.Net.Internal;
using CodeSandbox.SDK.Net.Models.New.GitModels;
using CodeSandbox.SDK.Net.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace CodeSandbox.SDK.Net.Tests.Services
{
    [TestClass]
    public class GitServiceTests
    {
        private Mock<ApiClient> _mockClient;
        private Mock<LoggerService> _mockLogger;
        private GitService _service;

        [TestInitialize]
        public void Setup()
        {
            _mockClient = new Mock<ApiClient>("http://localhost", null, null, null);
            _mockLogger = new Mock<LoggerService>(LogLevel.Trace);
            _service = new GitService(_mockClient.Object, _mockLogger.Object);
        }

        [TestMethod]
        public async Task GetStatusAsync_Success_ReturnsResponse()
        {
            var expected = new GitStatusResponse();
            _mockClient.Setup(c => c.PostAsync<GitStatusResponse>("/git/status", It.IsAny<object>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(expected);

            var result = await _service.GetStatusAsync();

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public async Task GetStatusAsync_ApiException_ThrowsWrapped()
        {
            _mockClient.Setup(c => c.PostAsync<GitStatusResponse>("/git/status", It.IsAny<object>(), It.IsAny<CancellationToken>()))
                .ThrowsAsync(new ApiException("fail", 400, "err"));

            await Assert.ThrowsExceptionAsync<Exception>(() => _service.GetStatusAsync());
        }

        [TestMethod]
        public async Task GetRemotesAsync_Success_ReturnsResponse()
        {
            var expected = new GitRemotesResponse();
            _mockClient.Setup(c => c.PostAsync<GitRemotesResponse>("/git/remotes", It.IsAny<object>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(expected);

            var result = await _service.GetRemotesAsync();

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public async Task GetTargetDiffAsync_ThrowsOnNullBranch()
        {
            await Assert.ThrowsExceptionAsync<ArgumentException>(() => _service.GetTargetDiffAsync(null));
            await Assert.ThrowsExceptionAsync<ArgumentException>(() => _service.GetTargetDiffAsync(""));
        }

        [TestMethod]
        public async Task GetTargetDiffAsync_Success_ReturnsResponse()
        {
            var expected = new GitTargetDiffResponse();
            _mockClient.Setup(c => c.PostAsync<GitTargetDiffResponse>("/git/targetDiff", It.IsAny<object>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(expected);

            var result = await _service.GetTargetDiffAsync("main");

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public async Task PostPullAsync_ThrowsOnNullBranch()
        {
            await Assert.ThrowsExceptionAsync<ArgumentException>(() => _service.PostPullAsync(null));
            await Assert.ThrowsExceptionAsync<ArgumentException>(() => _service.PostPullAsync(""));
        }

        [TestMethod]
        public async Task PostPullAsync_Success()
        {
            _mockClient.Setup(c => c.PostAsync<object>("/git/pull", It.IsAny<object>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new object());

            await _service.PostPullAsync("main");
        }

        [TestMethod]
        public async Task PostDiscardAsync_ThrowsOnNullPaths()
        {
            await Assert.ThrowsExceptionAsync<ArgumentNullException>(() => _service.PostDiscardAsync(null));
        }

        [TestMethod]
        public async Task PostDiscardAsync_Success_ReturnsPaths()
        {
            var expectedPaths = new List<string> { "file1", "file2" };
            _mockClient.Setup(c => c.PostAsync<GitDiscardResponse>("/git/discard", It.IsAny<object>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new GitDiscardResponse { Result = new GitDiscardResult { Paths = expectedPaths } });

            var result = await _service.PostDiscardAsync(new[] { "file1", "file2" });

            CollectionAssert.AreEqual(expectedPaths, result);
        }

        [TestMethod]
        public async Task PostCommitAsync_ThrowsOnNullMessage()
        {
            await Assert.ThrowsExceptionAsync<ArgumentException>(() => _service.PostCommitAsync(null));
            await Assert.ThrowsExceptionAsync<ArgumentException>(() => _service.PostCommitAsync(""));
        }

        [TestMethod]
        public async Task PostCommitAsync_Success_ReturnsShellId()
        {
            var expectedShellId = "abc123";
            _mockClient.Setup(c => c.PostAsync<GitCommitResponse>("/git/commit", It.IsAny<object>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new GitCommitResponse { Result = new GitCommitResult { ShellId = expectedShellId } });

            var result = await _service.PostCommitAsync("msg");

            Assert.AreEqual(expectedShellId, result);
        }

        [TestMethod]
        public async Task PostRemoteAddAsync_ThrowsOnNullUrl()
        {
            await Assert.ThrowsExceptionAsync<ArgumentException>(() => _service.PostRemoteAddAsync(null));
            await Assert.ThrowsExceptionAsync<ArgumentException>(() => _service.PostRemoteAddAsync(""));
        }

        [TestMethod]
        public async Task PostRemoteAddAsync_Success()
        {
            _mockClient.Setup(c => c.PostAsync<object>("/git/remoteAdd", It.IsAny<object>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new object());

            await _service.PostRemoteAddAsync("http://remote");
        }

        [TestMethod]
        public async Task PostPushAsync_Success()
        {
            _mockClient.Setup(c => c.PostAsync<object>("/git/push", It.IsAny<object>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new object());

            await _service.PostPushAsync();
        }

        [TestMethod]
        public async Task PostPushToRemoteAsync_ThrowsOnNulls()
        {
            await Assert.ThrowsExceptionAsync<ArgumentException>(() => _service.PostPushToRemoteAsync(null, "branch"));
            await Assert.ThrowsExceptionAsync<ArgumentException>(() => _service.PostPushToRemoteAsync("url", null));
            await Assert.ThrowsExceptionAsync<ArgumentException>(() => _service.PostPushToRemoteAsync("", "branch"));
            await Assert.ThrowsExceptionAsync<ArgumentException>(() => _service.PostPushToRemoteAsync("url", ""));
        }

        [TestMethod]
        public async Task PostPushToRemoteAsync_Success()
        {
            _mockClient.Setup(c => c.PostAsync<object>("/git/pushToRemote", It.IsAny<object>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new object());

            await _service.PostPushToRemoteAsync("url", "branch");
        }

        [TestMethod]
        public async Task PostRenameBranchAsync_ThrowsOnNulls()
        {
            await Assert.ThrowsExceptionAsync<ArgumentException>(() => _service.PostRenameBranchAsync(null, "new"));
            await Assert.ThrowsExceptionAsync<ArgumentException>(() => _service.PostRenameBranchAsync("old", null));
            await Assert.ThrowsExceptionAsync<ArgumentException>(() => _service.PostRenameBranchAsync("", "new"));
            await Assert.ThrowsExceptionAsync<ArgumentException>(() => _service.PostRenameBranchAsync("old", ""));
        }

        [TestMethod]
        public async Task PostRenameBranchAsync_Success()
        {
            _mockClient.Setup(c => c.PostAsync<object>("/git/renameBranch", It.IsAny<object>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new object());

            await _service.PostRenameBranchAsync("old", "new");
        }

        [TestMethod]
        public async Task PostRemoteContentAsync_ThrowsOnNulls()
        {
            await Assert.ThrowsExceptionAsync<ArgumentException>(() => _service.PostRemoteContentAsync(null, "path"));
            await Assert.ThrowsExceptionAsync<ArgumentException>(() => _service.PostRemoteContentAsync("ref", null));
            await Assert.ThrowsExceptionAsync<ArgumentException>(() => _service.PostRemoteContentAsync("", "path"));
            await Assert.ThrowsExceptionAsync<ArgumentException>(() => _service.PostRemoteContentAsync("ref", ""));
        }

        [TestMethod]
        public async Task PostRemoteContentAsync_Success_ReturnsContent()
        {
            var expectedContent = "file content";
            _mockClient.Setup(c => c.PostAsync<GitRemoteContentResponse>("/git/remoteContent", It.IsAny<object>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new GitRemoteContentResponse { Result = new GitRemoteContentResult { Content = expectedContent } });

            var result = await _service.PostRemoteContentAsync("ref", "path");

            Assert.AreEqual(expectedContent, result);
        }

        [TestMethod]
        public async Task PostDiffStatusAsync_ThrowsOnNulls()
        {
            await Assert.ThrowsExceptionAsync<ArgumentException>(() => _service.PostDiffStatusAsync(null, "head"));
            await Assert.ThrowsExceptionAsync<ArgumentException>(() => _service.PostDiffStatusAsync("base", null));
            await Assert.ThrowsExceptionAsync<ArgumentException>(() => _service.PostDiffStatusAsync("", "head"));
            await Assert.ThrowsExceptionAsync<ArgumentException>(() => _service.PostDiffStatusAsync("base", ""));
        }

        [TestMethod]
        public async Task PostDiffStatusAsync_Success_ReturnsResponse()
        {
            var expected = new GitDiffStatusResponse();
            _mockClient.Setup(c => c.PostAsync<GitDiffStatusResponse>("/git/diffStatus", It.IsAny<object>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(expected);

            var result = await _service.PostDiffStatusAsync("base", "head");

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public async Task PostResetLocalWithRemoteAsync_Success()
        {
            _mockClient.Setup(c => c.PostAsync<object>("/git/resetLocalWithRemote", It.IsAny<object>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new object());

            await _service.PostResetLocalWithRemoteAsync();
        }

        [TestMethod]
        public async Task PostCheckoutInitialBranchAsync_Success()
        {
            _mockClient.Setup(c => c.PostAsync<object>("/git/checkoutInitialBranch", It.IsAny<object>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new object());

            await _service.PostCheckoutInitialBranchAsync();
        }

        [TestMethod]
        public async Task PostTransposeLinesAsync_ThrowsOnNullOrEmpty()
        {
            await Assert.ThrowsExceptionAsync<ArgumentException>(() => _service.PostTransposeLinesAsync(null));
            await Assert.ThrowsExceptionAsync<ArgumentException>(() => _service.PostTransposeLinesAsync(new List<GitTransposeLinesResultItem>()));
        }

        [TestMethod]
        public async Task PostTransposeLinesAsync_Success_ReturnsResult()
        {
            var expected = new List<GitTransposeLinesResultItem> { new GitTransposeLinesResultItem { Path = "a", Line = 1 } };
            _mockClient.Setup(c => c.PostAsync<GitTransposeLinesResponse>("/git/transposeLines", It.IsAny<object>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new GitTransposeLinesResponse { Result = expected });

            var result = await _service.PostTransposeLinesAsync(new List<GitTransposeLinesResultItem> { new GitTransposeLinesResultItem() });

            CollectionAssert.AreEqual(expected, result);
        }
    }
}