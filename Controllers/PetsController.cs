using FirstExam.Models.DTO;
using FirstExam.Services;
using Microsoft.AspNetCore.Mvc;

namespace FirstExam.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PetsController : ControllerBase
    {
        private readonly IPetService _service;

        public PetsController(IPetService service)
        {
            _service = service;
        }

        // GET: api/pets
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var pets = await _service.GetAll();
            return Ok(pets);
        }

        // GET: api/pets/{id}
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetOne(Guid id)
        {
            var pet = await _service.GetById(id);
            return pet == null
                ? NotFound(new { error = "Pet not found", status = 404 })
                : Ok(pet);
        }

        // POST: api/pets
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreatePetDto dto)
        {
            if (!ModelState.IsValid) return ValidationProblem(ModelState);

            var pet = await _service.Create(dto);
            return CreatedAtAction(nameof(GetOne), new { id = pet.Id }, pet);
        }

        // PUT: api/pets/{id}
        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdatePetDto dto)
        {
            if (!ModelState.IsValid) return ValidationProblem(ModelState);

            var updated = await _service.Update(id, dto);
            return updated == null
                ? NotFound(new { error = "Pet not found", status = 404 })
                : Ok(updated);
        }

        // DELETE: api/pets/{id}
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
