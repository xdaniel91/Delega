using Delega.Api.Exceptions;
using Delega.Api.Interfaces.Services;
using Delega.Api.Models.Requests;
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
                var result = await Service.GetAllAsync();
                return Ok(result);
            }
            catch (DelegaException ex)
            {
                return BadRequest(ex);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno {Environment.NewLine}{ex.Message}");
            }
        }


        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            try
            {
                var result = Service.GetViewModel(id);

                if (result is null)
                    return NotFound();

                return Ok(result);
            }
            catch (DelegaException ex)
            {
                return BadRequest(ex);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno {Environment.NewLine}{ex.Message}");
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddAsync([FromBody] JudicialProcessCreateRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            try
            {
                var result = await Service.AddAsync(request);
                return StatusCode(201, result);
            }
            catch (DelegaException ex)
            {
                return BadRequest(ex);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno {Environment.NewLine}{ex.Message}");
            }
        }

        [HttpPost("{id}")]
        public async Task<IActionResult> InProgressAsync([FromHeader] int id)
        {
            try
            {
                var result = await Service.InProgressAsync(id);
                return Ok(result);
            }
            catch (DelegaException ex)
            {
                return BadRequest(ex);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno {Environment.NewLine}{ex.Message}");
            }
        }
    }
}
