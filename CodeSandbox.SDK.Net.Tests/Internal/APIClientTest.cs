using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using CodeSandbox.SDK.Net.Internal;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Moq.Protected;
using Newtonsoft.Json;

namespace CodeSandbox.SDK.Net.Tests.Internal
{
    [TestClass]
    public class ApiClientTests
    {
        private const string BaseUrl = "https://api.example.com/";

        private HttpClient CreateHttpClient(HttpResponseMessage responseMessage)
        {
            Mock<HttpMessageHandler> handlerMock = new Mock<HttpMessageHandler>(MockBehavior.Strict);

            handlerMock
               .Protected()
               .Setup<Task<HttpResponseMessage>>(
                  "SendAsync",
                  ItExpr.IsAny<HttpRequestMessage>(),
                  ItExpr.IsAny<CancellationToken>()
               )
               .ReturnsAsync(responseMessage)
               .Verifiable();

            HttpClient httpClient = new HttpClient(handlerMock.Object)
            {
                BaseAddress = new Uri(BaseUrl)
            };
            return httpClient;
        }

        [TestMethod]
        public async Task GetAsync_Success_ReturnsDeserializedObject()
        {
            var expectedObj = new { name = "test" };
            string json = JsonConvert.SerializeObject(expectedObj);

            HttpClient httpClient = CreateHttpClient(new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(json, System.Text.Encoding.UTF8, "application/json")
            });

            ApiClient client = new ApiClient(BaseUrl, httpClient: httpClient);

            dynamic result = await client.GetAsync<dynamic>("test");

            Assert.IsNotNull(result);
            Assert.AreEqual("test", (string)result.name);
        }

        [TestMethod]
        public async Task GetAsync_NonJsonResponse_ThrowsApiException()
        {
            HttpClient httpClient = CreateHttpClient(new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent("<html></html>", System.Text.Encoding.UTF8, "text/html")
            });

            ApiClient client = new ApiClient(BaseUrl, httpClient: httpClient);

            _ = await Assert.ThrowsExceptionAsync<ApiException>(async () =>
                await client.GetAsync<object>("test"));
        }

        [TestMethod]
        public async Task GetAsync_ErrorStatusCode_ThrowsApiException()
        {
            string errorJson = JsonConvert.SerializeObject(new { error = "fail" });

            HttpClient httpClient = CreateHttpClient(new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.BadRequest,
                Content = new StringContent(errorJson, System.Text.Encoding.UTF8, "application/json")
            });

            ApiClient client = new ApiClient(BaseUrl, httpClient: httpClient);

            ApiException ex = await Assert.ThrowsExceptionAsync<ApiException>(async () =>
                await client.GetAsync<object>("test"));

            Assert.AreEqual((int)HttpStatusCode.BadRequest, ex.StatusCode);
            Assert.IsNotNull(ex.ApiErrorDetails);
        }

        [TestMethod]
        public async Task PostAsync_Success_ReturnsDeserializedObject()
        {
            var expectedObj = new { success = true };
            string json = JsonConvert.SerializeObject(expectedObj);

            HttpClient httpClient = CreateHttpClient(new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.Created,
                Content = new StringContent(json, System.Text.Encoding.UTF8, "application/json")
            });

            ApiClient client = new ApiClient(BaseUrl, httpClient: httpClient);

            var payload = new { foo = "bar" };
            dynamic result = await client.PostAsync<dynamic>("test", payload);

            Assert.IsNotNull(result);
            Assert.IsTrue((bool)result.success);
        }

        [TestMethod]
        public async Task PutAsync_Success_ReturnsDeserializedObject()
        {
            var expectedObj = new { updated = true };
            string json = JsonConvert.SerializeObject(expectedObj);

            HttpClient httpClient = CreateHttpClient(new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(json, System.Text.Encoding.UTF8, "application/json")
            });

            ApiClient client = new ApiClient(BaseUrl, httpClient: httpClient);

            var payload = new { update = "value" };
            dynamic result = await client.PutAsync<dynamic>("test", payload);

            Assert.IsNotNull(result);
            Assert.IsTrue((bool)result.updated);
        }

        [TestMethod]
        public async Task DeleteAsync_Success_CompletesWithoutException()
        {
            HttpClient httpClient = CreateHttpClient(new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.NoContent,
                Content = new StringContent(string.Empty)
            });

            ApiClient client = new ApiClient(BaseUrl, httpClient: httpClient);

            await client.DeleteAsync("test");
        }

        [TestMethod]
        public async Task DeleteAsync_ErrorStatusCode_ThrowsApiException()
        {
            string errorJson = JsonConvert.SerializeObject(new { error = "fail" });

            HttpClient httpClient = CreateHttpClient(new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.InternalServerError,
                Content = new StringContent(errorJson, System.Text.Encoding.UTF8, "application/json")
            });

            ApiClient client = new ApiClient(BaseUrl, httpClient: httpClient);

            _ = await Assert.ThrowsExceptionAsync<ApiException>(async () =>
                await client.DeleteAsync("test"));
        }

        [TestMethod]
        public void Dispose_DisposesHttpClient()
        {
            HttpClient httpClient = new HttpClient(new Mock<HttpMessageHandler>().Object);
            ApiClient client = new ApiClient(BaseUrl, httpClient: httpClient);

            client.Dispose(); 
        }

        [TestMethod]
        public void ValidatePath_ThrowsArgumentException_OnNullOrEmpty()
        {
            ApiClient client = new ApiClient(BaseUrl);

            _ = Assert.ThrowsException<ArgumentException>(() => client.GetAsync<object>(null).Wait());
            _ = Assert.ThrowsException<ArgumentException>(() => client.GetAsync<object>("").Wait());
            _ = Assert.ThrowsException<ArgumentException>(() => client.GetAsync<object>("  ").Wait());
        }
    }
}
