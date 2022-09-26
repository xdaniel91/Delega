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
        public IActionResult GetAll()
        {
            try
            {
                var processes = Service.GetAllWithRelationships();
                return Ok(processes);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }


        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            try
            {
                var judicialProcess = Service.GetByIdWithRelationships(id);

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
        public IActionResult Add([FromBody] JudicialProcessCreateRequest request)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var result = Service.Add(request);
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
            var entity = Service.GetById()
        }
    }
}
