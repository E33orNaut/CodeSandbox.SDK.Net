using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using CodeSandbox.SDK.Net.Internal;
using CodeSandbox.SDK.Net.Models.New.SandboxTaskModels;
using CodeSandbox.SDK.Net.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Moq.Protected;

namespace CodeSandbox.SDK.Net.Tests.Services
{
    [TestClass]
    public class TaskServiceTests
    {
        private Mock<HttpMessageHandler> _mockHandler;
        private HttpClient _httpClient;
        private Mock<LoggerService> _mockLogger;
        private TaskService _service;

        [TestInitialize]
        public void Setup()
        {
            _mockHandler = new Mock<HttpMessageHandler>(MockBehavior.Strict);
            _httpClient = new HttpClient(_mockHandler.Object) { BaseAddress = new Uri("http://localhost") };
            _mockLogger = new Mock<LoggerService>(LogLevel.Trace);
            _service = new TaskService(_httpClient, _mockLogger.Object);
        }

        private void SetupHttpResponse(string url, object responseObj, HttpStatusCode status = HttpStatusCode.OK)
        {
            var response = new HttpResponseMessage(status)
            {
                Content = new StringContent(JsonSerializer.Serialize(responseObj))
            };
            _mockHandler.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync",
                    ItExpr.Is<HttpRequestMessage>(req => req.RequestUri.PathAndQuery.StartsWith(url)),
                    ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(response);
        }

        [TestMethod]
        public async Task GetTaskListAsync_Success_ReturnsResult()
        {
            var expected = new SandboxTaskSuccessResponse<SandboxTaskListResult> { Status = 0, Result = new SandboxTaskListResult() };
            SetupHttpResponse("/task/list", expected);

            var result = await _service.GetTaskListAsync();
            Assert.AreEqual(expected.Status, result.Status);
        }

        [TestMethod]
        public async Task GetTaskListAsync_ApiError_ThrowsTaskServiceException()
        {
            var error = new SandboxTaskErrorResponse { Error = new SandboxTaskError { Code = 123, Message = "fail" } };
            SetupHttpResponse("/task/list", error, HttpStatusCode.BadRequest);

            var ex = await Assert.ThrowsExceptionAsync<TaskServiceException>(() => _service.GetTaskListAsync());
            StringAssert.Contains(ex.Message, "fail");
            Assert.AreEqual(123, ex.ErrorCode);
        }

        [TestMethod]
        public async Task RunTaskAsync_Success_ReturnsResult()
        {
            var expected = new SandboxTaskSuccessResponse<SandboxTaskResult> { Status = 0, Result = new SandboxTaskResult() };
            SetupHttpResponse("/task/run/task1", expected);

            var result = await _service.RunTaskAsync("task1");
            Assert.AreEqual(expected.Status, result.Status);
        }

        [TestMethod]
        public async Task RunTaskAsync_ApiError_ThrowsTaskServiceException()
        {
            var error = new SandboxTaskErrorResponse { Error = new SandboxTaskError { Code = 123, Message = "fail" } };
            SetupHttpResponse("/task/run/task1", error, HttpStatusCode.BadRequest);

            var ex = await Assert.ThrowsExceptionAsync<TaskServiceException>(() => _service.RunTaskAsync("task1"));
            StringAssert.Contains(ex.Message, "fail");
            Assert.AreEqual(123, ex.ErrorCode);
        }

        [TestMethod]
        public async Task RunCommandAsync_Success_ReturnsResult()
        {
            var expected = new SandboxTaskSuccessResponse<SandboxTaskResult> { Status = 0, Result = new SandboxTaskResult() };
            SetupHttpResponse("/task/run/task1", expected);

            var result = await _service.RunCommandAsync("task1", "echo hi");
            Assert.AreEqual(expected.Status, result.Status);
        }

        [TestMethod]
        public async Task StopTaskAsync_Success_ReturnsResult()
        {
            var expected = new SandboxTaskSuccessResponse<SandboxTaskResult> { Status = 0, Result = new SandboxTaskResult() };
            SetupHttpResponse("/task/stop/task1", expected);

            var result = await _service.StopTaskAsync("task1");
            Assert.AreEqual(expected.Status, result.Status);
        }

        [TestMethod]
        public async Task CreateTaskAsync_Success_ReturnsResult()
        {
            var expected = new SandboxTaskSuccessResponse<SandboxTaskResult> { Status = 0, Result = new SandboxTaskResult() };
            SetupHttpResponse("/task/create/task1", expected);

            var result = await _service.CreateTaskAsync("task1");
            Assert.AreEqual(expected.Status, result.Status);
        }

        [TestMethod]
        public async Task UpdateTaskAsync_Success_ReturnsResult()
        {
            var expected = new SandboxTaskSuccessResponse<SandboxTaskResult> { Status = 0, Result = new SandboxTaskResult() };
            SetupHttpResponse("/task/update/task1", expected);

            var result = await _service.UpdateTaskAsync("task1");
            Assert.AreEqual(expected.Status, result.Status);
        }

        [TestMethod]
        public async Task SaveToConfigAsync_Success_ReturnsResult()
        {
            var expected = new SandboxTaskSuccessResponse<SandboxTaskResult> { Status = 0, Result = new SandboxTaskResult() };
            SetupHttpResponse("/task/save/task1", expected);

            var result = await _service.SaveToConfigAsync("task1");
            Assert.AreEqual(expected.Status, result.Status);
        }

        [TestMethod]
        public async Task GenerateConfigAsync_Success_ReturnsResult()
        {
            var expected = new SandboxTaskSuccessResponse<SandboxTaskResult> { Status = 0, Result = new SandboxTaskResult() };
            SetupHttpResponse("/task/generate/task1", expected);

            var result = await _service.GenerateConfigAsync("task1");
            Assert.AreEqual(expected.Status, result.Status);
        }

        [TestMethod]
        public async Task CreateSetupTasksAsync_Success_ReturnsResult()
        {
            var expected = new SandboxTaskSuccessResponse<SandboxTaskSetupTasksResult> { Status = 0, Result = new SandboxTaskSetupTasksResult() };
            SetupHttpResponse("/task/createSetupTasks", expected);

            var req = new SandboxTaskCreateSetupTasksRequest();
            var result = await _service.CreateSetupTasksAsync(req);
            Assert.AreEqual(expected.Status, result.Status);
        }
    }
}