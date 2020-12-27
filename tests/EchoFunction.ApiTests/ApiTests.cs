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
            var baseUri = Environment.GetEnvironmentVariable("StagingSlotUri");
            var responseMessage = await httpClient.GetAsync(new Uri($"https://{baseUri}/api/Echo"));
            Assert.True(responseMessage.IsSuccessStatusCode);
        }
    }
}
