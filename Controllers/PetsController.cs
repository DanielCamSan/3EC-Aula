using FirstExam.DTOs;
using FirstExam.Models;
using FirstExam.Services;
using Microsoft.AspNetCore.Mvc;

namespace FirstExam.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class PetsController : ControllerBase
    {
        private readonly IPetService _service;

        public PetsController(IPetService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var pets = await _service.GetAllAsync();
            return Ok(pets);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetOne(Guid id)
        {
            var pet = await _service.GetByIdAsync(id);
            return pet is null
                ? NotFound(new { error = "Pet not found", status = 404 })
                : Ok(pet);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreatePetDto dto)
        {
            if (!ModelState.IsValid)
                return ValidationProblem(ModelState);

            try
            {
                var pet = await _service.CreateAsync(dto);
                return CreatedAtAction(nameof(GetOne), new { id = pet.Id }, pet);
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(new { error = ex.Message, status = 409 });
            }
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdatePetDto dto)
        {
            if (!ModelState.IsValid)
                return ValidationProblem(ModelState);

            try
            {
                var updated = await _service.UpdateAsync(id, dto);
                return updated is null
                    ? NotFound(new { error = "Pet not found", status = 404 })
                    : Ok(updated);
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(new { error = ex.Message, status = 409 });
            }
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var success = await _service.DeleteAsync(id);
            return success
                ? NoContent()
                : NotFound(new { error = "Pet not found", status = 404 });
        }
    }
}
