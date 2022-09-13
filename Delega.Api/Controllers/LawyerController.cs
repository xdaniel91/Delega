using Delega.Api.Interfaces.Services;
using Delega.Api.Models;
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
    public IActionResult GetById(int id)
    {
        var lawyer = Service.GetById(id);

        if (lawyer is null)
            return NotFound(lawyer);

        return Ok(lawyer);
    }


    [HttpPost]
    public IActionResult Post([FromBody] LawyerCreateRequest lawyer)
    {
        if (ModelState.IsValid)
        {
            var result = Service.Add(lawyer);
            return Ok(result);
        }
        else
            return BadRequest();
    }
}

