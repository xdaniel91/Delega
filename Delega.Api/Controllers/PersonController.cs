using Delega.Api.Interfaces.Services;
using Delega.Api.Models;
using Microsoft.AspNetCore.Mvc;

namespace Delega.Api.Controllers;

[Route("[controller]")]
[ApiController]
public class PersonController : ControllerBase
{
    private readonly IPersonService Service;
    public PersonController(IPersonService personService)
    {
        Service = personService;
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        var people = Service.GetAll();

        if (people is null)
            return NotFound();
       
        return Ok(people);
    }

    [HttpGet("{id}")]
    public IActionResult Get(int id)
    {
        var person = Service.GetById(id);

        if (person is null)
            return NotFound();

        return Ok(person);
    }

    [HttpPost]
    public IActionResult Post([FromBody] PersonCreateRequest person)
    {
        try
        {
            var entity = Service.Add(person);
            
            return Ok(entity);
        }
        catch (Exception)
        { 
            throw;
        }
    }
}
