using Delega.Api.Models;
using Delega.Api.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Delega.Api.Controllers;

[Route("[controller]")]
[ApiController]
public class LawyerController : ControllerBase
{
    private readonly ILawyerService Service;

    public LawyerController(ILawyerService service)
    {
        Service = service;
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        try
        {
            var lawyers = Service.GetAll();
            return Ok(lawyers);
        }
        catch (Exception ex)
        {
            return NotFound(ex);
        }
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetByIdAsync(int id)
    {
        var lawyer = await Service.GetByIdAsync(id);

        if (lawyer is null)
            return NotFound(lawyer);

        return Ok(lawyer);
    }


    [HttpPost]
    public IActionResult Post([FromBody] LawyerCreateRequest lawyer)
    {
        if (ModelState.IsValid)
        {
            var result = Service.AddAsync(lawyer);
            return Ok(result);
        }
        else
            return BadRequest();
    }
}

