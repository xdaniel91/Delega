using Microsoft.AspNetCore.Mvc;
using Delega.Infraestrutura.DTOs;
using Delega.Infraestrutura.DTOs.Update;
using Delega.Application.Exceptions;
using Delega.Infraestrutura.Services_Interfaces;

namespace Delega.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class StateController : ControllerBase
{
    private readonly IStateService _stateService;

    public StateController(IStateService stateService)
    {
        _stateService = stateService;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetStateAsync([FromQuery] long id)
    {
        var cancellationToken = HttpContext.RequestAborted;

        try
        {
            var result = await _stateService.GetStateAsync(id, cancellationToken);
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
    public async Task<IActionResult> AddStateAsync([FromBody] StateCreateDTO createDto)
    {
        var cancellationToken = HttpContext.RequestAborted;

        try
        {
            var result = await _stateService.AddStateAsync(createDto, cancellationToken);
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
    public async Task<IActionResult> UpdateStateAsync([FromBody] StateUpdateDTO updateDto)
    {
        var cancellationToken = HttpContext.RequestAborted;

        try
        {
            var result = await _stateService.UpdateStateAsync(updateDto, cancellationToken);
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
