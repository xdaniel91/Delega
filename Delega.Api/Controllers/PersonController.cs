using Delega.Api.Database;
using Delega.Api.Interfaces.Services;
using Delega.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;

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
    public async Task<IActionResult> PostAsync([FromBody] PersonCreateRequest person)
    {
        try
        {
            var ct = HttpContext.RequestAborted;
            var entity = await Service.AddAsync(person, ct);
            return Ok(entity);
        }
        catch (TaskCanceledException)
        {
            return StatusCode(StatusCodes.Status400BadRequest, "Operation cancelled");
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex);
        }
    }
}
