using F1App.Api.Models;

namespace F1App.Api.Services
{
  interface IDriverRepository
  {
    Task<IEnumerable<Driver>> GetDriversAsync();
    Task<Driver> GetDriverAsync(string id, string team);
    Task<Driver> CreateDriverAsync(Driver driver);
    Task<Driver> UpdateDriverAsync(string id, Driver driver);
    Task<bool> DeleteDriver(string id, string team);
  }
}
