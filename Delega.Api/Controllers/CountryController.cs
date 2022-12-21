using Delega.Dominio.Exceptions;
using Delega.Infraestrutura.DTOs;
using Delega.Infraestrutura.DTOs.Update;
using Delega.Infraestrutura.Services_Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Delega.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CountryController : ControllerBase
{
    private readonly ICountryService _countryService;

    public CountryController(ICountryService countryService)
    {
        _countryService = countryService;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetCountryAsync(long id)
    {
        var cancellationToken = HttpContext.RequestAborted;

        try
        {
            var result = await _countryService.GetCountryAsync(id, cancellationToken);
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
    public async Task<IActionResult> AddCountryAsync([FromBody] CountryCreateDTO createDto)
    {
        var cancellationToken = HttpContext.RequestAborted;

        try
        {
            var result = await _countryService.AddCountryAsync(createDto, cancellationToken);
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
    public async Task<IActionResult> UpdateCountryAsync([FromBody] CountryUpdateDTO updateDto)
    {
        var cancellationToken = HttpContext.RequestAborted;

        try
        {
            var result = await _countryService.UpdateCountryAsync(updateDto, cancellationToken);
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
