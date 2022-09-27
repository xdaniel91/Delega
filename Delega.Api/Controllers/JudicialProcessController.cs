using Delega.Api.Interfaces.Services;
using Delega.Api.Models.Requests;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace Delega.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class JudicialProcessController : ControllerBase
    {
        private readonly IJudicialProcessService Service;

        public JudicialProcessController(IJudicialProcessService service)
        {
            Service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            try
            {
                var processes = await Service.GetAllAsync();
                return Ok(processes);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            try
            {
                var judicialProcess = await Service.GetByIdAsync(id);

                if (judicialProcess is null)
                    return NotFound();

                return Ok(judicialProcess);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddAsync([FromBody] JudicialProcessCreateRequest request)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var result = await Service.AddAsync(request);
                    return Ok(result);
                }
                catch (ValidationException ex)
                {
                    return BadRequest(ex);
                }
                catch (Exception ex)
                {
                    return BadRequest(ex);
                }

            }
            return BadRequest();
        }

        [HttpPost("{id}")]
        public Task<IActionResult> InProgressAsync([FromHeader] int id)
        {
            var result = Service.InProgressAsync(id);
        }
    }
}
