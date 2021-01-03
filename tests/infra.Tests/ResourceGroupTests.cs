using System.Threading.Tasks;
using Pulumi.Azure.Core;
using Xunit;

namespace infra.Tests
{
    public class ResourceGroupTests
    {
        [Fact]
        public async Task ResourceGroupShouldContainTags()
        {
            var resources = await Testing.RunAsync<TrainingStack>();
            foreach (var resource in resources.OfType<ResourceGroup>())
            {
                var tags = await resource.Tags.GetValueAsync();
                Assert.True(tags != null);
            }
        }
    }
}
