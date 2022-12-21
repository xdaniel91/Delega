using Delega.Dominio.Exceptions;
using Delega.Infraestrutura.DTOs;
using Delega.Infraestrutura.DTOs.Update;
using Delega.Infraestrutura.Services_Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Delega.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AddressController : Controller
{
    private readonly IAddressService _addressService;

    public AddressController(IAddressService AddressService)
    {
        _addressService = AddressService;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetAddressAsync(long id)
    {
        var cancellationToken = HttpContext.RequestAborted;

        try
        {
            var result = await _addressService.GetAddressAsync(id, cancellationToken);
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
    public async Task<IActionResult> AddAddressAsync([FromBody] AddressCreateDTO createDto)
    {
        var cancellationToken = HttpContext.RequestAborted;

        try
        {
            var result = await _addressService.AddAddressAsync(createDto, cancellationToken);
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
    public async Task<IActionResult> UpdateAddressAsync([FromBody] AddressUpdateDTO updateDto)
    {
        var cancellationToken = HttpContext.RequestAborted;

        try
        {
            var result = await _addressService.UpdateAddressAsync(updateDto, cancellationToken);
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
