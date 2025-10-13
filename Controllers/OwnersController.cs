using FirstExam.Models;
using FirstExam.Models.dtos;
using FirstExam.Services;
using Microsoft.AspNetCore.Mvc;

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
        public async Task<IActionResult> GetAll()
        {
            IEnumerable<Owner> items = await _service.GetAll();           
            return Ok(items);
        }
        [HttpGet("{id:guid}")]
        public async Task<ActionResult<Owner>> GetOne(Guid id)
        {
            var owner = await _service.GetById(id);
            return owner is null ? NotFound(new { error = "owner not found ", status = 404 }): Ok(owner);  

        }
        [HttpPost]
        public async Task<ActionResult<Owner>> Create([FromBody] CreateOwnerDto dto)
        {
            if (!ModelState.IsValid) return ValidationProblem(ModelState);
            var owner = await _service.Create(dto);
            return CreatedAtAction(nameof(GetOne), new {id=owner.Id},owner);
        }
        [HttpPut("{id:guid}")]
        public async Task<ActionResult<Owner>> Update(Guid id, [FromBody] UpdateOwnerDto dto)
        {
            if (!ModelState.IsValid) return ValidationProblem(ModelState);
            var updated = await _service.Update(id, dto);
           
            return Ok(updated);
        }
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var removed = await _service.Delete(id);
            return !removed ?
                NotFound(new  { error = "Owner not found", status = 404 }) :
                NoContent();
        }

    }
}
