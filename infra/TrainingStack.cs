using System.Runtime.InteropServices;
using Pulumi;
using Pulumi.Azure.AppService;
using Pulumi.Azure.AppService.Inputs;
using Pulumi.Azure.Core;
using Pulumi.Azure.Storage;

// ReSharper disable once CheckNamespace
class TrainingStack : Stack
{
    public TrainingStack()
    {
        var config = new Config();
        var suffix = config.Get("suffix");

        var resourceGroup = new ResourceGroup("rg-pulumi-training");

        var plan = new Plan("service-plan-dynamic", new PlanArgs
        {
            ResourceGroupName = resourceGroup.Name,
            Kind = "FunctionApp",
            Sku = new PlanSkuArgs
            {
                Tier = "Dynamic",
                Size = "Y1"
            }
        });

        var storageAccount = new Account("saechofunction", new AccountArgs
        {
            ResourceGroupName = resourceGroup.Name,
            AccountReplicationType = "LRS",
            AccountTier = "Standard",
            AccountKind = "StorageV2"
        });
        
        var app = new FunctionApp($"{suffix}-function-app", new FunctionAppArgs
        {
            ResourceGroupName = resourceGroup.Name,
            AppServicePlanId = plan.Id,
            StorageAccountName = storageAccount.Name,
            StorageAccountAccessKey = storageAccount.PrimaryAccessKey,
            Version = "~3",
            SiteConfig = new FunctionAppSiteConfigArgs
            {
                Http2Enabled = true,
                ScmType = "VSTSRM"
            }
        });

        var stagingSlot = new FunctionAppSlot("staging", new FunctionAppSlotArgs
        {
            Name = "staging", // do not let Pulumi append random suffix
            ResourceGroupName = resourceGroup.Name,
            AppServicePlanId = plan.Id,
            StorageAccountName = storageAccount.Name,
            StorageAccountAccessKey = storageAccount.PrimaryAccessKey,
            FunctionAppName = app.Name,
            Version = "~3",
            SiteConfig = new FunctionAppSlotSiteConfigArgs
            {
                Http2Enabled = true,
                ScmType = "VSTSRM"
            }
        });
        
        //init outputs
        ResourceGroupName = resourceGroup.Name;
        FunctionStatingSlotName = stagingSlot.Name;
        AppServicePlanId = app.AppServicePlanId;
        FunctionAppName = app.Name;
        StagingSlotUri = stagingSlot.DefaultHostname;
    }
    
    [Output]
    public Output<string> ResourceGroupName { get; set; }

    [Output]
    public Output<string> AppServicePlanId { get; set; }
    
    [Output]
    public  Output<string> FunctionAppName { get; set; }

    [Output]
    public Output<string> FunctionStatingSlotName { get; set; }
    
    [Output]
    public Output<string> StagingSlotUri { get; set; }
}
