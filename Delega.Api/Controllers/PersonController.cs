using Delega.Api.Database;
using Delega.Api.Interfaces.Services;
using Delega.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using System.Text;

namespace Delega.Api.Controllers;

[Route("[controller]")]
[ApiController]
public class PersonController : ControllerBase
{
    private readonly IPersonService Service;
    private readonly IDistributedCache DistributedCache;
    private readonly DelegaContext Context;
    public PersonController(IPersonService personService, IDistributedCache distributedCache, DelegaContext context)
    {
        Service = personService;
        DistributedCache = distributedCache;
        Context = context;
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
