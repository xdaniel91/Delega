using Microsoft.AspNetCore.Mvc;
using Delega.Infraestrutura.DTOs;
using Delega.Infraestrutura.DTOs.Update;
using Delega.Infraestrutura.Services_Interfaces;
using Delega.Application.Exceptions;

namespace Delega.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CityController : ControllerBase
{
    private readonly ICityService _cityService;

    public CityController(ICityService cityService)
    {
        _cityService = cityService;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetCityAsync([FromQuery] long id)
    {
        var cancellationToken = HttpContext.RequestAborted;

        try
        {
            var result = await _cityService.GetCityAsync(id, cancellationToken);
            return Ok(result);
        }
        catch (DelegaDataException de)
        {
            return BadRequest(de.Message);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    [HttpPost]
    public async Task<IActionResult> AddCityAsync([FromBody] CityCreateDTO createDto)
    {
        var cancellationToken = HttpContext.RequestAborted;

        try
        {
            var result = await _cityService.AddCityAsync(createDto, cancellationToken);
            return Ok(result);
        }
        catch (DelegaDataException de)
        {
            return BadRequest(de.Message);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    [HttpPut]
    public async Task<IActionResult> UpdateCityAsync([FromBody] CityUpdateDTO updateDto)
    {
        var cancellationToken = HttpContext.RequestAborted;

        try
        {
            var result = await _cityService.UpdateCityAsync(updateDto, cancellationToken);
            return Ok(result);
        }
        catch (DelegaDataException de)
        {
            return BadRequest(de.Message);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }
}
