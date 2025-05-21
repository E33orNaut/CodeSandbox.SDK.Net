using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using CodeSandbox.SDK.Net.Internal;
using CodeSandbox.SDK.Net.Models;
using CodeSandbox.SDK.Net.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Newtonsoft.Json;

namespace CodeSandbox.SDK.Net.Tests.Services
{
    [TestClass]
    public class GitServiceTests
    {
        private Mock<ApiClient> _mockClient;
        private GitService _service;
        public class DiscardResult
        {
            [JsonProperty("result")]
            public DiscardPaths Result { get; set; }
        }

        public class DiscardPaths
        {
            [JsonProperty("paths")]
            public List<string> Paths { get; set; }
        }
        [TestInitialize]
        public void Setup()
        {
            _mockClient = new Mock<ApiClient>(null, null);
            _service = new GitService(_mockClient.Object);
        }

        [TestMethod]
        public async Task GetStatusAsync_ReturnsExpectedResult()
        { 
            GitStatusResult expectedResult = new GitStatusResult
            {
                Status = 200,
                Result = new GitStatus
                {
                    ChangedFiles = new GitChangedFiles(),
                    DeletedFiles = new List<GitItem>(),
                    Conflicts = false,
                    LocalChanges = true,
                    Remote = null,
                    Target = null,
                    Head = "abc123",
                    Commits = new List<GitCommit>(),
                    Branch = "main",
                    IsMerging = false
                }
            };

            _ = _mockClient.Setup(c => c.PostAsync<GitStatusResult>(
                    "/git/status", It.IsAny<object>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(expectedResult);
             
            GitStatusResult actual = await _service.GetStatusAsync();
             
            Assert.IsNotNull(actual);
            Assert.AreEqual(expectedResult.Status, actual.Status);
            Assert.IsNotNull(actual.Result);
            Assert.AreEqual("main", actual.Result.Branch);
            Assert.IsFalse(actual.Result.Conflicts);
            Assert.IsTrue(actual.Result.LocalChanges);
        }


        [TestMethod]
        public async Task GetRemotesAsync_ShouldReturnResult_WhenSuccessful()
        { 
            GitRemotesResult expectedResult = new GitRemotesResult
            {
                Status = 200,
                Result = new GitRemotes
                {
                    Origin = "https://github.com/user/repo.git",
                    Upstream = "https://github.com/upstream/repo.git"
                }
            };

            _ = _mockClient
                .Setup(c => c.PostAsync<GitRemotesResult>("/git/remotes", It.IsAny<object>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(expectedResult);
             
            GitRemotesResult actual = await _service.GetRemotesAsync();
             
            Assert.IsNotNull(actual);
            Assert.AreEqual(expectedResult.Status, actual.Status);
            Assert.IsNotNull(actual.Result);
            Assert.AreEqual(expectedResult.Result.Origin, actual.Result.Origin);
            Assert.AreEqual(expectedResult.Result.Upstream, actual.Result.Upstream);
        }


        [TestMethod]
        public async Task GetTargetDiffAsync_ShouldReturnResult_WhenSuccessful()
        { 
            GitTargetDiffResult expectedResult = new GitTargetDiffResult
            {
                Status = 200,
                Result = new GitTargetDiff
                {
                    Ahead = 3,
                    Behind = 1,
                    Commits = new List<GitCommit>
            {
                new GitCommit { /* set properties as needed */ },
                new GitCommit { /* set properties as needed */ }
            }
                }
            };

            _ = _mockClient
                .Setup(c => c.PostAsync<GitTargetDiffResult>("/git/targetDiff", It.IsAny<object>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(expectedResult);
             
            GitTargetDiffResult actual = await _service.GetTargetDiffAsync("main");
             
            Assert.IsNotNull(actual);
            Assert.AreEqual(expectedResult.Status, actual.Status);
            Assert.IsNotNull(actual.Result);
            Assert.AreEqual(expectedResult.Result.Ahead, actual.Result.Ahead);
            Assert.AreEqual(expectedResult.Result.Behind, actual.Result.Behind);
            Assert.AreEqual(expectedResult.Result.Commits.Count, actual.Result.Commits.Count);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public async Task GetTargetDiffAsync_NullBranch_Throws()
        {
            _ = await _service.GetTargetDiffAsync(null);
        }

        [TestMethod]
        public async Task PostPullAsync_CallsApi()
        {
            _ = _mockClient.Setup(c => c.PostAsync<object>("/git/pull", It.IsAny<object>(), It.IsAny<CancellationToken>()))
                       .ReturnsAsync(null);

            await _service.PostPullAsync("main");

            _mockClient.Verify(c => c.PostAsync<object>("/git/pull", It.IsAny<object>(), It.IsAny<CancellationToken>()), Times.Once);
        }

        [TestMethod]
        public async Task PostDiscardAsync_ReturnsDiscardedPaths()
        {
            
            List<string> expectedPaths = new List<string> { "file1.txt", "file2.txt" };
            DiscardResult discardResult = new DiscardResult
            {
                Result = new DiscardPaths { Paths = expectedPaths }
            };

            _ = _mockClient.Setup(x => x.PostAsync<DiscardResult>(
                "/git/discard",
                It.IsAny<object>(),
                It.IsAny<CancellationToken>()))
                .ReturnsAsync(discardResult);
             
            List<string> result = await _service.PostDiscardAsync(expectedPaths.ToArray());

            
            CollectionAssert.AreEqual(expectedPaths, result);
        }

        [TestMethod]
        public async Task GetStatusAsync_ReturnsGitStatusResult()
        {
            GitStatusResult expectedResult = new GitStatusResult
            {
                Status = 200,
                Result = new GitStatus()
                {
                }
            };

            _ = _mockClient.Setup(c => c.PostAsync<GitStatusResult>(
                "/git/status",
                It.IsAny<object>(),
                It.IsAny<CancellationToken>()))
                .ReturnsAsync(expectedResult);

            GitStatusResult actual = await _service.GetStatusAsync();

            Assert.IsNotNull(actual);
            Assert.AreEqual(expectedResult.Status, actual.Status);
        }
        [TestMethod]
        public async Task PostCommitAsync_CallsApi()
        {
            _ = _mockClient.Setup(c => c.PostAsync<object>("/git/commit", It.IsAny<object>(), It.IsAny<CancellationToken>()))
                       .ReturnsAsync(null);

            await _service.PostCommitAsync("Initial commit");

            _mockClient.Verify(c => c.PostAsync<object>("/git/commit", It.IsAny<object>(), It.IsAny<CancellationToken>()), Times.Once);
        }

        [TestMethod]
        public async Task PostRemoteAddAsync_CallsApi()
        {
            _ = _mockClient.Setup(c => c.PostAsync<object>("/git/remoteAdd", It.IsAny<object>(), It.IsAny<CancellationToken>()))
                       .ReturnsAsync(null);

            await _service.PostRemoteAddAsync("https://github.com/user/repo.git");

            _mockClient.Verify(c => c.PostAsync<object>("/git/remoteAdd", It.IsAny<object>(), It.IsAny<CancellationToken>()), Times.Once);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public async Task PostCommitAsync_NullMessage_Throws()
        {
            await _service.PostCommitAsync(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public async Task PostRemoteAddAsync_NullUrl_Throws()
        {
            await _service.PostRemoteAddAsync(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task PostDiscardAsync_NullPaths_Throws()
        {
            _ = await _service.PostDiscardAsync(null);
        }
    }
}
