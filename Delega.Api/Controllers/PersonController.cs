using Delega.Dominio.Exceptions;
using Delega.Infraestrutura.DTOs;
using Delega.Infraestrutura.DTOs.Update;
using Delega.Infraestrutura.Services_Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Delega.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PersonController : ControllerBase
{
    private readonly IPersonService _personService;

    public PersonController(IPersonService personService)
    {
        _personService = personService;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetPersonAsync(long id)
    {
        var cancellationToken = HttpContext.RequestAborted;

        try
        {
            var result = await _personService.GetPersonAsync(id, cancellationToken);
            return Ok(result);
        }
        catch (DelegaDataException de)
        {
            return NotFound(de.Message);
        }
        catch (DelegaDomainException de)
        {
            return BadRequest(de.Message);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    [HttpPost]
    public async Task<IActionResult> AddPersonAsync([FromBody] PersonCreateDTO createDto)
    {
        var cancellationToken = HttpContext.RequestAborted;

        try
        {
            var result = await _personService.AddPersonAsync(createDto, cancellationToken);
            return StatusCode(201, result);
        }
        catch (DelegaDataException de)
        {
            return BadRequest(de.Message);
        }
        catch (DelegaDomainException de)
        {
            return BadRequest(de.Message);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    [HttpPatch]
    public async Task<IActionResult> UpdatePersonAsync([FromBody] PersonUpdateDTO updateDto)
    {
        var cancellationToken = HttpContext.RequestAborted;

        try
        {
            var result = await _personService.UpdatePersonAsync(updateDto, cancellationToken);
            return Ok(result);
        }
        catch (DelegaDomainException de)
        {
            return BadRequest(de.Message);
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