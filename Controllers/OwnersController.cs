using FirstExam.Models.dtos;
using FirstExam.Services;
using Microsoft.AspNetCore.Mvc;

namespace FirstExam.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
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
            var owners = await _service.GetAll();
            return Ok(owners);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetOne(Guid id)
        {
            var owner = await _service.GetById(id);
            return owner == null
                ? NotFound(new { error = "Owner not found", status = 404 })
                : Ok(owner);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateOwnerDto dto)
        {
            if (!ModelState.IsValid) return ValidationProblem(ModelState);
            var owner = await _service.Create(dto);
            return CreatedAtAction(nameof(GetOne), new { id = owner!.Id }, owner);
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateOwnerDto dto)
        {
            if (!ModelState.IsValid) return ValidationProblem(ModelState);

            try
            {
                await _service.Update(id, dto);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound(new { error = "Owner not found", status = 404 });
            }
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                await _service.Delete(id);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound(new { error = "Owner not found", status = 404 });
            }
        }
    }
}
