using System;
using System.Net;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using CodeSandbox.SDK.Net.Interfaces;
using CodeSandbox.SDK.Net.Internal;
using CodeSandbox.SDK.Net.Models.New.SandboxTaskModels;
using CodeSandbox.SDK.Net.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace CodeSandbox.SDK.Net.Tests.Services
{
    [TestClass]
    public class TaskServiceTests
    {
        private Mock<IApiClient> _mockApiClient;
        private Mock<LoggerService> _mockLogger;
        private ITaskService _service;

        [TestInitialize]
        public void Setup()
        {
            _mockApiClient = new Mock<IApiClient>(MockBehavior.Strict);
            _mockLogger = new Mock<LoggerService>(LogLevel.Trace);
            _service = new TaskService(_mockApiClient.Object, _mockLogger.Object);
        }

        [TestMethod]
        public async Task GetTaskListAsync_Success_ReturnsResult()
        {
            var expected = new SandboxTaskSuccessResponse<SandboxTaskListResult> { Status = 0, Result = new SandboxTaskListResult() };
            _mockApiClient.Setup(c => c.GetAsync<SandboxTaskSuccessResponse<SandboxTaskListResult>>("/task/list", It.IsAny<CancellationToken>()))
                .ReturnsAsync(expected);

            var result = await _service.GetTaskListAsync();
            Assert.AreEqual(expected.Status, result.Status);
        }

        [TestMethod]
        public async Task GetTaskListAsync_ApiError_ThrowsTaskServiceException()
        {
            var error = new SandboxTaskErrorResponse { Error = new SandboxTaskError { Code = 123, Message = "fail" } };
            _mockApiClient.Setup(c => c.GetAsync<SandboxTaskSuccessResponse<SandboxTaskListResult>>("/task/list", It.IsAny<CancellationToken>()))
                .ThrowsAsync(new TaskServiceException(123, "fail"));

            var ex = await Assert.ThrowsExceptionAsync<TaskServiceException>(() => _service.GetTaskListAsync());
            StringAssert.Contains(ex.Message, "fail");
            Assert.AreEqual(123, ex.ErrorCode);
        }

        [TestMethod]
        public async Task RunTaskAsync_Success_ReturnsResult()
        {
            var expected = new SandboxTaskSuccessResponse<SandboxTaskResult> { Status = 0, Result = new SandboxTaskResult() };
            _mockApiClient.Setup(c => c.PostAsync<SandboxTaskSuccessResponse<SandboxTaskResult>>("/task/run/task1", null, It.IsAny<CancellationToken>()))
                .ReturnsAsync(expected);

            var result = await _service.RunTaskAsync("task1");
            Assert.AreEqual(expected.Status, result.Status);
        }

        [TestMethod]
        public async Task RunTaskAsync_ApiError_ThrowsTaskServiceException()
        {
            _mockApiClient.Setup(c => c.PostAsync<SandboxTaskSuccessResponse<SandboxTaskResult>>("/task/run/task1", null, It.IsAny<CancellationToken>()))
                .ThrowsAsync(new TaskServiceException(123, "fail"));

            var ex = await Assert.ThrowsExceptionAsync<TaskServiceException>(() => _service.RunTaskAsync("task1"));
            StringAssert.Contains(ex.Message, "fail");
            Assert.AreEqual(123, ex.ErrorCode);
        }

        [TestMethod]
        public async Task RunCommandAsync_Success_ReturnsResult()
        {
            var expected = new SandboxTaskSuccessResponse<SandboxTaskResult> { Status = 0, Result = new SandboxTaskResult() };
            _mockApiClient.Setup(c => c.PostAsync<SandboxTaskSuccessResponse<SandboxTaskResult>>("/task/run/task1", It.IsAny<object>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(expected);

            var result = await _service.RunCommandAsync("task1", "echo hi");
            Assert.AreEqual(expected.Status, result.Status);
        }

        [TestMethod]
        public async Task StopTaskAsync_Success_ReturnsResult()
        {
            var expected = new SandboxTaskSuccessResponse<SandboxTaskResult> { Status = 0, Result = new SandboxTaskResult() };
            _mockApiClient.Setup(c => c.PostAsync<SandboxTaskSuccessResponse<SandboxTaskResult>>("/task/stop/task1", null, It.IsAny<CancellationToken>()))
                .ReturnsAsync(expected);

            var result = await _service.StopTaskAsync("task1");
            Assert.AreEqual(expected.Status, result.Status);
        }

        [TestMethod]
        public async Task CreateTaskAsync_Success_ReturnsResult()
        {
            var expected = new SandboxTaskSuccessResponse<SandboxTaskResult> { Status = 0, Result = new SandboxTaskResult() };
            _mockApiClient.Setup(c => c.PostAsync<SandboxTaskSuccessResponse<SandboxTaskResult>>("/task/create/task1", null, It.IsAny<CancellationToken>()))
                .ReturnsAsync(expected);

            var result = await _service.CreateTaskAsync("task1");
            Assert.AreEqual(expected.Status, result.Status);
        }

        [TestMethod]
        public async Task UpdateTaskAsync_Success_ReturnsResult()
        {
            var expected = new SandboxTaskSuccessResponse<SandboxTaskResult> { Status = 0, Result = new SandboxTaskResult() };
            _mockApiClient.Setup(c => c.PutAsync<SandboxTaskSuccessResponse<SandboxTaskResult>>("/task/update/task1", null, It.IsAny<CancellationToken>()))
                .ReturnsAsync(expected);

            var result = await _service.UpdateTaskAsync("task1");
            Assert.AreEqual(expected.Status, result.Status);
        }

        [TestMethod]
        public async Task SaveToConfigAsync_Success_ReturnsResult()
        {
            var expected = new SandboxTaskSuccessResponse<SandboxTaskResult> { Status = 0, Result = new SandboxTaskResult() };
            _mockApiClient.Setup(c => c.PostAsync<SandboxTaskSuccessResponse<SandboxTaskResult>>("/task/save/task1", null, It.IsAny<CancellationToken>()))
                .ReturnsAsync(expected);

            var result = await _service.SaveToConfigAsync("task1");
            Assert.AreEqual(expected.Status, result.Status);
        }

        [TestMethod]
        public async Task GenerateConfigAsync_Success_ReturnsResult()
        {
            var expected = new SandboxTaskSuccessResponse<SandboxTaskResult> { Status = 0, Result = new SandboxTaskResult() };
            _mockApiClient.Setup(c => c.PostAsync<SandboxTaskSuccessResponse<SandboxTaskResult>>("/task/generate/task1", null, It.IsAny<CancellationToken>()))
                .ReturnsAsync(expected);

            var result = await _service.GenerateConfigAsync("task1");
            Assert.AreEqual(expected.Status, result.Status);
        }

        [TestMethod]
        public async Task CreateSetupTasksAsync_Success_ReturnsResult()
        {
            var expected = new SandboxTaskSuccessResponse<SandboxTaskSetupTasksResult> { Status = 0, Result = new SandboxTaskSetupTasksResult() };
            _mockApiClient.Setup(c => c.PostAsync<SandboxTaskSuccessResponse<SandboxTaskSetupTasksResult>>("/task/createSetupTasks", It.IsAny<object>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(expected);

            var req = new SandboxTaskCreateSetupTasksRequest();
            var result = await _service.CreateSetupTasksAsync(req);
            Assert.AreEqual(expected.Status, result.Status);
        }
    }
}