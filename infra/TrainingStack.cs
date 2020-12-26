using Pulumi;
using Pulumi.Azure.Core;
using Pulumi.Azure.Network;
using Pulumi.Azure.Network.Inputs;

// ReSharper disable once CheckNamespace
class TrainingStack : Stack
{
    public TrainingStack()
    {
        var config = new Config();
        var suffix = config.Get("suffix");

        var resourceGroup = new ResourceGroup("rg-pulumi-training");
        var virtualNetwork = new VirtualNetwork($"vnet-{suffix}-main", new VirtualNetworkArgs
        {
            ResourceGroupName = resourceGroup.Name,
            AddressSpaces = new InputList<string>
            {
                "10.1.0.0/16"
            },
            Subnets = new InputList<VirtualNetworkSubnetArgs>
            {
                new VirtualNetworkSubnetArgs
                {
                    AddressPrefix = "10.1.0.0/20",
                    Name = "frontend"
                }
            }
        });
        VNetId = virtualNetwork.Id;
        
    }

    [Output]
    public Output<string> VNetId { get; set; }
}
