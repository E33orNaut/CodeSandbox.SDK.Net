using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using CodeSandbox.SDK.Net.Internal;
using CodeSandbox.SDK.Net.Models.New.SandboxSystemModels;
using CodeSandbox.SDK.Net.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Moq.Protected;
using Newtonsoft.Json;

namespace CodeSandbox.SDK.Net.Tests.Services
{
    [TestClass]
    public class SystemServiceTests
    {
        private Mock<HttpMessageHandler> _mockHandler;
        private HttpClient _httpClient;
        private Mock<LoggerService> _mockLogger;
        private SystemService _service;

        [TestInitialize]
        public void Setup()
        {
            _mockHandler = new Mock<HttpMessageHandler>(MockBehavior.Strict);
            _httpClient = new HttpClient(_mockHandler.Object) { BaseAddress = new Uri("http://localhost") };
            _mockLogger = new Mock<LoggerService>(LogLevel.Trace);
            _service = new SystemService(_httpClient, _mockLogger.Object);
        }

        [TestMethod]
        public async Task UpdateSystemAsync_Success_ReturnsResponse()
        {
            var expected = new SandboxSystemSuccessResponse { Status = 0 };
            var httpResponse = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(JsonConvert.SerializeObject(expected))
            };
            _mockHandler.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(httpResponse);

            var result = await _service.UpdateSystemAsync();
            Assert.AreEqual(expected.Status, result.Status);
        }

        [TestMethod]
        public async Task UpdateSystemAsync_ApiError_ThrowsApiException()
        {
            var error = new SandboxSystemErrorResponse { Error = new SandboxSystemError { Code = 0, Message = "fail" } };
            var httpResponse = new HttpResponseMessage(HttpStatusCode.BadRequest)
            {
                Content = new StringContent(JsonConvert.SerializeObject(error))
            };
            _mockHandler.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(httpResponse);

            var ex = await Assert.ThrowsExceptionAsync<ApiException>(() => _service.UpdateSystemAsync());
            StringAssert.Contains(ex.Message, "fail");
        }

        [TestMethod]
        public async Task HibernateSystemAsync_Success_ReturnsResponse()
        {
            var expected = new SandboxSystemSuccessResponse { Status = 200 };
            var httpResponse = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(JsonConvert.SerializeObject(expected))
            };
            _mockHandler.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(httpResponse);

            var result = await _service.HibernateSystemAsync();
            Assert.AreEqual(expected.Status, result.Status);
        }

        [TestMethod]
        public async Task GetSystemMetricsAsync_Success_ReturnsMetrics()
        {
            var metrics = new SandboxSystemMetricsStatus
            {
                Cpu = new SandboxSystemCpuMetrics {  },
                Memory = new SandboxSystemMemoryMetrics {  },
                Storage = new SandboxSystemStorageMetrics {  }
            };
            var wrapper = new SandboxSystemSuccessResponse { Result = metrics };
            var httpResponse = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(JsonConvert.SerializeObject(wrapper))
            };
            _mockHandler.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(httpResponse);

            var result = await _service.GetSystemMetricsAsync();
            Assert.AreEqual(metrics.Cpu, result.Cpu);
            Assert.AreEqual(metrics.Memory, result.Memory);
            Assert.AreEqual(metrics.Storage, result.Storage);
        }

        [TestMethod]
        public async Task GetSystemMetricsAsync_ApiError_ThrowsApiException()
        {
            var error = new SandboxSystemErrorResponse { Error = new SandboxSystemError { Code = 123, Message = "fail" } };
            var httpResponse = new HttpResponseMessage(HttpStatusCode.BadRequest)
            {
                Content = new StringContent(JsonConvert.SerializeObject(error))
            };
            _mockHandler.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(httpResponse);

            var ex = await Assert.ThrowsExceptionAsync<ApiException>(() => _service.GetSystemMetricsAsync());
            StringAssert.Contains(ex.Message, "fail");
        }
    }
}