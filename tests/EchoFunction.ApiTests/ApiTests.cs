using System;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace EchoFunction.ApiTests
{
    public class ApiTests
    {
        [Fact]
        public async Task FunctionReturnsOk()
        {
            var httpClient = new HttpClient();
            var baseUri = Environment.GetEnvironmentVariable("STAGINGSLOTURI");
            var requestUri = new Uri($"https://{baseUri}/api/Echo");
            Console.WriteLine($"Calling {requestUri.ToString()}");
            var responseMessage = await httpClient.GetAsync(requestUri);
            Assert.True(responseMessage.IsSuccessStatusCode);
        }
    }
}
