using NewtonSoft.Json;

namespace F1App.Api.Models
{
  puclic class Driver
  {
    [JsonProperty(PropertyName = "id")]
    public string Id { get; set; } = string.Empty;

    public string Name { get; set; } = string.Empty;

    public int DriverNumber { get; set; }

    [JsonProperty(PropertyName = "team")]
    public string Team { get; set; } = string.Empty;
  }
}
