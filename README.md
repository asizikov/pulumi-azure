# pulumi-azure
Pulumi managed infrastructure project for Azure

## Structure

There are four C# projects here:

* Azure Function
* Unit Tests project
* API Tests project
* Pulumi infrastructure project


### Azure Pipelines

Azure Pipelines is used to build and test the Azure Function code, compile and run Pulumi, deploy Function to Azure (staging slot), Verify that it's up and running and swap the slot to puduction.

![Pipeline](docs/pipeline.png)

And another pipleline is used to delete temp environment when the feature branch is done.