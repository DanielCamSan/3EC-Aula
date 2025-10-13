using FirstExam.Models;
using Microsoft.AspNetCore.Mvc;
using FirstExam.Models.dtos;
using FirstExam.Services;


namespace FirstExam.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
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
            var items = await _service.GetAll();
            return Ok(items);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetOne(Guid id)
        {
            var owner = await _service.GetById(id);
            return owner is null ? NotFound(new { error = "Owner not found", status = 404 }) : Ok(owner);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateOwnerDto dto)
        {
            if (!ModelState.IsValid) return ValidationProblem(ModelState);
            var owner = await _service.Create(dto);
            return CreatedAtAction(nameof(GetOne), new { id = owner.Id }, owner);
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var success = await _service.Delete(id);
            return success ? NoContent() : NotFound(new { error = "Owner not found", status = 404 });
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateOwnerDto dto)
        {
            var updated = await _service.Update(id, dto);
            if (updated is null)
                return NotFound();

            return Ok(updated);
        }

    }
}
