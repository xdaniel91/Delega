using Microsoft.AspNetCore.Mvc;

namespace Delega.Api.Controllers;

[Route("[controller]")]
[ApiController]
public class PersonController : ControllerBase
{

    public PersonController()
    {

    }

    [HttpGet]
    public async Task<IActionResult> GetAllAsync()
    {
        return null;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetAsync()
    {
        return null;
    }

    [HttpPost]
    public async Task<IActionResult> PostAsync()
    {
        return null;
    }
}
