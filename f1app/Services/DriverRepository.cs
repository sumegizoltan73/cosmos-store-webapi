using Microsoft.Azure.Cosmos; // TODO: namespace hiányzik, container vagy ilyesmi
//using Microsoft.Azure.Cosmos;
using F1App.Api.Models;

namespace F1App.Api.Services;
public class DriverRepository : IDriverRepository
{
  private readonly Container container;
  public DriverRepository(
    string conn,
    string key,
    string databaseName,
    string containerName)
  {
    var cosmosClient = new CosmosClient(conn, key, new CosmosClientOptions() { });
    container = cosmosClient.GetContainer(databaseName, containerName);

  }
  public async Task<IEnumerable<Driver>> GetDriversAsync()
  {
    var query = container.GetItemQueryIterator<Driver>(new QueryDefinition("SELECT * FROM c"));

    var result = new List<Driver>();

    while (query.HasMoreResults)
    {
      var response = await query.ReadNextAsync();
      result.AddRange(response.ToList());
    }

    return result;
  }
  public async Task<Driver> GetDriverAsync(string id, string team)
  {
    try
    {
      var response = await container.ReadItemAsync<Driver>(id, new PartitionKey(team));
      return response;
    }
    catch (System.Exception)
    {
      return null;
    }
  }
  public async Task<Driver> CreateDriverAsync(Driver driver)
  {
    var response = await container.CreateItemAsync(driver, new PartitionKey(driver.Team));
    return response.Resource;
  }
  public async Task<Driver> UpdateDriverAsync(string id, Driver driver)
  {
    // https://learn.microsoft.com/en-us/dotnet/api/microsoft.azure.cosmos.container.patchitemasync?view=azure-dotnet
    // műveletek egyenként patchoperation osztállyal definiálandóak egy listában.
    List<PatchOperation> patchOperations = new List<PatchOperation>()
    {
      PatchOperation.Add("/daysOfWeek", new string[]{"Monday", "Thursday"}),
      PatchOperation.Replace("/frequency", 2),
      PatchOperation.Remove("/description")
    };

    var response = await container.PatchItemAsync<Driver>(driver.Id, new PartitionKey(driver.Team), patchOperations);
    return response.Resource;
  }
  public async Task<bool> DeleteDriverAsync(string id, string team)
  {
    var response = await container.DeleteItemAsync<Driver>(id, new PartitionKey(team));
    if (response.Resource != null)
      return false;

    return true;
  }

}