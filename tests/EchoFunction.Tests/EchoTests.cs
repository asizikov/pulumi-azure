using System.Threading.Tasks;
using EchoFunction.Api;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging.Abstractions;
using Xunit;

namespace EchoFunction.Tests
{
    public class EchoTests
    {
        [Fact]
        public async Task FunctionReturnsOk()
        {
            var actionResult = await Echo.Run(null, NullLogger.Instance);
            Assert.True(actionResult is OkObjectResult);
        }
    }
}
