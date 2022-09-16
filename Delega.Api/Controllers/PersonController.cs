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

    //[HttpGet("redis")]
    //public IActionResult GetRedis()
    //{
    //    var cacheKey = "personlist";
    //    string serialized;
    //    var personList = new List<Person>();

    //    var redisList = DistributedCache.Get(cacheKey);

    //    if (redisList != null)
    //    {
    //        serialized = Encoding.UTF8.GetString(redisList);
    //        personList = JsonConvert.DeserializeObject<List<Person>>(serialized);
    //    }
    //    else
    //    {
    //        personList = Context.person.ToList();
    //        serialized = JsonConvert.SerializeObject(personList);
    //        redisList = Encoding.UTF8.GetBytes(serialized);

    //        var options = new DistributedCacheEntryOptions().SetAbsoluteExpiration(DateTime.Now.AddMinutes(80))
    //            .SetSlidingExpiration(TimeSpan.FromMinutes(2));

    //        DistributedCache.Set(cacheKey, redisList, options);
    //    }

    //    return Ok(personList);
    //}

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
