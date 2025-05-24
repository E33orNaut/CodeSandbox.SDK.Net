using System;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using CodeSandbox.SDK.Net.Internal;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Moq.Protected;
using Newtonsoft.Json;
namespace CodeSandbox.SDK.Net.Tests
{
    [TestClass]
    public class ApiClientTests
    {
        private Mock<HttpMessageHandler> _mockHttpMessageHandler;
        private HttpClient _mockHttpClient;
        private ApiClient _apiClient;

        private const string BaseUrl = "https://api.example.com/";

        [TestInitialize]
        public void Setup()
        {
            _mockHttpMessageHandler = new Mock<HttpMessageHandler>(MockBehavior.Loose);

            _mockHttpClient = new HttpClient(_mockHttpMessageHandler.Object)
            {
                BaseAddress = new Uri(BaseUrl)
            };

            _apiClient = new ApiClient(BaseUrl, "fake-token", null, _mockHttpClient);
        }

        [TestCleanup]
        public void Cleanup()
        {
            _apiClient.Dispose();
            _mockHttpClient.Dispose();
        }

        private void SetupHttpResponse(HttpMethod method, string path, HttpStatusCode statusCode, string responseContent, string mediaType = "application/json")
        {
            _mockHttpMessageHandler
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.Is<HttpRequestMessage>(req =>
                        req.Method == method &&
                        req.RequestUri == new Uri(BaseUrl + path)),
                    ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = statusCode,
                    Content = new StringContent(responseContent)
                    {
                        Headers = { ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue(mediaType) }
                    }
                });
        }

        [TestMethod]
        public async Task GetAsync_ValidRequest_ReturnsDeserializedObject()
        {
            // Arrange
            string path = "items/123";
            var expectedObj = new { Id = 123, Name = "Test Item" };
            string jsonResponse = JsonConvert.SerializeObject(expectedObj);

            SetupHttpResponse(HttpMethod.Get, path, HttpStatusCode.OK, jsonResponse);

            // Act
            var result = await _apiClient.GetAsync<dynamic>(path);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(expectedObj.Id, (int)result.Id);
            Assert.AreEqual(expectedObj.Name, (string)result.Name);
        }

        [TestMethod]
        public async Task GetAsync_NonJsonResponse_ThrowsApiException()
        {
            // Arrange
            string path = "items/123";
            string nonJsonResponse = "<html>Not JSON</html>";

            SetupHttpResponse(HttpMethod.Get, path, HttpStatusCode.OK, nonJsonResponse, mediaType: "text/html");

            // Act & Assert
            var ex = await Assert.ThrowsExceptionAsync<ApiException>(() => _apiClient.GetAsync<dynamic>(path));
            Assert.AreEqual("GET failed: Response content type is not JSON", ex.Message);
            Assert.AreEqual((int)HttpStatusCode.OK, ex.StatusCode);
            Assert.AreEqual(nonJsonResponse, ex.ResponseContent);
        }

        [TestMethod]
        public async Task PostAsync_ValidRequest_ReturnsDeserializedObject()
        {
            // Arrange
            string path = "items";
            var payload = new { Name = "NewItem" };
            var expectedResponse = new { Id = 999, Name = "NewItem" };
            string jsonResponse = JsonConvert.SerializeObject(expectedResponse);

            SetupHttpResponse(HttpMethod.Post, path, HttpStatusCode.Created, jsonResponse);

            // Act
            var result = await _apiClient.PostAsync<dynamic>(path, payload);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(expectedResponse.Id, (int)result.Id);
            Assert.AreEqual(expectedResponse.Name, (string)result.Name);
        }

        [TestMethod]
        public async Task PostAsync_ErrorResponse_ThrowsApiExceptionWithDetails()
        {
            // Arrange
            string path = "items";
            var payload = new { Name = "BadItem" };
            var errorObj = new { error = "Invalid data" };
            string jsonResponse = JsonConvert.SerializeObject(errorObj);

            SetupHttpResponse(HttpMethod.Post, path, HttpStatusCode.BadRequest, jsonResponse);

            // Act & Assert
            var ex = await Assert.ThrowsExceptionAsync<ApiException>(() => _apiClient.PostAsync<dynamic>(path, payload));
            Assert.AreEqual("POST failed: 400", ex.Message);
            Assert.AreEqual((int)HttpStatusCode.BadRequest, ex.StatusCode);
            Assert.IsNotNull(ex.ApiErrorDetails);
            Assert.AreEqual("Invalid data", (string)((dynamic)ex.ApiErrorDetails).error);
        }

        [TestMethod]
        public async Task PutAsync_ValidRequest_ReturnsDeserializedObject()
        {
            // Arrange
            string path = "items/123";
            var payload = new { Name = "UpdatedName" };
            var expectedResponse = new { Id = 123, Name = "UpdatedName" };
            string jsonResponse = JsonConvert.SerializeObject(expectedResponse);

            SetupHttpResponse(HttpMethod.Put, path, HttpStatusCode.OK, jsonResponse);

            // Act
            var result = await _apiClient.PutAsync<dynamic>(path, payload);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(expectedResponse.Id, (int)result.Id);
            Assert.AreEqual(expectedResponse.Name, (string)result.Name);
        }

        [TestMethod]
        public async Task DeleteAsync_ValidRequest_CompletesWithoutException()
        {
            // Arrange
            string path = "items/123";

            SetupHttpResponse(HttpMethod.Delete, path, HttpStatusCode.NoContent, "");

            // Act & Assert
            await _apiClient.DeleteAsync(path);
        }

        [TestMethod]
        public async Task DeleteAsync_ErrorResponse_ThrowsApiException()
        {
            // Arrange
            string path = "items/123";
            var errorObj = new { error = "Not found" };
            string jsonResponse = JsonConvert.SerializeObject(errorObj);

            SetupHttpResponse(HttpMethod.Delete, path, HttpStatusCode.NotFound, jsonResponse);

            // Act & Assert
            var ex = await Assert.ThrowsExceptionAsync<ApiException>(() => _apiClient.DeleteAsync(path));
            Assert.AreEqual("DELETE failed: 404", ex.Message);
            Assert.AreEqual((int)HttpStatusCode.NotFound, ex.StatusCode);
            Assert.IsNotNull(ex.ApiErrorDetails);
            Assert.AreEqual("Not found", (string)((dynamic)ex.ApiErrorDetails).error);
        }

        [TestMethod]
        public void Constructor_WithNullBaseUrl_ThrowsUriFormatException()
        {
            // Arrange & Act & Assert
            Assert.ThrowsException<ArgumentNullException>(() => new ApiClient(null));
        }

        [TestMethod]
        public void ValidatePath_EmptyPath_ThrowsArgumentException()
        {
            var ex = Assert.ThrowsException<ArgumentException>(() => _apiClient.ValidatePathT(""));
            Assert.AreEqual("path", ex.ParamName);
            Assert.IsTrue(ex.Message.StartsWith("Path cannot be null or empty"));
        }

     

    }
}
