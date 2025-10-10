using FirstExam.Models;
using FirstExam.Models.dtos;
using FirstExam.Services;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
namespace FirstExam.Controllers
{
    [ApiController]
    [Route("api/v1/[Controller]")]
    public class OwnersController : ControllerBase
    {
        private readonly IOwnerService _service;
        public OwnersController(IOwnerService service)
        {
            _service = service;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] int Page, [FromQuery] int limit, [FromQuery] string sort, [FromQuery] string? order, [FromQuery] string? Q)
        {
            var owners = await _service.GetAll();
            return Ok(owners);
        }
        [HttpGet("{id:guid}")]
        public async Task<ActionResult<Owner>> GetOne(Guid id)
        {
            var owner = await _service.GetById(id);
            return owner == null
                ? NotFound(new { error = "Owner not found", status = 404 })
                : Ok(owner);
        }
        [HttpPost]
        public async Task<ActionResult<Owner>> Create([FromBody] CreateOwnerDto dto)
        {
            if (!ModelState.IsValid) return ValidationProblem(ModelState);
            var owner = await _service.Create(dto);
            return CreatedAtAction(nameof(GetOne), new { id = owner.Id }, owner);
        }
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var success = await _service.Delete(id);
            return success
                ? NoContent()
                : NotFound(new { error = "Owner not found", status = 404 });
        }
        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateOwnerDto dto)
        {
            if(!ModelState.IsValid) return ValidationProblem(ModelState);
            var updated = await _service.Update(id, dto);
            if(updated == null)
            {
                return NotFound(new { error = "Owner not found", status = 404 });
            }
            return Ok(updated);
        }
    }
}
