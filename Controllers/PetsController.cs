using FirstExam.Models;
using FirstExam.Models.Dtos;
using FirstExam.Services;
using Microsoft.AspNetCore.Mvc;

namespace FirstExam.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class PetsController : Controller
    {
        private readonly IPetService _service;
        public PetsController(IPetService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var pets = await _service.GetAll();
            return Ok(pets);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var pet = await _service.GetById(id);
            if (pet == null) return NotFound(new { error = "Pet not found", status = 404 });
            return Ok(pet);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreatePetDto dto)
        {
            if (!ModelState.IsValid) return ValidationProblem(ModelState);
            try
            {
                var pet = await _service.Create(dto);
                return CreatedAtAction(nameof(GetById), new { id = pet.Id }, pet);
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(new { message = ex.Message, status = 409 });
            }
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdatePetDto dto)
        {
            if (!ModelState.IsValid) return ValidationProblem(ModelState);
            try
            {
                var updated = await _service.Update(id, dto);
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
            var success = await _service.Delete(id);
            return success
                ? NoContent()
                : NotFound(new { error = "Pet not found", status = 404 });
        }
    }
}
