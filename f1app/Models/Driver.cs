

namespace F1App.Api.Models;

public class Driver
{
  public string Id { get; set; } = string.Empty;
  public string Name { get; set; } = string.Empty;
  public int DriverNumber { get; set; }
  public string Team { get; set; } = string.Empty;
}
