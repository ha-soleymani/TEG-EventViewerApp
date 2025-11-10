using Moq;
using Moq.Protected;

namespace Infrastructure.Tests
{
    public static class HttpHandlerMockExtensions
    {
        public static void SetupRequest(this Mock<HttpMessageHandler> mock, string url, HttpResponseMessage response)
        {
            mock.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync",
                    ItExpr.Is<HttpRequestMessage>(r => r.RequestUri.ToString() == url),
                    ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(response);
        }

        public static void SetupRequestFailure(this Mock<HttpMessageHandler> mock, string url)
        {
            mock.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync",
                    ItExpr.Is<HttpRequestMessage>(r => r.RequestUri.ToString() == url),
                    ItExpr.IsAny<CancellationToken>())
                .ThrowsAsync(new HttpRequestException("Simulated failure"));
        }
    }
}
