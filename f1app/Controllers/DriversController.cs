using Microsoft.AspNetCore.Mvc;
using F1App.Api.Services;
using F1App.Api.Models;

namespace F1App.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class DriversController : ControllerBase
{
  private readonly IDriverRepository _driverRepository;
  public DriversController(IDriverRepository driverRepository)
  {
    _driverRepository = driverRepository;
  }

  [HttpGet]
  public async Task<IActionResult> Get()
  {
    var result = await _driverRepository.GetDriversAsync();
    return Ok(result);
  }

  [HttpGet("{id}")]
  public async Task<IActionResult> Get(string id, string team)
  {
    var result = await _driverRepository.GetDriverAsync(id, team);
    return Ok(result);
  }

  [HttpPost]
  public async Task<IActionResult> Post([FromBody] Driver driver)
  {
    var result = await _driverRepository.CreateDriverAsync(driver);
    return CreatedAtAction(nameof(Get), new { result.Id, driver.team }, result);
  }

  [HttpPut("{id}")]
  public async Task<IActionResult> Put(string id, [FromBody] Driver driver)
  {
    var result = await _driverRepository.UpdateDriverAsync(id, driver);
    return Ok(result);
  }

  [HttpDelete("{id}")]
  public async Task<IActionResult> Delete(string id, string team)
  {
    var result = await _driverRepository.DeleteDriverAsync(id, team);

    if (result)
      return NoContent();

    return BadRequest();
  }
}