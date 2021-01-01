using System;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace EchoFunction.ApiTests
{
    public class ApiTests
    {
        private readonly ITestOutputHelper _testOutputHelper;

        public ApiTests(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
        }

        [Fact]
        public async Task FunctionReturnsOk()
        {
            var httpClient = new HttpClient();
            var baseUri = Environment.GetEnvironmentVariable("STAGINGSLOTURI");
            var requestUri = new Uri($"https://{baseUri}/api/Echo");
            _testOutputHelper.WriteLine($"Calling {requestUri}");
            var responseMessage = await httpClient.GetAsync(requestUri);
            _testOutputHelper.WriteLine($"Received {responseMessage.StatusCode} in {responseMessage}");
            Assert.True(responseMessage.IsSuccessStatusCode);
        }
    }
}
