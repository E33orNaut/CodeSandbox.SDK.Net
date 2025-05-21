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
    public class GitStatusServiceTests
    {
        private Mock<ApiClient> _mockApiClient;
        private Mock<LoggerService> _mockLogger;
        private GitStatusService _service;

        [TestInitialize]
        public void Setup()
        {
            _mockApiClient = new Mock<ApiClient>("https://dummy.url", null, null);
            _mockLogger = new Mock<LoggerService>(LogLevel.Trace);
            _service = new GitStatusService(_mockApiClient.Object, _mockLogger.Object);
        }

        #region GetStatusAsync

        [TestMethod]
        public async Task GetStatusAsync_ReturnsGitStatus_AndLogsProperly()
        {
            GitStatus expected = new GitStatus();
            _ = _mockApiClient.Setup(c => c.GetAsync<GitStatus>("/git/status", It.IsAny<CancellationToken>()))
                .ReturnsAsync(expected);

            GitStatus result = await _service.GetStatusAsync();

            Assert.AreEqual(expected, result);
            _mockLogger.Verify(l => l.LogInfo("Starting GetStatusAsync..."), Times.Once);
            _mockLogger.Verify(l => l.LogSuccess("GetStatusAsync completed successfully."), Times.Once);
        }

        [TestMethod]
        public async Task GetStatusAsync_ApiException_ThrowsExceptionAndLogs()
        {
            Net.Services.ApiException apiEx = new Net.Services.ApiException("API fail", "500");
            _ = _mockApiClient.Setup(c => c.GetAsync<GitStatus>("/git/status", It.IsAny<CancellationToken>()))
                .ThrowsAsync(apiEx);

            Exception ex = await Assert.ThrowsExceptionAsync<Exception>(() => _service.GetStatusAsync());

            StringAssert.Contains(ex.Message, "API error while getting Git status");
            _mockLogger.Verify(l => l.LogError(It.Is<string>(s => s.Contains("API error in GetStatusAsync"))), Times.Once);
            _mockLogger.Verify(l => l.LogTrace(It.IsAny<string>()), Times.AtLeastOnce);
        }

        [TestMethod]
        public async Task GetStatusAsync_GeneralException_ThrowsExceptionAndLogs()
        {
            InvalidOperationException generalEx = new InvalidOperationException("Something bad");
            _ = _mockApiClient.Setup(c => c.GetAsync<GitStatus>("/git/status", It.IsAny<CancellationToken>()))
                .ThrowsAsync(generalEx);

            Exception ex = await Assert.ThrowsExceptionAsync<Exception>(() => _service.GetStatusAsync());

            StringAssert.Contains(ex.Message, "Unexpected error while getting Git status");
            _mockLogger.Verify(l => l.LogError("Unexpected error in GetStatusAsync.", generalEx), Times.Once);
            _mockLogger.Verify(l => l.LogTrace(It.IsAny<string>()), Times.AtLeastOnce);
        }

        #endregion

        #region GetTargetDiffAsync

        [TestMethod]
        public async Task GetTargetDiffAsync_ReturnsGitTargetDiff_AndLogsProperly()
        {
            GitTargetDiff expected = new GitTargetDiff();
            _ = _mockApiClient.Setup(c => c.GetAsync<GitTargetDiff>("/git/targetDiff", It.IsAny<CancellationToken>()))
                .ReturnsAsync(expected);

            GitTargetDiff result = await _service.GetTargetDiffAsync();

            Assert.AreEqual(expected, result);
            _mockLogger.Verify(l => l.LogInfo("Starting GetTargetDiffAsync..."), Times.Once);
            _mockLogger.Verify(l => l.LogSuccess("GetTargetDiffAsync completed successfully."), Times.Once);
        }

        [TestMethod]
        public async Task GetTargetDiffAsync_ApiException_ThrowsExceptionAndLogs()
        {
            Net.Services.ApiException apiEx = new Net.Services.ApiException("API fail", "400");
            _ = _mockApiClient.Setup(c => c.GetAsync<GitTargetDiff>("/git/targetDiff", It.IsAny<CancellationToken>()))
                .ThrowsAsync(apiEx);

            Exception ex = await Assert.ThrowsExceptionAsync<Exception>(() => _service.GetTargetDiffAsync());

            StringAssert.Contains(ex.Message, "API error while getting Git target diff");
            _mockLogger.Verify(l => l.LogError(It.Is<string>(s => s.Contains("API error in GetTargetDiffAsync"))), Times.Once);
            _mockLogger.Verify(l => l.LogTrace(It.IsAny<string>()), Times.AtLeastOnce);
        }

        [TestMethod]
        public async Task GetTargetDiffAsync_GeneralException_ThrowsExceptionAndLogs()
        {
            Exception generalEx = new Exception("Fail");
            _ = _mockApiClient.Setup(c => c.GetAsync<GitTargetDiff>("/git/targetDiff", It.IsAny<CancellationToken>()))
                .ThrowsAsync(generalEx);

            Exception ex = await Assert.ThrowsExceptionAsync<Exception>(() => _service.GetTargetDiffAsync());

            StringAssert.Contains(ex.Message, "Unexpected error while getting Git target diff");
            _mockLogger.Verify(l => l.LogError("Unexpected error in GetTargetDiffAsync.", generalEx), Times.Once);
            _mockLogger.Verify(l => l.LogTrace(It.IsAny<string>()), Times.AtLeastOnce);
        }

        #endregion

        #region GetRemotesAsync

        [TestMethod]
        public async Task GetRemotesAsync_ReturnsGitRemotes_AndLogsProperly()
        {
            GitRemotes expected = new GitRemotes();
            _ = _mockApiClient.Setup(c => c.GetAsync<GitRemotes>("/git/remotes", It.IsAny<CancellationToken>()))
                .ReturnsAsync(expected);

            GitRemotes result = await _service.GetRemotesAsync();

            Assert.AreEqual(expected, result);
            _mockLogger.Verify(l => l.LogInfo("Starting GetRemotesAsync..."), Times.Once);
            _mockLogger.Verify(l => l.LogSuccess("GetRemotesAsync completed successfully."), Times.Once);
        }

        [TestMethod]
        public async Task GetRemotesAsync_ApiException_ThrowsExceptionAndLogs()
        {
            Net.Services.ApiException apiEx = new Net.Services.ApiException("API fail", "404");
            _ = _mockApiClient.Setup(c => c.GetAsync<GitRemotes>("/git/remotes", It.IsAny<CancellationToken>()))
                .ThrowsAsync(apiEx);

            Exception ex = await Assert.ThrowsExceptionAsync<Exception>(() => _service.GetRemotesAsync());

            StringAssert.Contains(ex.Message, "API error while getting Git remotes");
            _mockLogger.Verify(l => l.LogError(It.Is<string>(s => s.Contains("API error in GetRemotesAsync"))), Times.Once);
            _mockLogger.Verify(l => l.LogTrace(It.IsAny<string>()), Times.AtLeastOnce);
        }

        [TestMethod]
        public async Task GetRemotesAsync_GeneralException_ThrowsExceptionAndLogs()
        {
            Exception generalEx = new Exception("Fail");
            _ = _mockApiClient.Setup(c => c.GetAsync<GitRemotes>("/git/remotes", It.IsAny<CancellationToken>()))
                .ThrowsAsync(generalEx);

            Exception ex = await Assert.ThrowsExceptionAsync<Exception>(() => _service.GetRemotesAsync());

            StringAssert.Contains(ex.Message, "Unexpected error while getting Git remotes");
            _mockLogger.Verify(l => l.LogError("Unexpected error in GetRemotesAsync.", generalEx), Times.Once);
            _mockLogger.Verify(l => l.LogTrace(It.IsAny<string>()), Times.AtLeastOnce);
        }

        #endregion

        #region GetRemoteParamsAsync

        [TestMethod]
        public async Task GetRemoteParamsAsync_ReturnsGitRemoteParams_AndLogsProperly()
        {
            string reference = "ref123";
            string path = "some/path";
            GitRemoteParams expected = new GitRemoteParams();
            string expectedUrl = $"/git/remoteParams?reference={Uri.EscapeDataString(reference)}&path={Uri.EscapeDataString(path)}";

            _ = _mockApiClient.Setup(c => c.GetAsync<GitRemoteParams>(expectedUrl, It.IsAny<CancellationToken>()))
                .ReturnsAsync(expected);

            GitRemoteParams result = await _service.GetRemoteParamsAsync(reference, path);

            Assert.AreEqual(expected, result);
            _mockLogger.Verify(l => l.LogInfo($"Starting GetRemoteParamsAsync for reference '{reference}', path '{path}'..."), Times.Once);
            _mockLogger.Verify(l => l.LogSuccess("GetRemoteParamsAsync completed successfully."), Times.Once);
        }

        [TestMethod]
        [DataRow(null, "path")]
        [DataRow("", "path")]
        [DataRow("   ", "path")]
        [DataRow("ref", null)]
        [DataRow("ref", "")]
        [DataRow("ref", "   ")]
        public async Task GetRemoteParamsAsync_InvalidArguments_ThrowsArgumentNullException(string reference, string path)
        {
            _ = await Assert.ThrowsExceptionAsync<ArgumentNullException>(() => _service.GetRemoteParamsAsync(reference, path));
        }

        [TestMethod]
        public async Task GetRemoteParamsAsync_ApiException_ThrowsExceptionAndLogs()
        {
            string reference = "ref";
            string path = "path";
            string expectedUrl = $"/git/remoteParams?reference={Uri.EscapeDataString(reference)}&path={Uri.EscapeDataString(path)}";

            Net.Services.ApiException apiEx = new Net.Services.ApiException("API fail", "400");
            _ = _mockApiClient.Setup(c => c.GetAsync<GitRemoteParams>(expectedUrl, It.IsAny<CancellationToken>()))
                .ThrowsAsync(apiEx);

            Exception ex = await Assert.ThrowsExceptionAsync<Exception>(() => _service.GetRemoteParamsAsync(reference, path));

            StringAssert.Contains(ex.Message, $"API error while getting remote params for reference '{reference}', path '{path}'");
            _mockLogger.Verify(l => l.LogError(It.Is<string>(s => s.Contains("API error in GetRemoteParamsAsync"))), Times.Once);
            _mockLogger.Verify(l => l.LogTrace(It.IsAny<string>()), Times.AtLeastOnce);
        }

        [TestMethod]
        public async Task GetRemoteParamsAsync_GeneralException_ThrowsExceptionAndLogs()
        {
            string reference = "ref";
            string path = "path";
            string expectedUrl = $"/git/remoteParams?reference={Uri.EscapeDataString(reference)}&path={Uri.EscapeDataString(path)}";

            Exception generalEx = new Exception("Fail");
            _ = _mockApiClient.Setup(c => c.GetAsync<GitRemoteParams>(expectedUrl, It.IsAny<CancellationToken>()))
                .ThrowsAsync(generalEx);

            Exception ex = await Assert.ThrowsExceptionAsync<Exception>(() => _service.GetRemoteParamsAsync(reference, path));

            StringAssert.Contains(ex.Message, $"Unexpected error while getting remote params for reference '{reference}', path '{path}'");
            _mockLogger.Verify(l => l.LogError(It.Is<string>(s => s.Contains("Unexpected error in GetRemoteParamsAsync"))), Times.Once);
            _mockLogger.Verify(l => l.LogTrace(It.IsAny<string>()), Times.AtLeastOnce);
        }

        #endregion

        #region GetDiffStatusAsync

        [TestMethod]
        public async Task GetDiffStatusAsync_ReturnsGitDiffStatusResult_AndLogsProperly()
        {
            string baseRef = "base123";
            string headRef = "head456";
            GitDiffStatusResult expected = new GitDiffStatusResult();

            _ = _mockApiClient.Setup(c => c.PostAsync<GitDiffStatusResult>(
                "/git/diffStatus",
                It.Is<GitDiffStatusParams>(p => p.Base == baseRef && p.Head == headRef),
                It.IsAny<CancellationToken>()))
                .ReturnsAsync(expected);

            GitDiffStatusResult result = await _service.GetDiffStatusAsync(baseRef, headRef);

            Assert.AreEqual(expected, result);
            _mockLogger.Verify(l => l.LogInfo($"Starting GetDiffStatusAsync between '{baseRef}' and '{headRef}'..."), Times.Once);
            _mockLogger.Verify(l => l.LogSuccess("GetDiffStatusAsync completed successfully."), Times.Once);
        }

        [TestMethod]
        [DataRow(null, "head")]
        [DataRow("", "head")]
        [DataRow("   ", "head")]
        [DataRow("base", null)]
        [DataRow("base", "")]
        [DataRow("base", "   ")]
        public async Task GetDiffStatusAsync_InvalidArguments_ThrowsArgumentNullException(string baseRef, string headRef)
        {
            _ = await Assert.ThrowsExceptionAsync<ArgumentNullException>(() => _service.GetDiffStatusAsync(baseRef, headRef));
        }

        [TestMethod]
        public async Task GetDiffStatusAsync_ApiException_ThrowsExceptionAndLogs()
        {
            string baseRef = "base";
            string headRef = "head";
            Net.Services.ApiException apiEx = new Net.Services.ApiException("API fail", "503");

            _ = _mockApiClient.Setup(c => c.PostAsync<GitDiffStatusResult>(
                "/git/diffStatus",
                It.IsAny<GitDiffStatusParams>(),
                It.IsAny<CancellationToken>()))
                .ThrowsAsync(apiEx);

            Exception ex = await Assert.ThrowsExceptionAsync<Exception>(() => _service.GetDiffStatusAsync(baseRef, headRef));

            StringAssert.Contains(ex.Message, $"API error while getting diff status between '{baseRef}' and '{headRef}'");
            _mockLogger.Verify(l => l.LogError(It.Is<string>(s => s.Contains("API error in GetDiffStatusAsync"))), Times.Once);
            _mockLogger.Verify(l => l.LogTrace(It.IsAny<string>()), Times.AtLeastOnce);
        }

        [TestMethod]
        public async Task GetDiffStatusAsync_GeneralException_ThrowsExceptionAndLogs()
        {
            string baseRef = "base";
            string headRef = "head";
            Exception generalEx = new Exception("Fail");

            _ = _mockApiClient.Setup(c => c.PostAsync<GitDiffStatusResult>(
                "/git/diffStatus",
                It.IsAny<GitDiffStatusParams>(),
                It.IsAny<CancellationToken>()))
                .ThrowsAsync(generalEx);

            Exception ex = await Assert.ThrowsExceptionAsync<Exception>(() => _service.GetDiffStatusAsync(baseRef, headRef));

            StringAssert.Contains(ex.Message, $"Unexpected error while getting diff status between '{baseRef}' and '{headRef}'");
            _mockLogger.Verify(l => l.LogError(It.Is<string>(s => s.Contains("Unexpected error in GetDiffStatusAsync"))), Times.Once);
            _mockLogger.Verify(l => l.LogTrace(It.IsAny<string>()), Times.AtLeastOnce);
        }

        #endregion
    }
}
